using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using Chashavshavon.Utils;
using Chashavshavon.Utils.HebrewCalendarHelper;
using Microsoft.Win32;

namespace Chashavshavon
{
    public partial class frmMain : Form
    {
        //To hold all the "Entry" objects 
        public static List<Entry> Entries;
        public static XmlDocument LocationsXmlDoc;

        #region Private Variables
        private HebrewCalendar _hc;
        private CultureInfo _ci;
        //The combos are changed dynamically and we don't want to fire the change event during initial loading.
        private bool _loading;
        private DateTime _today;
        private Onah _nowOnah;
        private Location _location;
        #endregion

        public frmMain()
        {
            this.Hide();
            string password = this.GetPassword();

            InitializeComponent();
            _ci = new CultureInfo("he-IL", false);
            _hc = new HebrewCalendar();
            _ci.DateTimeFormat.Calendar = _hc;
            System.Threading.Thread.CurrentThread.CurrentCulture = _ci;
            System.Threading.Thread.CurrentThread.CurrentUICulture = _ci;
            Entries = new List<Entry>();
            LocationsXmlDoc = new XmlDocument();
            LocationsXmlDoc.Load(Application.StartupPath + "\\Locations.xml");
            this.SetSummerTime();
            this.SetLocation();
            this.SetDateAndDayNight();
            this.FillZmanData();
            this.timer1.Start();
            if (password == null)
            {
                this.Show();
            }
            else
            {
                frmEnterPassword f = new frmEnterPassword(password);
                do
                {
                    f.ShowDialog();
                    if (f.DialogResult == DialogResult.No)
                        MessageBox.Show("סיסמה שגויה",
                        "חשבשבון",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                }
                while (f.DialogResult == DialogResult.No);
                this.CloseMeFirst = f.DialogResult != DialogResult.Yes;
            }
        }

        public frmMain(string filePath)
            : this()
        {
            this.CurrentFileIsRemote = false;
            this.CurrentFile = filePath;
        }

        #region Event Handlers
        private void frmMain_Load(object sender, EventArgs e)
        {
            if (this.CloseMeFirst)
            {
                this.Close();
            }

            dgEntries.AutoGenerateColumns = false;
            _loading = true;
            this.TestInternet();
            this.LoadXmlFile();
            _loading = false;
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.SaveCurrentFile();
            Properties.Settings.Default.Save();
        }

        private void cmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_loading)
            {
                return;
            }
            this.FillDays();
            this.FillZmanim();
        }

