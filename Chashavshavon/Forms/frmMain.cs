﻿using Chashavshavon.Utils;
using JewishCalendar;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace Chashavshavon
{
    public partial class frmMain : Form
    {
        #region Private Variables

        private DateTime _monthToDisplay;
        private string _tempXMLFileName = Program.TempFolderPath + @"\ChashavshavonTempFile.xml";

        #endregion Private Variables

        #region Constructors

        public frmMain()
        {
            this.StartUp();
        }

        public frmMain(string filePath)
        {
            this.CurrentFileIsRemote = false;
            this.CurrentFile = filePath;
            this.StartUp();
        }

        #endregion Constructors

        #region Event Handlers

        private void AbouToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (AboutBox ab = new AboutBox())
            {
                ab.ShowDialog(this);
            }
        }

        private void AddKavuahToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.AddNewKavuah(this);
        }

        private void AddNewEntry(object sender, EventArgs e)
        {
            using (frmAddNewEntry f = new frmAddNewEntry((DateTime)((Control)sender).Tag))
            {
                if (f.ShowDialog(this) == DialogResult.OK)
                {
                    //Refresh in case of change to current month
                    this.DisplayMonth();
                }
            }
        }

        private void btnAddEntry_Click(object sender, EventArgs e)
        {
            frmAddNewEntry f = new frmAddNewEntry(Program.Today);
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                //Refresh in case of change to current month
                this.DisplayMonth();
            }
        }

        private void btnAddKavuah_Click(object sender, EventArgs e)
        {
            this.AddNewKavuah(this);
        }

        private void btnCheshbonKavuahs_Click(object sender, EventArgs e)
        {
            if (Kavuah.FindAndPromptKavuahs())
            {
                //Save the new Kavuahs to the file
                this.SaveCurrentFile();
                //In case there were changes to the notes on some entries such as if there was a NoKavuah added
                this.bindingSourceEntries.DataSource = Entry.EntryList.Where(en => !en.IsInvisible);
            }
            else
            {
                MessageBox.Show("לא נמצאו וסת קבוע אפשריים");
            }
        }

        private void btnLastMonth_Click(object sender, EventArgs e)
        {
            this._monthToDisplay = Program.HebrewCalendar.AddMonths(this._monthToDisplay, -1);
            this.DisplayMonth();
        }

        private void btnNextMonth_Click(object sender, EventArgs e)
        {
            this._monthToDisplay = Program.HebrewCalendar.AddMonths(this._monthToDisplay, 1);
            this.DisplayMonth();
        }

        private void btnOpenKavuahs_Click(object sender, EventArgs e)
        {
            this.ShowKavuahList();
        }

        private void btnPrefs_Click(object sender, EventArgs e)
        {
            frmPreferences prefs = new frmPreferences();
            prefs.Show(this);
        }

        private void btnPrintCalendar_Click(object sender, EventArgs e)
        {
            this.PrintCalendarList();
        }

        private void btnPrintEntryList_Click(object sender, EventArgs e)
        {
            this.PrintEntryList();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void btnToday_Click(object sender, EventArgs e)
        {
            this._monthToDisplay = Program.Today;
            this.DisplayMonth();
        }

        private void btnViewTextCalendar_Click(object sender, EventArgs e)
        {
            this.ShowCalendarTextList();
        }

        private void btnViewTextEntryList_Click(object sender, EventArgs e)
        {
            this.ShowEntryTextList();
        }

        private void clearRecentFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.RecentFiles.Clear();
            this.recentFilesToolStripMenuItem.DropDownItems.Clear();
            this.recentFilesToolStripMenuItem.Enabled = this.clearRecentFilesToolStripMenuItem.Enabled = false;
        }

        private void dgEntries_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgEntries.Columns[e.ColumnIndex] == btnDeleteColumn &&
                dgEntries.Rows[e.RowIndex].DataBoundItem is Entry)
            {
                this.DeleteEntry((Entry)dgEntries.Rows[e.RowIndex].DataBoundItem);
            }
        }

        private void dgEntries_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == this.dgEntries.Columns["NotesColumn"].Index)
            {
                DataGridViewRow r = this.dgEntries.Rows[e.RowIndex];
                if (r.DataBoundItem is Entry entry)
                {
                    string nkText = "";
                    foreach (Kavuah nk in entry.NoKavuahList)
                    {
                        nkText += " לא לרשום קבוע " + nk.ToString();
                    }
                    if (nkText.Length > 0)
                    {
                        e.Value += " [" + nkText.Trim() + "]";
                    }
                }
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.SaveCurrentFile();
                if (!Properties.Settings.Default.OpenLastFile)
                {
                    Properties.Settings.Default.CurrentFile = null;
                    Properties.Settings.Default.IsCurrentFileRemote = false;
                }

                //the temp folder is only deleted if the user manually closed the app.
                //Otherwise we may be in an installer run and do not want to delete the installation files.
                Program.BeforeExit(e.CloseReason == CloseReason.UserClosing);
            }
            catch (Exception ex)
            {
                MessageBox.Show("ארעה שגיעה.\nיתכן שלא נשמר הפעולות אחרונות שנעשתה בתוכנה.\nפרטי השגיעה רשומים למטה.\n---------------------------------------------\n."
                    + ex.Message,
                                              "חשבשבון",
                                              MessageBoxButtons.OK,
                                              MessageBoxIcon.Error,
                                              MessageBoxDefaultButton.Button1,
                                              MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            if (this.CloseMeFirst)
            {
                this.Close();
            }

            dgEntries.AutoGenerateColumns = false;

            //Checks to see if the user is connected to the internet, and activates remote functionality if they are.
            this.TestInternet();
            //Load the last opened file. If it does not exist or this is the first run, a blank list is presented
            this.LoadXmlFile();
            this.bindingSourceEntries.DataSource = Entry.EntryList.Where(en => !en.IsInvisible);
            this._monthToDisplay = Program.Today;
            this.DisplayMonth();

            foreach (string f in Properties.Settings.Default.RecentFiles)
            {
                recentFilesToolStripMenuItem.DropDownItems.Add(f);
            }
            this.recentFilesToolStripMenuItem.Enabled = this.clearRecentFilesToolStripMenuItem.Enabled = recentFilesToolStripMenuItem.HasDropDownItems;
        }

        private void frmMain_ResizeBegin(object sender, EventArgs e)
        {
            this.luachTableLayout.Visible = false;
            this.luachTableLayout.SuspendLayout();
        }

        private void frmMain_ResizeEnd(object sender, EventArgs e)
        {
            this.luachTableLayout.ResumeLayout();
            this.luachTableLayout.Visible = true;
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SaveCurrentFile(); //why not...
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(this.openFileDialog1.FileName);
                    if (doc.SelectNodes("//Kavuah").Count > 0)
                    {
                        try
                        {
                            var ser = new XmlSerializer(typeof(List<Kavuah>));
                            var list = (List<Kavuah>)ser.Deserialize(
                                new StringReader(doc.SelectSingleNode("//ArrayOfKavuah").OuterXml));

                            Kavuah.KavuahsList.AddRange(list);

                            /* If the setting entry of the Kavuah is not contained
                            * on the current list, than frmKavuah will cause an error
                            * as it uses the current EntryList as a DataSource for a drop-down
                            * for the SettingEntry field and it sets its displayed value for each Kavuah
                            * as the settingEntryDate of the Kavuah. So each Kavuah in the list
                            * needs a corresponding Entry in the EntryList.
                            * This is the scenario that prompted the whole IsInvisible property of the
                            * Entry class. */
                            foreach (Kavuah kav in list)
                            {
                                if (!Entry.EntryList.Exists(en =>
                                    en.DateTime == kav.SettingEntryDate &&
                                    en.DayNight == kav.DayNight &&
                                    en.Interval == kav.SettingEntryInterval))
                                {
                                    Entry.EntryList.Add(new Entry(
                                        Program.HebrewCalendar.GetDayOfMonth(kav.SettingEntryDate),
                                        Program.HebrewCalendar.GetMonth(kav.SettingEntryDate),
                                        Program.HebrewCalendar.GetYear(kav.SettingEntryDate),
                                        kav.DayNight,
                                        null)
                                    {
                                        IsInvisible = true
                                    });
                                }
                            }
                            //Clean out any overlappers
                            Kavuah.ClearDoubleKavuahs(Kavuah.KavuahsList);

                            this.CalculateProblemOnahs();
                            this.DisplayMonth();
                            this.ShowKavuahList();
                        }
                        catch
                        {
                            MessageBox.Show("רשימת וסת קבוע בקובץ\"" +
                                Path.GetFileName(this.openFileDialog1.FileName) + "\" .איננה תקינה",
                                "חשבשבון",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation,
                                MessageBoxDefaultButton.Button1,
                                MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
                        }
                    }
                    else
                    {
                        MessageBox.Show("רשימת וסת קבוע בקובץ\"" +
                            Path.GetFileName(this.openFileDialog1.FileName) + "\" ריקה.",
                            "חשבשבון",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information,
                            MessageBoxDefaultButton.Button1,
                            MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateNewFile();
        }

        private void OpenBackupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Program.BackupFolderPath);
        }

        private void openKavuaListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowKavuahList();
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SaveCurrentFile();
            openFileDialog1.ShowDialog(this);
            openFileDialog1.CheckFileExists = true;
            CurrentFile = openFileDialog1.FileName;
            CurrentFileIsRemote = false;
            LoadXmlFile();
            this.CalculateProblemOnahs();
            DisplayMonth();
        }

        private void pbWeb_Click(object sender, EventArgs e)
        {
            frmRemoteFiles f = new frmRemoteFiles();
            f.Show(this);
        }

        private void PreferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPreferences prefs = new frmPreferences();
            prefs.Show(this);
        }

        private void PrintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintCalendarList();
        }

        private void recentFilesToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (File.Exists(e.ClickedItem.Text))
            {
                this.SaveCurrentFile();
                CurrentFile = e.ClickedItem.Text;
                CurrentFileIsRemote = false;
                LoadXmlFile();
                this.CalculateProblemOnahs();
                DisplayMonth();
            }
            else
            {
                this.recentFilesToolStripMenuItem.HideDropDown(); // was blocking message box
                if (MessageBox.Show("הקובץ \"" + e.ClickedItem.Text +
                        "\" לא נמצא.\nלהסירה מרשימת קבצים אחרונים?",
                     "חשבשבון",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading) == DialogResult.Yes)
                {
                    Properties.Settings.Default.RecentFiles.Remove(e.ClickedItem.Text);
                    recentFilesToolStripMenuItem.DropDownItems.Remove(e.ClickedItem);
                    this.recentFilesToolStripMenuItem.Enabled = this.clearRecentFilesToolStripMenuItem.Enabled = recentFilesToolStripMenuItem.HasDropDownItems;
                }
            }
        }

        private void RefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.RefreshData();
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SaveAs();
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SaveCurrentFile();
            MessageBox.Show("הקובץ נשמרה" + (CurrentFileIsRemote ? " ברשת " : ""),
                            "חשבשבון",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
        }

        private void SearchForKavuahsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Kavuah.FindAndPromptKavuahs())
            {
                //Save the new Kavuahs to the file
                this.SaveCurrentFile();
                //In case there were changes to the notes on some entries such as if there was a NoKavuah added
                this.bindingSourceEntries.DataSource = Entry.EntryList.Where(en => !en.IsInvisible);
            }
            else
            {
                MessageBox.Show("לא נמצאו וסת קבוע אפשריים");
            }
        }

        private void SourceTextMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.CurrentFile) && File.Exists(CurrentFile))
            {
                SaveCurrentFile();

                var notepad = new System.Diagnostics.Process();
                int progThreadId = System.Threading.Thread.CurrentThread.ManagedThreadId;

                notepad.StartInfo.FileName = "notepad.exe";
                notepad.StartInfo.Arguments = this.CurrentFile;
                notepad.EnableRaisingEvents = true;
                notepad.Exited += delegate
                {
                    //sometimes this is called twice - once in a different thread...
                    if (progThreadId == System.Threading.Thread.CurrentThread.ManagedThreadId)
                    {
                        this.RefreshData();
                    }
                };
                notepad.Start();
                notepad.WaitForExit();
                notepad.Dispose();
            }
            else
            {
                MessageBox.Show(".הצגת מקור מצריך קובץ",
                        "חשבשבון",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
            }
        }

        private void SourceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.CurrentFile))
            {
                SaveCurrentFile();
                this.GetTempXmlFile();
                System.Diagnostics.Process.Start(this._tempXMLFileName);
            }
            else
            {
                MessageBox.Show(".הצגת מקור מצריך קובץ",
                        "חשבשבון",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
            }
        }

        private void TextListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowCalendarTextList();
        }

        private void toolStripMenuItemEntryList_Click(object sender, EventArgs e)
        {
            this.ShowEntryTextList();
        }

        private void toolStripMenuItemPrintEntryList_Click(object sender, EventArgs e)
        {
            this.PrintEntryList();
        }

        private void toolStripMenuItemRemote_Click(object sender, EventArgs e)
        {
            frmRemoteFiles f = new frmRemoteFiles();
            f.Show(this);
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ImportEntriesStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.CheckFileExists = false;
            openFileDialog1.DefaultExt = "pm";
            if (openFileDialog1.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }
            using (var i = new frmImport(openFileDialog1.FileName))
            {
                var added = false;
                if (i.ShowDialog() == DialogResult.OK)
                {
                    var (entries, kavuahs) = (i.EntryList, i.KavuahList);
                    foreach (var entry in entries)
                    {
                        if (!Entry.EntryList.Exists(o => Onah.IsSimilarOnah(o, entry)))
                        {
                            Entry.EntryList.Add(entry);
                            added = true;
                        }
                    }
                    foreach (var kavuah in kavuahs)
                    {
                        if (!Kavuah.KavuahsList.Exists(o => Kavuah.IsSameKavuah(o, kavuah)))
                        {
                            Kavuah.KavuahsList.Add(kavuah);
                            added = true;
                        }
                    }
                    if (added)
                    {
                        this.SaveCurrentFile();
                        this.RefreshData();
                    }
                }

            }
        }
        #endregion Event Handlers

        #region Private Functions

        private void CreateNewFile()
        {
            if (!string.IsNullOrEmpty(this.CurrentFile))
            {
                this.SaveCurrentFile();
            }
            saveFileDialog1.Title = "נא לבחור שם ומיקום לקובץ החדש";
            saveFileDialog1.CreatePrompt = true;
            saveFileDialog1.CheckFileExists = false;
            saveFileDialog1.OverwritePrompt = true;
            saveFileDialog1.DefaultExt = "pm";
            saveFileDialog1.FileName = Program.Today.ToString("ddMMMyyyy").Replace("\"", "").Replace("'", "") + ".pm";

            if (saveFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                CurrentFile = saveFileDialog1.FileName;
                CurrentFileIsRemote = false;
                this.RefreshData();
            }
        }

        private void SaveAs(Form sourceForm = null)
        {
            openFileDialog1.CheckFileExists = false;
            openFileDialog1.DefaultExt = "pm";
            openFileDialog1.FileName = Program.Today.ToString("ddMMMyyyy").Replace("\"", "").Replace("'", "") + ".pm";
            if (openFileDialog1.ShowDialog(sourceForm ?? this) != DialogResult.OK)
            {
                return;
            }
            CurrentFile = openFileDialog1.FileName;
            CurrentFileIsRemote = false;

            SaveCurrentFile();
            return;
        }

        private void ShowKavuahList()
        {
            using (frmKavuahs f = new frmKavuahs())
            {
                if (f.ShowDialog(this) != DialogResult.Cancel)
                {
                    this.SaveCurrentFile();
                    this.TestInternet();
                    this.LoadXmlFile();
                }

                //In case some Kavuahs were added or deleted...
                this.CalculateProblemOnahs();
                this.DisplayMonth();
            }
        }

        private void StartUp()
        {
            //In case we will display the password entry form, we hide this until form load
            this.Hide();
            string password = this.GetPassword();

            InitializeComponent();

            //The following sets all output displays of date time functions to Jewish dates for the current thread
            Program.CultureInfo.DateTimeFormat.Calendar = Program.HebrewCalendar;
            System.Threading.Thread.CurrentThread.CurrentCulture = Program.CultureInfo;
            System.Threading.Thread.CurrentThread.CurrentUICulture = Program.CultureInfo;

            this.SetLocation();
            this.SetDateAndDayNight();

            if (password == null)
            {
                this.Show();
            }
            else
            {
                //Prompt for a password and don't stop prompting until the user gets it right or gives up
                using (frmEnterPassword f = new frmEnterPassword(password))
                {
                    do
                    {
                        f.ShowDialog(this);
                        if (f.DialogResult == DialogResult.No)
                        {
                            MessageBox.Show("סיסמה שגויה",
                            "חשבשבון",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        }
                    }
                    while (f.DialogResult == DialogResult.No);
                    //If the user canceled etc. we will close this in form load
                    this.CloseMeFirst = f.DialogResult != DialogResult.Yes;
                }
            }
        }

        private void DisplayMonth()
        {
            int year = Program.HebrewCalendar.GetYear(this._monthToDisplay);
            MonthObject month = new MonthObject(year, Program.HebrewCalendar.GetMonth(this._monthToDisplay));
            DateTime firstDayOfMonth = this._monthToDisplay.AddDays(1 - Program.HebrewCalendar.GetDayOfMonth(this._monthToDisplay));
            int firstDayOfWeek = 1 + (int)firstDayOfMonth.DayOfWeek;
            int currentRow = 1, currentColumn = firstDayOfWeek - 1;
            var smallFont = new Font(Font.FontFamily, 6f);
            var sysCulture = CultureInfo.InstalledUICulture.DateTimeFormat;

            this.luachTableLayout.Visible = false;
            this.luachTableLayout.SuspendLayout();

            foreach (Control c in this.luachTableLayout.Controls)
            {
                c.Dispose();
            }
            this.luachTableLayout.Controls.Clear();

            this.lblMonthName.Text = this._monthToDisplay.ToString("MMM yyyy", Program.CultureInfo);
            this.btnLastMonth.Text = "  " + Program.HebrewCalendar.AddMonths(this._monthToDisplay, -1).ToString("MMM") + "  ";
            this.btnNextMonth.Text = "  " + Program.HebrewCalendar.AddMonths(this._monthToDisplay, 1).ToString("MMM") + "  ";

            if (((int)firstDayOfMonth.DayOfWeek >= 6) &&
                   Program.HebrewCalendar.GetDaysInMonth(year, month.MonthInYear)
                > 29)
            {
                this.luachTableLayout.RowCount = 7;
                this.luachTableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 16.66667F));
                foreach (RowStyle rs in this.luachTableLayout.RowStyles)
                {
                    if (rs.SizeType == SizeType.Percent)
                    {
                        rs.Height = 16.66667F;
                    }
                }
            }
            else
            {
                this.luachTableLayout.RowCount = 6;
            }

            for (int i = 0; i < 7; i++)
            {
                Panel dow = new Panel()
                {
                    BackgroundImage = Properties.Resources.DarkBlueMarbleBar,
                    BackgroundImageLayout = ImageLayout.Tile,
                    Margin = new Padding(0),
                    Height = 20,
                    Padding = new Padding(0),
                    Dock = DockStyle.Fill
                };
                dow.Controls.Add(new Label()
                {
                    Text = Chashavshavon.Utils.Zmanim.DaysOfWeekHebrewFull[i],
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = Color.FromArgb(255, 225, 228, 230),
                    BackColor = Color.Transparent,
                    Font = new Font("Narkisim", 12f)
                });
                this.luachTableLayout.Controls.Add(dow, i, 0);
            }

            for (int i = 1; i < month.DaysInMonth + 1; i++)
            {
                DateTime date = new DateTime(Program.HebrewCalendar.GetYear(this._monthToDisplay),
                    Program.HebrewCalendar.GetMonth(this._monthToDisplay),
                    i,
                    Program.HebrewCalendar);

                Panel pnl = new TableLayoutPanel()
                {
                    Dock = DockStyle.Fill,
                    Margin = new Padding(0),
                    BackColor = Color.Transparent
                };
                Image dayBgImgMainOrLeft = Properties.Resources.WhiteMarble;
                Image dayBgImgRight = null;

                this.luachTableLayout.Controls.Add(pnl, currentColumn, currentRow);

                var itlp = new TableLayoutPanel()
                {
                    ColumnCount = 2,
                    RowCount = 1,
                    Height = pnl.Height / 4,
                    Dock = DockStyle.Top,
                    Margin = new Padding(0),
                    Padding = new Padding(0)
                };

                itlp.Controls.Add(new Label()
                {
                    Dock = DockStyle.Fill,
                    AutoSize = true,
                    Font = new Font("Verdana", 18f, FontStyle.Bold),
                    ForeColor = Color.Maroon,
                    Text = Chashavshavon.Utils.Zmanim.DaysOfMonthHebrew[i].Replace("\"", "").Replace("\'", ""),
                });

                itlp.Controls.Add(new Label()
                {
                    Dock = DockStyle.Fill,
                    AutoSize = true,
                    TextAlign = ContentAlignment.TopRight,
                    Font = new Font("Verdana", 8f, FontStyle.Bold),
                    ForeColor = Color.DarkSlateBlue,
                    Text = date.ToString("%d", sysCulture)
                });

                pnl.Controls.Add(itlp);

                string daySpecialText = "";
                bool isInIsrael = Program.CurrentLocation.IsInIsrael;
                var jd = new JewishDate(date);
                var holidays = JewishCalendar.Zmanim.GetHolidays(jd, isInIsrael).Cast<SpecialDay>();
                if (jd.DayOfWeek == DayOfWeek.Saturday)
                {
                    if (!((jd.Month == 7 && jd.Day.In(1, 2, 10, 15, 16, 17, 18, 19, 20, 21, 22, (isInIsrael ? 0 : 23))) ||
                        (jd.Month == 1 && jd.Day.In(15, 16, 17, 18, 19, 20, 21, (isInIsrael ? 0 : 22)) ||
                        (jd.Month == 3 && jd.Day.In(6, (isInIsrael ? 0 : 7))))))
                    {
                        var parshas = Sedra.GetSedra(jd, isInIsrael);
                        daySpecialText = "שבת פרשת " + string.Join(" - ", parshas.Select(p => p.nameHebrew)) + Environment.NewLine;
                    }
                }
                if (holidays.Any(h => h.DayType.IsSpecialDayType(SpecialDayTypes.HasCandleLighting)) &&
                    !holidays.Any(h => h.DayType.IsSpecialDayType(SpecialDayTypes.MajorYomTov)))
                {
                    var shkiah = JewishCalendar.Zmanim.GetShkia(date, Program.CurrentLocation);
                    shkiah -= Program.CurrentLocation.CandleLighting;
                    daySpecialText += "הדלק\"נ: " +
                        shkiah.ToString(true, false, false) + Environment.NewLine;
                }

                daySpecialText += JewishCalendar.Zmanim.GetHolidaysText(
                    holidays.Where(h => h.NameHebrew != "ערב שבת").ToArray(),
                    Environment.NewLine, true);

                if (!string.IsNullOrWhiteSpace(daySpecialText))
                {
                    pnl.Controls.Add(new Label()
                    {
                        AutoSize = true,
                        Anchor = AnchorStyles.Top,
                        Text = daySpecialText,
                        Font = smallFont,
                        ForeColor = Color.DarkSlateGray,
                        TextAlign = ContentAlignment.TopCenter,
                        RightToLeft = RightToLeft.Yes,
                        Margin = new Padding(3),
                        AutoEllipsis = true
                    });
                }

                if (date.DayOfWeek == DayOfWeek.Saturday || holidays.Any(h =>
                 h.DayType.IsSpecialDayType(SpecialDayTypes.MajorYomTov)))
                {
                    dayBgImgMainOrLeft = Properties.Resources.BlueMarble;
                }
                else if (
                    holidays.Any(h => h.DayType.IsSpecialDayType(SpecialDayTypes.MinorYomtov)))
                {
                    dayBgImgMainOrLeft = Properties.Resources.LightBlueMarble;
                }

                string onahText = "";
                bool hasDayOnah = false;
                bool hasNightOnah = false;
                if (ProblemOnahs.ProblemOnahList != null)
                {
                    foreach (var o in ProblemOnahs.ProblemOnahList.Where(o => o.DateTime == date && !o.IsIgnored))
                    {
                        if (!string.IsNullOrWhiteSpace(onahText))
                        {
                            onahText += Environment.NewLine;
                        }
                        if (o.DayNight == DayNight.Day)
                        {
                            hasDayOnah = true;
                        }
                        else if (o.DayNight == DayNight.Night)
                        {
                            hasNightOnah = true;
                        }
                        onahText += o.HebrewDayNight + ": " + o.Name;
                    }
                }

                Entry entry = Entry.EntryList.FirstOrDefault(en =>
                    !en.IsInvisible &&
                    en.DateTime == date);
                if (entry != null)
                {
                    string entryText = "ראיה - עונת " + entry.HebrewDayNight;
                    if (entry.Interval > 0)
                    {
                        entryText += Environment.NewLine + " הפלגה: " + entry.Interval.ToString();
                    }
                    if (entry.DayNight == DayNight.Day)
                    {
                        dayBgImgRight = dayBgImgMainOrLeft;
                        dayBgImgMainOrLeft = Properties.Resources.PinkMarbleTile;
                    }
                    else
                    {
                        dayBgImgRight = Properties.Resources.PinkMarbleTile;
                    }
                    pnl.Controls.Add(new Label()
                    {
                        AutoSize = true,
                        Anchor = AnchorStyles.Top,
                        Text = entryText,
                        ForeColor = Color.DarkRed,
                        Font = smallFont,
                        TextAlign = ContentAlignment.BottomCenter,
                        RightToLeft = RightToLeft.Yes
                    });
                }
                else if (!string.IsNullOrWhiteSpace(onahText))
                {
                    if (hasNightOnah)
                    {
                        dayBgImgRight = Properties.Resources.ParchmentMarbleTile;
                    }
                    if (hasDayOnah)
                    {
                        if (dayBgImgRight == null)
                        {
                            dayBgImgRight = dayBgImgMainOrLeft;
                        }
                        dayBgImgMainOrLeft = Properties.Resources.ParchmentMarbleTile;
                    }


                    pnl.Controls.Add(new Label()
                    {
                        AutoSize = true,
                        Anchor = AnchorStyles.Top,
                        Text = onahText,
                        Font = smallFont,
                        ForeColor = Color.FromArgb(0x80, 0x50, 0),
                        TextAlign = ContentAlignment.BottomCenter,
                        RightToLeft = RightToLeft.Yes,
                        AutoEllipsis = true
                    });
                }

                pnl.Paint += delegate (object sender, PaintEventArgs e)
                {
                    using (Graphics g = e.Graphics)
                    {
                        int pWidth = pnl.DisplayRectangle.Width;
                        int pHeight = pnl.DisplayRectangle.Height;

                        if (dayBgImgRight == null)
                        {
                            g.DrawImage(dayBgImgMainOrLeft, 0, 0, pWidth, pHeight);
                        }
                        else
                        {
                            float halfX = pWidth / 2f;
                            g.DrawImage(dayBgImgMainOrLeft, 0, 0, halfX, pHeight);
                            g.DrawImage(dayBgImgRight, halfX, 0, halfX, pHeight);
                        }
                        if (Program.Today.IsSameday(date))
                        {
                            SolidBrush solidBrush = new SolidBrush(Color.FromArgb(50, Color.DarkSlateBlue));
                            var eh = pHeight / 3.3f;
                            var ew = pWidth / 3.3f;
                            g.FillClosedCurve(solidBrush, new PointF[]
                            {
                            new PointF(ew, eh),
                            new PointF((pWidth - ew), eh),
                            new PointF((pWidth - ew), (pHeight - eh)),
                            new PointF(ew, (pHeight - eh))
                            }, System.Drawing.Drawing2D.FillMode.Alternate, 3f);
                        }
                    }
                };


                string toolTipText = date.ToLongDateString() +
                    Environment.NewLine +
                    date.ToString("D", sysCulture) +
                    (!string.IsNullOrWhiteSpace(onahText) ?
                        Environment.NewLine + "--------------------------------" + Environment.NewLine + onahText : "");

                //Sets the tag, tooltip and click function for all controls of the day
                foreach (Control cntrl in Utils.GeneralUtils.GetAllControls(this.luachTableLayout.GetControlFromPosition(currentColumn, currentRow)))
                {
                    cntrl.Tag = date;
                    cntrl.Click += new EventHandler(AddNewEntry);
                    this.toolTip1.SetToolTip(cntrl, toolTipText);
                }

                if (currentColumn == luachTableLayout.ColumnCount - 1)
                {
                    currentColumn = 0;
                    currentRow++;
                }
                else
                {
                    currentColumn++;
                }
            }
            this.luachTableLayout.ResumeLayout();
            this.luachTableLayout.Visible = true;
        }

        private void CreateLocalBackup()
        {
            var bgw = new System.ComponentModel.BackgroundWorker();
            bgw.DoWork += delegate
            {
                if (File.Exists(this.CurrentFile))
                {
                    string path = Program.BackupFolderPath + "\\" +
                        Path.GetFileNameWithoutExtension(this.CurrentFile) +
                        "_" +
                        DateTime.Now.ToString("d-MMM-yy_HH-mm-ss", CultureInfo.GetCultureInfo("en-us").DateTimeFormat) +
                        ".pm";
                    if (File.Exists(path))
                    {
                        path = path.Replace(".pm", "_Version_1" + DateTime.Now.Millisecond.ToString() + ".pm");
                    }
                    File.Copy(this.CurrentFile, path);
                }
            };
            bgw.RunWorkerAsync();
        }

        private string GetEntryListText()
        {
            var sb = new StringBuilder("<html><head><meta content='text/html;charset=UTF-8;' />" +
                "<title>רשימת וסתות</title>" +
                "<style>body,table,td{text-align:right;direction:rtl;font-family:Narkisim;}" +
                "table{border:solid 1px silver;width:100%;}" +
                "td{padding:3px;margin:1px;}tr.alt td{background-color:#f1f1f1;}</style></head>");
            sb.AppendFormat("<body><h3>רשימת וסתות - {0}</h3><table>", Program.Today.ToLongDateString());
            sb.Append("<tr><th>מספר</th><th>תאריך</th><th>יום/לילה</th><th>הפלגה</th><th>הערות</th></tr>");
            int count = 0;
            foreach (Entry e in Entry.EntryList.Where(en => !en.IsInvisible))
            {
                sb.AppendFormat("<tr{0}><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td width='50%'>{5}</td></tr>",
                    (count++ % 2 == 0 ? " class='alt'" : ""),
                    count,
                    e.DateTime.ToLongDateString(),
                    e.HebrewDayNight,
                    e.Interval,
                    (string.IsNullOrEmpty(e.Notes) ? "&nbsp;" : e.Notes));
            }
            sb.Append("</table></body></html>");
            return sb.ToString();
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

        private void PrintCalendarList()
        {
            this.ShowCalendarTextList(true);
        }

        private void PrintEntryList()
        {
            this.ShowEntryTextList(true);
        }

        private void ProccessProblem(Label lbl, Label lblDate, Onah problemOnah)
        {
            lbl.Text = lbl.Text.Trim();
            if (problemOnah.IsIgnored)
            {
                lbl.Text += string.Format("{0}[{1}]",
                    (lbl.Text.Length > 0 ? Environment.NewLine : ""),
                    problemOnah.Name);
            }
            else
            {
                lbl.Text += (lbl.Text.Length > 0 ? Environment.NewLine : "") + problemOnah.Name;
            }

            this.toolTip1.SetToolTip(lbl, lbl.Text);

            //If this onah is to be ignored and the same onah doesn't have another non-ignoreable problem

            lblDate.BackColor = Color.SlateGray;
            lblDate.ForeColor = Color.Lavender;
            if (lbl.Name.Contains("Today") && Program.NowOnah.DayNight == problemOnah.DayNight)
            {
                lbl.BackColor = Color.Red;
                lbl.ForeColor = Color.Lavender;
            }
            else
            {
                lbl.BackColor = Color.Lavender;
            }
        }

        private void RefreshData()
        {
            this.TestInternet();
            this.LoadXmlFile();
            this.DisplayMonth();
        }

        private void SetDateAndDayNight()
        {
            DateTime date = DateTime.Now;
            var zman = new JewishCalendar.Zmanim(date, Program.CurrentLocation);
            var isNight = zman.GetShkia() <= date.TimeOfDay;
            if (isNight)
            {
                date = date.AddDays(1);
            }
            Program.Today = date;
            Program.NowOnah = new Onah(Program.Today, isNight ? DayNight.Night : DayNight.Day);
        }

        private void SetLocation()
        {
            string location = Properties.Settings.Default.LocationName;
            if (string.IsNullOrWhiteSpace(location))
            {
                location = Properties.Settings.Default.LocationName = "Jerusalem"; //Yerushalayim
            }

            Program.CurrentLocation = Utils.Locations.GetPlace(location);
        }

        private frmBrowser ShowCalendarTextList(bool print = false)
        {
            var fb = new frmBrowser(print)
            {
                Text = "לוח חשבשבון_" + Program.Today.ToString("dd_MMM_yyyy").Replace("'", "").Replace("\"", ""),
                Html = this.WeekListHtml
            };
            fb.Show();
            return fb;
        }

        private void ShowDayDetails(DateTime dateTime)
        {
            using (frmAddNewEntry f = new frmAddNewEntry(dateTime))
            {
                f.ShowDialog(this);
            }
        }

        private frmBrowser ShowEntryTextList(bool print = false)
        {
            var fb = new frmBrowser(print)
            {
                Text = "רשימת חשבשבון_" + Program.Today.ToString("dd_MMM_yyyy").Replace("'", "").Replace("\"", ""),
                Html = this.GetEntryListText()
            };
            fb.Show();
            return fb;
        }
        #endregion Private Functions

        #region Public Functions

        public void AddNewEntry(Entry newEntry, Form sourceForm = null)
        {
            Entry.EntryList.Add(newEntry);
            Entry.SortEntriesAndSetInterval();
            Kavuah.FindAndPromptKavuahs();
            this.CalculateProblemOnahs();
            this.SaveCurrentFile(sourceForm);
            //In case there were changes to the notes on some entries such as if there was a NoKavuah added
            this.bindingSourceEntries.DataSource = Entry.EntryList.Where(en => !en.IsInvisible);
        }

        public bool AddNewKavuah(Form owner)
        {
            using (frmAddKavuah f = new frmAddKavuah())
            {
                f.ShowDialog(owner);
                if (f.DialogResult != DialogResult.Cancel)
                {
                    this.SaveCurrentFile();
                    this.RefreshData();
                    return true;
                }
            }
            return false;
        }

        public void AfterChangePreferences()
        {
            this.SetLocation();
            this.SetDateAndDayNight();
            this.CalculateProblemOnahs();
            this.SetCaptionText();
            this.DisplayMonth();
        }

        public void DeleteEntry(Entry entry)
        {
            if (MessageBox.Show("האם אתם בטוחים שברצונכם למחוק השורה של " +
                                                    entry.DateTime.ToString("dd MMMM yyyy"),
                                                  "מחיקת שורה " + entry.DateTime.ToString("dd MMMM yyyy"),
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Question,
                                                  MessageBoxDefaultButton.Button1,
                                                  MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading) == DialogResult.Yes)
            {
                if (Kavuah.KavuahsList.Exists(k => k.SettingEntry == entry ||
                    (k.SettingEntryDate == entry.DateTime && k.DayNight == entry.DayNight)))
                {
                    if (MessageBox.Show(" נמצאו וסתי קבוע שהוגדרו על פי רשומה הזאת. האם אתם עדיין בטוחים שברצונכם למחוק השורה של " +
                                                entry.DateTime.ToString("dd MMMM yyyy") +
                                                " וגם כל וסת הקבוע שנרשמו בגללה?",
                                              "מחיקת שורה " + entry.DateTime.ToString("dd MMMM yyyy"),
                                              MessageBoxButtons.YesNo,
                                              MessageBoxIcon.Exclamation,
                                              MessageBoxDefaultButton.Button2,
                                              MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading) == DialogResult.Yes)
                    {
                        Kavuah.KavuahsList.RemoveAll(k => k.SettingEntry == entry ||
                            (k.SettingEntryDate == entry.DateTime && k.DayNight == entry.DayNight));
                    }
                    else
                    {
                        return;
                    }
                }
                Entry.EntryList.Remove(entry);
                Entry.SortEntriesAndSetInterval();
                Kavuah.FindAndPromptKavuahs();
                this.CalculateProblemOnahs();
                this.SaveCurrentFile();
                //In case there were changes to the notes on some entries such as if there was a NoKavuah added
                this.bindingSourceEntries.DataSource = Entry.EntryList.Where(en => !en.IsInvisible);
                this.DisplayMonth();
            }
        }

        public string GetTempXmlFile()
        {
            File.WriteAllText(this._tempXMLFileName, CurrentFileXML ?? "");
            return this._tempXMLFileName;
        }

        public void LoadXmlFile()
        {
            this.CreateLocalBackup();
            this.lblNextProblem.Text = "";

            if (CurrentFileIsRemote || File.Exists(CurrentFile))
            {
                try
                {
                    (Entry.EntryList, Kavuah.KavuahsList) =
                        Program.LoadEntriesKavuahsFromXml(this.CurrentFileXML);
                }
                catch (Exception)
                {
                    MessageBox.Show("הקובץ שאמור ליפתח" + Environment.NewLine + "\"" +
                        CurrentFile + "\"" + Environment.NewLine +
                        " איננה קובץ חשבשבון תקינה. תפתח רשימה ריקה.",
                        "חשבשבון",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
                    //Clear previous list data
                    Entry.EntryList.Clear();
                    //Clear previous Kavuahs
                    if (Kavuah.KavuahsList != null)
                    {
                        Kavuah.KavuahsList.Clear();
                    }
                }
            }

            if (Kavuah.KavuahsList == null)
            {
                Kavuah.KavuahsList = new List<Kavuah>();
            }

            if (File.Exists(CurrentFile) && !Properties.Settings.Default.RecentFiles.Contains(CurrentFile))
            {
                Properties.Settings.Default.RecentFiles.Insert(0, CurrentFile);
                this.recentFilesToolStripMenuItem.DropDownItems.Insert(0, new ToolStripMenuItem(CurrentFile));
                this.recentFilesToolStripMenuItem.Enabled = this.clearRecentFilesToolStripMenuItem.Enabled = true;
            }

            this.SetCaptionText();
            this.CalculateProblemOnahs();
            this.bindingSourceEntries.DataSource = Entry.EntryList.Where(en => !en.IsInvisible);
        }

        private void CalculateProblemOnahs()
        {
            ProblemOnahs.CalculateProblemOnahs();
            //The lblNextProblem displays the next upcoming Onah that needs to be kept
            this.lblNextProblem.Text = ProblemOnahs.GetNextOnahText();
            this.WeekListHtml = GetWeekListHtml();
        }

        private string GetWeekListHtml()
        {
            // First we combine double onahs - such as if one onah is also day 30 and also a haflagah.
            // We will only display it once, but with both descriptions.
            // If one of them is to be ignored though, it will get it's own row.
            var onahsToAdd = new List<Onah>();
            foreach (Onah onah in ProblemOnahs.ProblemOnahList.Where(on => on.DateTime >= Program.Today || Program.Today.IsSameday(on.DateTime)))
            {
                if (onahsToAdd.Exists(o => Onah.IsSameOnahPeriod(o, onah) && o.IsIgnored == onah.IsIgnored))
                {
                    onahsToAdd.Where(o => Onah.IsSameOnahPeriod(o, onah)).First().Name += ", וגם " + onah.Name;
                }
                else
                {
                    onahsToAdd.Add(onah.Clone());
                }
            }

            var sb = new StringBuilder("<html><head><meta content='text/html;charset=UTF-8;' />" +
                "<title>לוח חשבשבון ");
            sb.Append(Program.Today.ToLongDateString());
            sb.Append("</title><style>" +
                "body,table,td{text-align:right;direction:rtl;font-family:Narkisim;}" +
                "table{border:solid 1px silver;width:100%;}" +
                "div.ignored{float:right;color:#999999;width:400px;text-align:right;}" +
                "td{padding:3px;margin:1px;}" +
                "tr.alt td{background-color:#f1f1f1;}" +
                "tr.red td{color:#ff0000;}" +
                "tr.ignored td{color:#999999;}</style></head>");
            sb.AppendFormat("<body><h3>לוח חשבשבון - עונות הבאות - {0}</h3>", Program.Today.ToLongDateString());
            if (onahsToAdd.Exists(o => o.IsIgnored))
            {
                sb.Append("<div class='ignored'>רשומות באפור לא פעילים עקב וסת קבוע</div>");
            }
            sb.Append("<table><tr><th>יום</th><th>תאריך</th><th>יום/לילה</th><th>סיבה</th></tr>");

            int count = 0;
            foreach (Onah onah in onahsToAdd)
            {
                sb.AppendFormat("<tr class='{0}'><td>{1}</td><td>{2} {3}</td><td>{4}</td><td width='50%'>{5}</td></tr>",
                    (count++ % 2 == 0 ? "alt" : "") + (Onah.IsSameOnahPeriod(Program.NowOnah, onah) ? " red" : "") + (onah.IsIgnored ? " ignored" : ""),
                    Chashavshavon.Utils.Zmanim.GetDayOfWeekText(onah.DateTime),
                    Chashavshavon.Utils.Zmanim.DaysOfMonthHebrew[onah.Day],
                    onah.Month.ToString(),
                    onah.HebrewDayNight,
                    onah.Name);
            }
            sb.AppendFormat("</table><br /><hr /><strong>{0}</strong><hr />", this.lblNextProblem.Text);
            sb.Append("</body></html>");
            return sb.ToString();
        }

        /// <summary>
        /// Saves all changes back to the source file.
        /// </summary>
        /// <remarks>
        /// This function is run whenever a change is made to the list and when closing the app.
        /// </remarks>
        public void SaveCurrentFile(Form sourceForm = null)
        {
            //If no file was originally loaded, CurrentFile will be null.
            //In this case, if there are entries in the list
            //we prompt the user to create a file to save to.
            while (string.IsNullOrEmpty(this.CurrentFile))
            {
                if (((Entry.EntryList.Count + Kavuah.KavuahsList.Count) > 0) &&
                    MessageBox.Show("?שמירת הרשימה מצריך קובץ. האם ליצור קובץ חדש",
                        "חשבשבון",
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading) == DialogResult.Yes)
                {
                    this.SaveAs(sourceForm);
                }
                else
                {
                    return;
                }
            }

            XmlTextWriter xtw;
            Stream stream = null;

            if (this.CurrentFileIsRemote)
            {
                stream = new MemoryStream();
            }
            else
            {
                string dir = Path.GetDirectoryName(this.CurrentFile);

                try
                {
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }
                }
                catch (IOException)
                {
                    string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
                        @"\Chashavshavon Files";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    this.CurrentFile = this.CurrentFile.Replace(dir, path);
                    MessageBox.Show("חשבשבון היה צריל להעתיק הקובץ הפעילה למיקום אחר.\nהקובץ עכשיו נמצא ב: \n" +
                        this.CurrentFile,
                        "חשבשבון",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
                }
                stream = File.CreateText(this.CurrentFile).BaseStream;
            }
            xtw = new XmlTextWriter(stream, Encoding.UTF8);
            xtw.WriteStartDocument();
            xtw.WriteStartElement("Entries");
            foreach (Entry entry in Entry.EntryList)
            {
                xtw.WriteStartElement("Entry");
                xtw.WriteElementString("IsInvisible", entry.IsInvisible.ToString());
                xtw.WriteElementString("Date", entry.DateTime.ToString("dd MMMM yyyy") + " " + entry.HebrewDayNight);
                xtw.WriteElementString("Day", entry.Day.ToString());
                xtw.WriteElementString("Month", entry.Month.MonthInYear.ToString());
                xtw.WriteElementString("Year", entry.Year.ToString());
                xtw.WriteElementString("DN", ((int)entry.DayNight).ToString());
                xtw.WriteElementString("Notes", entry.Notes);
                foreach (Kavuah k in entry.NoKavuahList)
                {
                    xtw.WriteStartElement("NoKavuah");
                    xtw.WriteAttributeString("KavuahType", k.KavuahType.ToString());
                    xtw.WriteAttributeString("Number", k.Number.ToString());
                    xtw.WriteEndElement();
                }
                xtw.WriteEndElement();
            }

            var ser = new XmlSerializer(typeof(List<Kavuah>));
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
            Properties.Settings.Default.CurrentFile = this.CurrentFile;
            Properties.Settings.Default.Save();
            this.SetCaptionText();
        }

        public void SetCaptionText()
        {
            this.Text = " חשבשבון גירסה " +
                System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString() + " - " +
                Program.GetCurrentPlaceName() + " - " +
                (this.CurrentFileIsRemote ? "קובץ רשת - " : "") + this.CurrentFileName;
            if (this.pbWeb != null)
            {
                this.pbWeb.Visible = this.CurrentFileIsRemote;
            }
        }

        /// <summary>
        /// Checks to see if the user is connected to the internet, and activates remote functionality if they are.
        /// If the current file is a remote one and they are not connected, they are duly complained to about it...
        /// </summary>
        /// <returns></returns>
        public bool TestInternet()
        {
            bool hasInternet = Properties.Settings.Default.DevMode || Utils.RemoteFunctions.IsConnectedToInternet();
            RemoteToolStripMenuItem.Visible = hasInternet;
            if (CurrentFileIsRemote && !hasInternet)
            {
                MessageBox.Show("הקובץ שאמור ליפתח" + Environment.NewLine + "\"" +
                    CurrentFile + "\"" + Environment.NewLine + "הוא קובץ רשת, אבל אין גישה לרשת כרגע" + Environment.NewLine + ".לכן תפתח קובץ ריק",
                    "חשבשבון",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
                CurrentFileIsRemote = false;
                CurrentFile = Program.Today.ToString("ddMMMyyyy_hhmm").Replace("\"", "").Replace("'", "") + ".pm";
            }
            return hasInternet;
        }

        #endregion Public Functions

        #region Properties

        public bool CloseMeFirst { get; set; }

        public string CurrentFile
        {
            get => Properties.Settings.Default.CurrentFile;
            set
            {
                Properties.Settings.Default.CurrentFile = value;
                Properties.Settings.Default.Save();
                this.SetCaptionText();
            }
        }

        public bool CurrentFileIsRemote
        {
            get => Properties.Settings.Default.IsCurrentFileRemote;
            set
            {
                Properties.Settings.Default.IsCurrentFileRemote = value;
                Properties.Settings.Default.Save();
                this.SetCaptionText();
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
                else if (!String.IsNullOrWhiteSpace(this.CurrentFile))
                {
                    xml = File.ReadAllText(this.CurrentFile);
                }

                return (string.IsNullOrWhiteSpace(xml) ? "<Entries />" : xml);
            }
        }
        public string WeekListHtml { get; set; }

        #endregion Properties                
    }
}