        private void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_loading)
            {
                return;
            }
            _loading = true;
            this.FillMonths();
            _loading = false;
            this.FillDays();
            this.FillZmanim();
        }

        private void rbDay_CheckedChanged(object sender, EventArgs e)
        {
            rbDay.Checked = !rbNight.Checked;
        }

        private void rbNight_CheckedChanged(object sender, EventArgs e)
        {
            rbNight.Checked = !rbDay.Checked;
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            Entry newEntry = new Entry();

            newEntry.Day = this.cmbDay.SelectedIndex + 1;
            newEntry.Month = (MonthObject)cmbMonth.SelectedItem;
            newEntry.Year = (int)cmbYear.SelectedItem;
            newEntry.DayNight = rbNight.Checked ? DayNight.Night : DayNight.Day;
            newEntry.Notes = this.txtNotes.Text;

            ((BindingSource)this.dgEntries.DataSource).Add(newEntry);
            this.SortEntriesAndSetInterval();
            this.FillCalendar();
            this.SaveCurrentFile();
        }

        private void dgEntries_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgEntries.Columns[e.ColumnIndex] == btnDeleteColumn)
            {
                Entry selectedEntry = (Entry)dgEntries.Rows[e.RowIndex].DataBoundItem;
                DialogResult dr = MessageBox.Show("האם אתם בטוחים שברצונכם למחוק השורה של " +
                                                    selectedEntry.DateTime.ToString("dd MMMM yyyy"),
                                                  "מחיקת שורה " + selectedEntry.DateTime.ToString("dd MMMM yyyy"),
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    ((BindingSource)dgEntries.DataSource).Remove(selectedEntry);
                    this.SortEntriesAndSetInterval();
                    this.FillCalendar();
                    this.SaveCurrentFile();
                }
            }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SaveCurrentFile();
            MessageBox.Show("הקובץ נשמרה" + (CurrentFileIsRemote ? " ברשת " : ""),
                            "חשבשבון",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.CheckFileExists = false;
            openFileDialog1.DefaultExt = "pm";
            openFileDialog1.FileName = DateTime.Now.ToString("ddMMMyyyy").Replace("\"", "").Replace("'", "") + ".pm";
            if (openFileDialog1.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }
            CurrentFile = openFileDialog1.FileName;
            CurrentFileIsRemote = false;

            SaveCurrentFile();
        }

        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.CurrentFile))
            {
                this.SaveCurrentFile();
            }
            openFileDialog1.CheckFileExists = false;
            openFileDialog1.DefaultExt = "pm";
            openFileDialog1.FileName = DateTime.Now.ToString("ddMMMyyyy").Replace("\"", "").Replace("'", "") + ".pm";
            openFileDialog1.FileOk += new CancelEventHandler(openFileDialog1_FileOk);
            openFileDialog1.ShowDialog();
        }

        private void AbouToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox ab = new AboutBox();
            ab.ShowDialog(this);
        }

        void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            if (openFileDialog1.FileName.Length > 0)
            {
                CurrentFile = openFileDialog1.FileName;
                CurrentFileIsRemote = false;
                LoadXmlFile();
            }
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            this.ShowTextList();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            this.PrintList();
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SaveCurrentFile();
            openFileDialog1.ShowDialog();
            openFileDialog1.CheckFileExists = true;
            CurrentFile = openFileDialog1.FileName;
            CurrentFileIsRemote = false;
            LoadXmlFile();
        }

        private void PreferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPreferences prefs = new frmPreferences();
            prefs.Top = Convert.ToInt32(this.Top + ((this.Height / 2) - (prefs.Height / 2)));
            prefs.Left = Convert.ToInt32(this.Left + ((this.Width / 2) - (prefs.Width / 2)));
            prefs.Show(this);
        }

        private void RefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.TestInternet();
            this.LoadXmlFile();
        }

        private void toolStripMenuItemRemote_Click(object sender, EventArgs e)
        {
            frmRemoteFiles f = new frmRemoteFiles();
            f.Show(this);
        }


        private void KavuahToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmKavuahs f = new frmKavuahs();
            f.ShowDialog(this);
            if (f.DialogResult != System.Windows.Forms.DialogResult.Cancel)
            {
                this.SaveCurrentFile();
                this.TestInternet();
                this.LoadXmlFile();
            }
        }

        private void PrintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintList();
        }

        private void TextListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowTextList();
        }        
       
        private void pbWeb_Click(object sender, EventArgs e)
        {
            frmRemoteFiles f = new frmRemoteFiles();
            f.Show(this);
        }

        private void cmbDay_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.FillZmanim();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.lblTime.Text = DateTime.Now.ToLongTimeString();
        }

        private void SourceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveCurrentFile();
            string fileName;
            if (CurrentFileIsRemote)
            {
                fileName = Path.GetTempFileName();
                File.WriteAllText(fileName, CurrentFileXML);
            }
            else
            {
                fileName = CurrentFile;
            }
            System.Diagnostics.Process.Start("notepad.exe", fileName);
        }
        #endregion

        #region Private Functions
        private void FillZmanData()
        {
            for (int i = 5600; i < 6001; i++)
            {
                cmbYear.Items.Add(i);
            }

            cmbYear.SelectedItem = _hc.GetYear(DateTime.Now);

            this.FillMonths();
            this.FillDays();
            this.FillZmanim();
        }

        private void FillMonths()
        {
            string currentSelection = null;
            if (cmbMonth.Items.Count != 0)
            {
                currentSelection = cmbMonth.Text;
                cmbMonth.Items.Clear();
            }
            int year = (int)cmbYear.SelectedItem;
            for (int i = 0; i < _hc.GetMonthsInYear(year); i++)
            {
                MonthObject month = new MonthObject(year, i + 1);
                if (currentSelection == null && month.Year == _hc.GetYear(DateTime.Now) && month.MonthInYear == _hc.GetMonth(DateTime.Now))
                {
                    currentSelection = month.MonthName;
                }
                cmbMonth.Items.Add(month);
                if (month.MonthName == currentSelection)
                {
                    cmbMonth.SelectedItem = month;
                }
            }
        }

        private void FillDays()
        {
            MonthObject curMonth = (MonthObject)cmbMonth.SelectedItem;
            KeyValuePair<int, string> curDay;

            if (cmbDay.Items.Count == 0)
            {
                curDay = new KeyValuePair<int, string>(_hc.GetDayOfMonth(_today), Onah.DaysOfMonthHebrew[_hc.GetDayOfMonth(_today)]);
            }
            else
            {
                curDay = (KeyValuePair<int, string>)cmbDay.SelectedItem;
            }

            cmbDay.Items.Clear();
            for (int i = 1; i < curMonth.DaysInMonth + 1; i++)
            {
                KeyValuePair<int, string> day = new KeyValuePair<int, string>(i, Onah.DaysOfMonthHebrew[i]);
                cmbDay.Items.Add(day);
            }

            //If the current month does not have as many days as the last one and the 30th day was selected, we go to day 29.
            if (curDay.Key > curMonth.DaysInMonth)
            {
                cmbDay.SelectedIndex = cmbDay.Items.Count - 1;
            }
            else
            {
                cmbDay.SelectedItem = curDay;
            }
        }

        private void FillCalendar()
        {
            DateTime tomorrow = _today.AddDays(1);
            DateTime dayAfterTomorrow = _today.AddDays(2);
            DateTime yesterday = _today.AddDays(-1);

            MonthObject ms = new MonthObject(_hc.GetYear(yesterday), _hc.GetMonth(yesterday));
            this.lblYesterdayDate.Text = Onah.DaysOfMonthHebrew[_hc.GetDayOfMonth(yesterday)] + " " + ms.MonthName;
            this.toolTip1.SetToolTip(this.lblYesterdayDate, this.GetToolTipForDate(yesterday));
            lblYesterdayWeekDay.Text = GetDayOfWeekText(yesterday);

            ms = new MonthObject(_hc.GetYear(_today), _hc.GetMonth(_today));
            this.lblTodayDate.Text = Onah.DaysOfMonthHebrew[_hc.GetDayOfMonth(_today)] + " " + ms.MonthName;
            this.toolTip1.SetToolTip(this.lblTodayDate, this.GetToolTipForDate(_today));
            lblTodayWeekDay.Text = GetDayOfWeekText(_today);

            ms = new MonthObject(_hc.GetYear(tomorrow), _hc.GetMonth(tomorrow));
            this.lblTomorrowDate.Text = Onah.DaysOfMonthHebrew[_hc.GetDayOfMonth(tomorrow)] + " " + ms.MonthName;
            this.toolTip1.SetToolTip(this.lblTomorrowDate, this.GetToolTipForDate(tomorrow));
            lblTomorrowWeekDay.Text = GetDayOfWeekText(tomorrow);

            ms = new MonthObject(_hc.GetYear(dayAfterTomorrow), _hc.GetMonth(dayAfterTomorrow));
            this.lblNextdayDate.Text = Onah.DaysOfMonthHebrew[_hc.GetDayOfMonth(dayAfterTomorrow)] + " " + ms.MonthName;
            this.toolTip1.SetToolTip(this.lblNextdayDate, this.GetToolTipForDate(dayAfterTomorrow));
            lblNextDayWeekDay.Text = GetDayOfWeekText(dayAfterTomorrow);

            lblYesterdayEntryStuff.Text = "";
            lblTodayEntryStuff.Text = "";
            lblTomorrowEntryStuff.Text = "";
            lblNextDayEntryStuff.Text = "";

            foreach (Entry entry in Entries)
            {
                if (GeneralUtils.IsSameday(entry.DateTime, yesterday))
                {
                    lblYesterdayEntryStuff.Text = "עונה: " + entry.HebrewDayNight + "\nהפלגה: " + entry.Interval.ToString();
                }
                if (GeneralUtils.IsSameday(entry.DateTime, _today))
                {
                    lblTodayEntryStuff.Text = "עונה: " + entry.HebrewDayNight + "\nהפלגה: " + entry.Interval.ToString();
                }
                if (GeneralUtils.IsSameday(entry.DateTime, tomorrow))
                {
                    lblTomorrowEntryStuff.Text = "עונה: " + entry.HebrewDayNight + "\nהפלגה: " + entry.Interval.ToString();
                }
                if (GeneralUtils.IsSameday(entry.DateTime, dayAfterTomorrow))
                {
                    lblNextDayEntryStuff.Text = "עונה: " + entry.HebrewDayNight + "\nהפלגה: " + entry.Interval.ToString();
                }
            }
            this.CalculateCalendar();
        }

        private string GetDayOfWeekText(DateTime d)
        {
            string s = Onah.DaysOfWeekHebrew[(int)_hc.GetDayOfWeek(d)];
            if (((int)_hc.GetDayOfWeek(d)) < 6)
            {
                s += "'";
            }
            return s;
        }

        private void CalculateCalendar()
        {
            this.ClearCalendar();
            DateTime tomorrow = _today.AddDays(1);
            DateTime dayAfterTomorrow = _today.AddDays(2);
            DateTime yesterday = _today.AddDays(-1);

            DateTime[] days = new DateTime[4];
            days[0] = yesterday;
            days[1] = _today;
            days[2] = tomorrow;
            days[3] = dayAfterTomorrow;
            List<Onah> onahs = new List<Onah>();
            List<Onah> problemOnas = new List<Onah>();
            Queue<Entry> lastThree = new Queue<Entry>();

            Onah thirty,
                thirtyOhrZarua,
                thirtyOne,
                thirtyOneOhrZarua,
                intervalHaflagah,
                intervalHaflagahOhrZarua,
                kavuahHaflaga,
                kavuahHaflagaOhrZarua,
                kavuahYomHachodesh,
                kavuahYomHachodeshOhrZarua;

            foreach (DateTime day in days)
            {
                onahs.Add(new Onah(day, DayNight.Night));
                onahs.Add(new Onah(day, DayNight.Day));
            }

            foreach (Entry entry in Entries)
            {
                lastThree.Enqueue(entry);
                if (lastThree.Count > 3)
                {
                    lastThree.Dequeue();
                }
                if (lastThree.Count == 3)
                {
                    this.CheshbonKavuahs(lastThree.ToArray<Entry>());
                }

                thirty = entry.AddDays(29);
                thirty.Name = "יום שלושים";
                problemOnas.Add(thirty);

                if (Properties.Settings.Default.ShowOhrZeruah)
                {
                    thirtyOhrZarua = Onah.GetPreviousOnah(thirty);
                    thirtyOhrZarua.Name = "או\"ז - יום שלושים";
                    problemOnas.Add(thirtyOhrZarua);
                }

                thirtyOne = entry.AddDays(30);
                thirtyOne.Name = "יום ל\"א";
                problemOnas.Add(thirtyOne);

                if (Properties.Settings.Default.ShowOhrZeruah)
                {
                    thirtyOneOhrZarua = Onah.GetPreviousOnah(thirtyOne);
                    thirtyOneOhrZarua.Name = "או\"ז - יום ל\"א";
                    problemOnas.Add(thirtyOneOhrZarua);
                }

                if (entry.Interval > 0)
                {
                    intervalHaflagah = entry.AddDays(entry.Interval - 1);
                    intervalHaflagah.Name = "יום הפלגה";
                    problemOnas.Add(intervalHaflagah);
                    if (Properties.Settings.Default.ShowOhrZeruah)
                    {
                        intervalHaflagahOhrZarua = Onah.GetPreviousOnah(intervalHaflagah);
                        intervalHaflagahOhrZarua.Name = "או\"ז - יום הפלגה";
                        problemOnas.Add(intervalHaflagahOhrZarua);
                    }
                }

                foreach (Kavuah kavuah in Kavuah.KavuahsList.Where(k => k.ProblemOnahType == ProblemOnahType.Haflagah))
                {
                    kavuahHaflaga = entry.AddDays(kavuah.Number);
                    kavuahHaflaga.DayNight = kavuah.DayNight;
                    kavuahHaflaga.Name = " קבוע - הפלגה (" + kavuah.Number.ToString() + ")";
                    problemOnas.Add(kavuahHaflaga);
                    if (Properties.Settings.Default.ShowOhrZeruah)
                    {
                        kavuahHaflagaOhrZarua = Onah.GetPreviousOnah(kavuahHaflaga);
                        kavuahHaflagaOhrZarua.Name = " או\"ז - " + kavuahHaflaga.Name;
                        problemOnas.Add(kavuahHaflagaOhrZarua);
                    }
                }
            }

            foreach (Onah o in onahs)
            {
                foreach (Kavuah kavuah in Kavuah.KavuahsList.Where(k =>
                                        k.ProblemOnahType == ProblemOnahType.DayOfMonth &&
                                        k.Number == o.Day &&
                                        k.DayNight == o.DayNight))
                {

                    kavuahYomHachodesh = o;
                    kavuahYomHachodesh.Name = " קבוע - יום החדש (" + Onah.DaysOfMonthHebrew[kavuah.Number] + ")";
                    problemOnas.Add(kavuahYomHachodesh);
                    if (Properties.Settings.Default.ShowOhrZeruah)
                    {
                        kavuahYomHachodeshOhrZarua = Onah.GetPreviousOnah(kavuahYomHachodesh);
                        kavuahYomHachodeshOhrZarua.Name = " או\"ז - " + kavuahYomHachodesh.Name;
                        problemOnas.Add(kavuahYomHachodeshOhrZarua);
                    }
                }
            }

            foreach (Onah onah in onahs)
            {
                foreach (Onah problemOnah in problemOnas.Where(o => Onah.IsSameOnah(onah, o)))
                {
                    switch (onahs.IndexOf(onah))
                    {
                        case 0: //yesterday night
                            ProccessProblem(lblYesterdayNight, lblYesterdayDate, problemOnah);
                            break;
                        case 1: //yesterday day  
                            ProccessProblem(lblYesterdayDay, lblYesterdayDate, problemOnah);
                            break;
                        case 2: //today night
                            ProccessProblem(lblTodayNight, lblTodayDate, problemOnah);
                            break;
                        case 3: //today day    
                            ProccessProblem(lblTodayDay, lblTodayDate, problemOnah);
                            break;
                        case 4: //tomorrow night
                            ProccessProblem(lblTomorrowNight, lblTomorrowDate, problemOnah);
                            break;
                        case 5: //tomorrow day 
                            ProccessProblem(lblTomorrowDay, lblTomorrowDate, problemOnah);
                            break;
                        case 6: //day after tomorrow night
                            ProccessProblem(lblNextDayNight, lblNextdayDate, problemOnah);
                            break;
                        case 7: //day after tomorrow day  
                            ProccessProblem(lblNextDayDay, lblNextdayDate, problemOnah);
                            break;
                    }
                }
            }
            string nextProblemText = "";
            if (problemOnas.Count > 0)
            {
                problemOnas.Sort(Onah.CompareOnahs);
                if (problemOnas.Exists(o => Onah.IsSameOnah(o, this._nowOnah)))
                {
                    nextProblemText += "עכשיו הוא " + problemOnas.First(o => Onah.IsSameOnah(o, this._nowOnah)).Name;
                }
                if (problemOnas.Exists(o => Onah.CompareOnahs(o, this._nowOnah) == 1))
                {
                    Onah nextProb = problemOnas.First(o => Onah.CompareOnahs(o, this._nowOnah) == 1);
                    if (nextProblemText.Length > 0)
                    {
                        nextProblemText += " - ";
                    }
                    nextProblemText += "העונה הבאה בעוד " +
                                               ((nextProb.DateTime - this._today).Days + 1).ToString() +
                                               " ימים - בתאריך: " +
                                               nextProb.DateTime.ToString("dd MMMM yyyy") +
                                               " (" +
                                               nextProb.HebrewDayNight +
                                               ") שהוא " +
                                               nextProb.Name;

                }
            }
            this.lblNextProblem.Text = nextProblemText;

            SetWeekListText(problemOnas);
        }

        private void SetWeekListText(List<Onah> problemOnas)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("עונות הבאות - {0} {1}\r\n{2}\r\n", 
                lblToday.Text, 
                lblTime.Text, 
                new string('-', 70));
            foreach (Onah onah in problemOnas.Where(on => DateTime.Now.AddDays(-1) < on.DateTime))
            {                
                sb.AppendFormat("{0}\r\n * {1} - {2} {3} - {4}",
                    new string('-', 30),
                    this.GetDayOfWeekText(onah.DateTime), 
                    Onah.DaysOfMonthHebrew[onah.Day],
                    onah.Month.MonthName,
                    onah.HebrewDayNight);
                sb.AppendFormat("\r\n\t{0}\r\n", onah.Name);                 
            }
            sb.AppendFormat("\r\n{0}\r\n{1}\r\n{0}",new string('-', 70), this.lblNextProblem.Text);
            this.WeekListText = sb.ToString();
        }

        private void CheshbonKavuahs(Entry[] last3Array)
        {
            if (last3Array[0].DayNight.In(last3Array[1].DayNight, last3Array[2].DayNight))
            {
                //Cheshbon out Kavuah of same haflagah
                if ((last3Array[0].Interval == last3Array[1].Interval) && 
                    (last3Array[1].Interval == last3Array[2].Interval))
                {
                    if (!Kavuah.KavuahsList.Exists(k =>
                            (k.ProblemOnahType == ProblemOnahType.Haflagah) &&
                            (k.Number == last3Array[0].Interval)))
                    {
                        if (MessageBox.Show("התוכנית זיהה מצב שבו יתכן ויש לקבוע ווסת קבוע של הפלגת " +
                            last3Array[0].Interval.ToString() +
                            " יום.\nהאם להוסיף לרשימת ווסתות הקבוע?\nבכל מקרא, מומלץ מאד להתיעץ עם מורה הוראה.",
                            "חשבשבון - ווסת קבוע",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question,
                            MessageBoxDefaultButton.Button1,
                            MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading) == System.Windows.Forms.DialogResult.Yes)
                        {
                            Kavuah.KavuahsList.Add(new Kavuah() {
                                DayNight = last3Array[0].DayNight, 
                                ProblemOnahType=ProblemOnahType.Haflagah, 
                                Number=last3Array[0].Interval });
                        }                        
                    }
                }

                //Cheshbon out Kavuah of same day
                if ((last3Array[0].Day == last3Array[1].Day) &&
                    (last3Array[1].Day == last3Array[2].Day))
                {
                    if (!Kavuah.KavuahsList.Exists(k =>
                            (k.ProblemOnahType == ProblemOnahType.DayOfMonth) &&
                            (k.Number == last3Array[0].Day)))
                    {
                        if (MessageBox.Show("התוכנית זיהה מצב שבו יתכן ויש לקבוע ווסת קבוע של יום  - " + 
                            last3Array[0].Day.ToString() + 
                            " בחודש.\nהאם להוסיף לרשימת ווסתות הקבוע?\nבכל מקרא, מומלץ מאד להתיעץ עם מורה הוראה.",
                            "חשבשבון - ווסת קבוע",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question,
                            MessageBoxDefaultButton.Button1, 
                            MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading) == System.Windows.Forms.DialogResult.Yes)
                        {
                            Kavuah.KavuahsList.Add(new Kavuah()
                            {
                                DayNight = last3Array[0].DayNight,
                                ProblemOnahType = ProblemOnahType.DayOfMonth,
                                Number = last3Array[0].Day 
                            });
                        }
                    }
                }
            }
        }

        private void ClearCalendar()
        {
            foreach (Label l in gbCalendar.Controls.OfType<Panel>().SelectMany(pnl => pnl.Controls.OfType<Label>()))
            {
                switch (Convert.ToString(l.Tag))
                {
                    case "Date":
                        l.BackColor = Color.Wheat;
                        l.ForeColor = Color.Black;
                        break;
                    case "Day":
                    case "Night":
                        l.Text = "";
                        l.BackColor = Color.White;
                        l.ForeColor = Color.Black;
                        l.Font = new Font(l.Font.FontFamily, 8);
                        break;
                }
            }
        }

        private void ProccessProblem(Label lbl, Label lblDate, Onah problemOnah)
        {
            lbl.Text = problemOnah.Name;
            lblDate.BackColor = Color.SteelBlue;
            lblDate.ForeColor = Color.Wheat;
            if (lbl.Name.Contains("Today") && this._nowOnah.DayNight == problemOnah.DayNight)
            {
                lbl.BackColor = Color.Red;
                lbl.ForeColor = Color.Wheat;
                lbl.Font = new Font(lbl.Font.FontFamily, 11);
            }
            else
            {
                lbl.BackColor = Color.Tan;
            }
        }

        private void SortEntriesAndSetInterval()
        {
            Entries.Sort(Onah.CompareOnahs);

            Entry previousEntry = null;
            foreach (Entry entry in Entries)
            {
                if (previousEntry != null)
                {
                    entry.SetInterval(previousEntry);
                }
                else
                {
                    entry.Interval = 0;
                }
                previousEntry = entry;
            }
        }

        private void SaveCurrentFile()
        {
            XmlTextWriter xtw;
            Stream stream = null;

            if (this.CurrentFileIsRemote)
            {
                stream = new MemoryStream();
            }
            else
            {
                stream = File.CreateText(this.CurrentFile).BaseStream;
            }
            xtw = new XmlTextWriter(stream, Encoding.UTF8);
            xtw.WriteStartDocument();
            xtw.WriteStartElement("Entries");
            foreach (Entry entry in Entries)
            {
                xtw.WriteStartElement("Entry");
                xtw.WriteElementString("Date", entry.DateTime.ToString("dd MMMM yyyy") + " " + entry.HebrewDayNight);
                xtw.WriteElementString("Day", entry.Day.ToString());
                xtw.WriteElementString("Month", entry.Month.MonthInYear.ToString());
                xtw.WriteElementString("Year", entry.Year.ToString());
                xtw.WriteElementString("DN", ((int)entry.DayNight).ToString());
                xtw.WriteElementString("Notes", entry.Notes);
                xtw.WriteEndElement();
            }

            var ser = new System.Xml.Serialization.XmlSerializer(typeof(List<Kavuah>));
            ser.Serialize(xtw, Kavuah.KavuahsList);

            xtw.WriteEndDocument();
            xtw.Flush();
            if (this.CurrentFileIsRemote)
            {
                stream.Position = 0;
                StreamReader sr = new StreamReader(stream);
                try
                {
                    Utils.RemoteFunctions.SaveCurrentFile(this.CurrentFile, sr.ReadToEnd());
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "חשבשבון", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                sr.Close();
                sr.Dispose();
            }
            xtw.Close();
            stream.Dispose();
            Properties.Settings.Default.IsCurrentFileRemote = CurrentFileIsRemote;
            Properties.Settings.Default.CurrentFile = this.CurrentFileName;
            Properties.Settings.Default.Save();
            this.SetCaption();
        }

        private void PrintList()
        {
            //this.WeekListText;
            using(Graphics g = this.CreateGraphics())
            {
                throw new NotImplementedException("PRINT");
            }
        }

        private void ShowTextList()
        {
            var fileName = Path.GetTempFileName();
            File.WriteAllText(fileName, this.WeekListText);
            System.Diagnostics.Process.Start("notepad.exe", fileName);
        }

        private void SetDateAndDayNight()
        {
            TimesCalculation tc = new TimesCalculation();
            AstronomicalTime shkiah = tc.GetSunset(DateTime.Now, this._location);
            AstronomicalTime netz = tc.GetSunrise(DateTime.Now, this._location);
            DateTime now = DateTime.Now;

            if (Properties.Settings.Default.IsSummerTime)
            {
                shkiah.Hour++;
                netz.Hour++;
            }

            int curHour = now.Hour;
            int curMin = now.Minute;

            bool isAfterNetz = curHour > netz.Hour || (curHour == netz.Hour && curMin > netz.Minute);
            bool isBeforeShkiah = (curHour < shkiah.Hour || (curHour == shkiah.Hour && curMin < shkiah.Minute));
            bool isNightTime = (!isAfterNetz) || (!isBeforeShkiah);
            bool isAfterMidnight = now.Hour < shkiah.Hour || (now.Hour == shkiah.Hour && now.Minute < shkiah.Minute);

            this._today = isNightTime && !isAfterMidnight ? now.AddDays(1) : now; //after shkia before midnight is tommorow in Jewish...
            this.rbDay.Checked = !isNightTime;
            this.rbNight.Checked = isNightTime;
            this._nowOnah = new Onah(_today, isNightTime ? DayNight.Night : DayNight.Day);

            string todayString = this._today.ToString("dd MMMM yyyy");
            foreach (string holiday in JewishHolidays.GetHebrewHolidays(this._today, Properties.Settings.Default.UserInIsrael))
            {
                todayString += " - " + holiday;
            }
            this.lblToday.Text = todayString;
        }

        private void SetSummerTime()
        {
            if (Properties.Settings.Default.UserInIsrael)
            {
                Properties.Settings.Default.IsSummerTime = TimesCalculation.IsIsraeliSummerTime(DateTime.Now);
            }
        }

        private string GetToolTipForDate(DateTime dateTime)
        {
            StringBuilder toolTip = new StringBuilder();
            foreach (string holiday in JewishHolidays.GetHebrewHolidays(dateTime, Properties.Settings.Default.UserInIsrael))
            {
                toolTip.AppendLine(holiday);
            }

            TimesCalculation tc = new TimesCalculation();
            AstronomicalTime shkiah = tc.GetSunset(dateTime, this._location);
            AstronomicalTime netz = tc.GetSunrise(dateTime, this._location);

            if (Properties.Settings.Default.IsSummerTime)
            {
                shkiah.Hour++;
                netz.Hour++;
            }
            toolTip.Append("שקיעה - ");
            toolTip.Append(shkiah.Hour.ToString());
            toolTip.Append(":");
            toolTip.Append(shkiah.Minute.ToString("0#"));
            toolTip.Append("\nנץ - ");
            toolTip.Append(netz.Hour.ToString());
            toolTip.Append(":");
            toolTip.Append(netz.Minute.ToString("0#"));
            return toolTip.ToString();
        }

        private void FillZmanim()
        {
            int day = this.cmbDay.SelectedIndex + 1;
            int month = ((MonthObject)cmbMonth.SelectedItem).MonthInYear;
            int year = (int)cmbYear.SelectedItem;

            DateTime selDate = new DateTime(year, month, day, _hc);

            TimesCalculation tc = new TimesCalculation();
            AstronomicalTime shkiah = tc.GetSunset(selDate, this._location);
            AstronomicalTime netz = tc.GetSunrise(selDate, this._location);

            if (Properties.Settings.Default.IsSummerTime)
            {
                shkiah.Hour++;
                netz.Hour++;
            }

            string sHoliday = null;
            foreach (string holiday in JewishHolidays.GetHebrewHolidays(selDate, Properties.Settings.Default.UserInIsrael))
            {
                sHoliday += holiday + " ";
            }

            if (sHoliday != null)
            {
                sHoliday = " - " + sHoliday;
            }

            lblLocation.Text = this._location.Name + " - " + selDate.ToString("dd MMM yyyy") + sHoliday;

            StringBuilder sb = new StringBuilder("נץ - ");
            sb.Append(netz.Hour.ToString());
            sb.Append(":");
            sb.Append(netz.Minute.ToString("0#"));
            sb.Append("\nשקיעה - ");
            sb.Append(shkiah.Hour.ToString());
            sb.Append(":");
            sb.Append(shkiah.Minute.ToString("0#"));

            lblZmanim.Text = sb.ToString();
        }

        private void SetLocation()
        {
            string locName = Properties.Settings.Default.UserLocation;
            XmlNode locNode = LocationsXmlDoc.SelectSingleNode("//Location[Name='" + locName + "']");

            this._location = new Location();

            this._location.Name = locName;
            this._location.LatitudeDegrees = Convert.ToInt32(locNode.SelectSingleNode("LatitudeDegrees").InnerText);
            this._location.LatitudeMinutes = Convert.ToInt32(locNode.SelectSingleNode("LatitudeMinutes").InnerText);
            this._location.LatitudeType = (locNode.SelectSingleNode("LatitudeType").InnerText == "N" ? LatitudeTypeEnum.North : LatitudeTypeEnum.South);
            this._location.LongitudeDegrees = Convert.ToInt32(locNode.SelectSingleNode("LongitudeDegrees").InnerText);
            this._location.LongitudeMinutes = Convert.ToInt32(locNode.SelectSingleNode("LongitudeMinutes").InnerText);
            this._location.LongitudeType = (locNode.SelectSingleNode("LongitudeType").InnerText == "E" ? LongitudeTypeEnum.East : LongitudeTypeEnum.West);
            this._location.TimeZone = Convert.ToInt32(locNode.SelectSingleNode("Timezone").InnerText);
            this._location.Elevation = Convert.ToInt32(locNode.SelectSingleNode("Elevation").InnerText);
        }

        private string GetPassword()
        {
            RegistryKey regKey = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("Chashavshavon");
            if (regKey == null)
            {
                regKey = Registry.CurrentUser.OpenSubKey("Software", true).CreateSubKey("Chashavshavon", RegistryKeyPermissionCheck.ReadWriteSubTree);
                regKey.SetValue("Entry", "", RegistryValueKind.String);
                regKey.SetValue("Straight", "1", RegistryValueKind.String);
            }
            string password = regKey.GetValue("Straight").ToString() == "1" ? null : regKey.GetValue("Entry").ToString();
            regKey.Close();
            return password;
        }        

        #endregion

        #region Public Functions
        public bool TestInternet()
        {
            bool hasInternet = Properties.Settings.Default.UseLocalURL || Program.IsConnectedToInternet();
            toolStripSeparator1.Visible = hasInternet;
            RemoteToolStripMenuItem.Visible = hasInternet;
            if (CurrentFileIsRemote && !hasInternet)
            {
                MessageBox.Show("הקובץ שאמור ליפתח" + Environment.NewLine + "\"" + CurrentFile + "\"" + Environment.NewLine + "הוא קובץ רשת, אבל אין גישה לרשת כרגע" + Environment.NewLine + ".לכן תפתח קובץ ריק", "חשבשבון", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
                CurrentFileIsRemote = false;
                CurrentFile = DateTime.Now.ToString("ddMMMyyyy_hhmm").Replace("\"", "").Replace("'", "") + ".pm";
            }
            return hasInternet;
        }

        public void LoadXmlFile()
        {
            Entries.Clear();
            XmlDocument xml = new XmlDocument();

            if (CurrentFileIsRemote || File.Exists(CurrentFile))
            {
                xml.LoadXml(this.CurrentFileXML);
            }

            if (xml.HasChildNodes)
            {
                foreach (XmlNode entryNode in xml.SelectNodes("//Entry"))
                {
                    int day = Convert.ToInt32(entryNode.SelectSingleNode("Day").InnerText);
                    int month = Convert.ToInt32(entryNode.SelectSingleNode("Month").InnerText);
                    int year = Convert.ToInt32(entryNode.SelectSingleNode("Year").InnerText); ;
                    DayNight dayNight = (DayNight)Convert.ToInt32(entryNode.SelectSingleNode("DN").InnerText);
                    string notes = entryNode.SelectSingleNode("Notes").InnerText; ;

                    Entry newEntry = new Entry(day, month, year, dayNight, notes);

                    Entries.Add(newEntry);
                }
                if (xml.SelectNodes("//Kavuah").Count > 0)
                {
                    var ser = new XmlSerializer(typeof(List<Kavuah>));
                    Kavuah.KavuahsList = (List<Kavuah>)ser.Deserialize(
                        new StringReader(xml.SelectSingleNode("//ArrayOfKavuah").OuterXml));
                }
                else
                {
                    Kavuah.KavuahsList = new List<Kavuah>();
                }
            }

            this.SetCaption();
            this.SortEntriesAndSetInterval();
            this.FillCalendar();
            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = Entries;
            this.dgEntries.DataSource = bindingSource;
        }

        public void SetCaption()
        {
            this.Text = "חשבשבון - " + this._location.Name + " - " + (this.CurrentFileIsRemote ? "קובצי רשת - " : "") + this.CurrentFileName;
            this.pbWeb.Visible = this.CurrentFileIsRemote;
        }

        public void AfterChangePreferences()
        {
            this.SetLocation();
            this.SetDateAndDayNight();
            this.FillCalendar();
            this.FillZmanim();
            this.CalculateCalendar();
            this.SetCaption();
        }
        #endregion

        #region Properties
        public bool CloseMeFirst { get; set; }
        public string WeekListText { get; set; }
        public string CurrentFile
        {
            get { return Properties.Settings.Default.CurrentFile; }
            set
            {
                Properties.Settings.Default.CurrentFile = value;
                Properties.Settings.Default.Save();
                this.SetCaption();
            }
        }

        public bool CurrentFileIsRemote
        {
            get { return Properties.Settings.Default.IsCurrentFileRemote; }
            set
            {
                Properties.Settings.Default.IsCurrentFileRemote = value;
                Properties.Settings.Default.Save();
                this.SetCaption();
            }
        }

        public string CurrentFileXML
        {
            get
            {
                string xml = null;
                if (this.CurrentFileIsRemote)
                {
                    try
                    {
                        xml = Utils.RemoteFunctions.GetCurrentFileText(this.CurrentFile);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message, "חשבשבון", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    xml = File.ReadAllText(this.CurrentFile);
                }

                return (string.IsNullOrEmpty(xml) ? "<Entries />" : xml);
            }
        }

        public string CurrentFileName
        {
            get
            {
                string[] cf = CurrentFile.Split(new char[] { '\\', '/' });
                return cf[cf.Length - 1];
            }

        }
        #endregion                
    }
}
