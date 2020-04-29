namespace Chashavshavon
{
    partial class frmPreferences
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPreferences));
            this.rbPlacesInDiaspora = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.cbPlaces = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbPlacesInIsrael = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.pbShowPassword = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbRequirePassword = new System.Windows.Forms.CheckBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rbOpenFileDialog = new System.Windows.Forms.RadioButton();
            this.rbOpenNewFile = new System.Windows.Forms.RadioButton();
            this.rbOpenLastFileName = new System.Windows.Forms.RadioButton();
            this.rbOpenLastFile = new System.Windows.Forms.RadioButton();
            this.numberMonthsAheadToWarn = new System.Windows.Forms.NumericUpDown();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.cbKeepLongerHaflagah = new System.Windows.Forms.CheckBox();
            this.ShowOhrZeruah = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbShowPassword)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numberMonthsAheadToWarn)).BeginInit();
            this.SuspendLayout();
            // 
            // rbPlacesInDiaspora
            // 
            this.rbPlacesInDiaspora.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rbPlacesInDiaspora.AutoSize = true;
            this.rbPlacesInDiaspora.Location = new System.Drawing.Point(503, 28);
            this.rbPlacesInDiaspora.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.rbPlacesInDiaspora.Name = "rbPlacesInDiaspora";
            this.rbPlacesInDiaspora.Size = new System.Drawing.Size(65, 20);
            this.rbPlacesInDiaspora.TabIndex = 2;
            this.rbPlacesInDiaspora.TabStop = true;
            this.rbPlacesInDiaspora.Text = "בחו\"ל";
            this.rbPlacesInDiaspora.UseVisualStyleBackColor = true;
            this.rbPlacesInDiaspora.CheckedChanged += new System.EventHandler(this.rbPlacesInDiaspora_CheckedChanged);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Lavender;
            this.button1.BackgroundImage = global::Chashavshavon.Properties.Resources.DarkBlueMarbleTile;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Narkisim", 11F);
            this.button1.ForeColor = System.Drawing.Color.Lavender;
            this.button1.Location = new System.Drawing.Point(122, 23);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(102, 27);
            this.button1.TabIndex = 3;
            this.button1.Text = "שמור";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Lavender;
            this.button2.BackgroundImage = global::Chashavshavon.Properties.Resources.DarkBlueMarbleTile;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Narkisim", 11F);
            this.button2.ForeColor = System.Drawing.Color.Lavender;
            this.button2.Location = new System.Drawing.Point(12, 23);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(102, 27);
            this.button2.TabIndex = 4;
            this.button2.Text = "בטל";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // cbPlaces
            // 
            this.cbPlaces.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbPlaces.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbPlaces.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbPlaces.DisplayMember = "InnerText";
            this.cbPlaces.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPlaces.ForeColor = System.Drawing.Color.SaddleBrown;
            this.cbPlaces.FormattingEnabled = true;
            this.cbPlaces.Location = new System.Drawing.Point(252, 54);
            this.cbPlaces.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cbPlaces.Name = "cbPlaces";
            this.cbPlaces.Size = new System.Drawing.Size(386, 24);
            this.cbPlaces.TabIndex = 0;
            this.cbPlaces.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.cbPlacess_Format);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.cbPlaces);
            this.groupBox1.Controls.Add(this.rbPlacesInIsrael);
            this.groupBox1.Controls.Add(this.rbPlacesInDiaspora);
            this.groupBox1.Location = new System.Drawing.Point(13, 57);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Size = new System.Drawing.Size(672, 98);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "בחר מקום";
            // 
            // rbPlacesInIsrael
            // 
            this.rbPlacesInIsrael.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rbPlacesInIsrael.AutoSize = true;
            this.rbPlacesInIsrael.Location = new System.Drawing.Point(576, 28);
            this.rbPlacesInIsrael.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.rbPlacesInIsrael.Name = "rbPlacesInIsrael";
            this.rbPlacesInIsrael.Size = new System.Drawing.Size(62, 20);
            this.rbPlacesInIsrael.TabIndex = 1;
            this.rbPlacesInIsrael.TabStop = true;
            this.rbPlacesInIsrael.Text = "בארץ";
            this.rbPlacesInIsrael.UseVisualStyleBackColor = true;
            this.rbPlacesInIsrael.CheckedChanged += new System.EventHandler(this.rbPlacesInIsrael_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.checkBox1);
            this.groupBox2.Controls.Add(this.checkBox3);
            this.groupBox2.Controls.Add(this.cbKeepLongerHaflagah);
            this.groupBox2.Controls.Add(this.ShowOhrZeruah);
            this.groupBox2.Location = new System.Drawing.Point(13, 170);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox2.Size = new System.Drawing.Size(672, 175);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "העדפות הלכיות";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Controls.Add(this.pbShowPassword);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.numberMonthsAheadToWarn);
            this.groupBox3.Controls.Add(this.cbRequirePassword);
            this.groupBox3.Controls.Add(this.txtPassword);
            this.groupBox3.Location = new System.Drawing.Point(13, 360);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox3.Size = new System.Drawing.Size(672, 209);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "העדפות כלליות";
            // 
            // pbShowPassword
            // 
            this.pbShowPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbShowPassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbShowPassword.Image = ((System.Drawing.Image)(resources.GetObject("pbShowPassword.Image")));
            this.pbShowPassword.Location = new System.Drawing.Point(267, 78);
            this.pbShowPassword.Name = "pbShowPassword";
            this.pbShowPassword.Size = new System.Drawing.Size(24, 24);
            this.pbShowPassword.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbShowPassword.TabIndex = 11;
            this.pbShowPassword.TabStop = false;
            this.pbShowPassword.Click += new System.EventHandler(this.pbShowPassword_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(382, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(242, 16);
            this.label1.TabIndex = 10;
            this.label1.Text = "מספר חודשים קדימה לחשב התראות:";
            // 
            // cbRequirePassword
            // 
            this.cbRequirePassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbRequirePassword.Checked = true;
            this.cbRequirePassword.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbRequirePassword.Location = new System.Drawing.Point(267, 54);
            this.cbRequirePassword.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cbRequirePassword.Name = "cbRequirePassword";
            this.cbRequirePassword.Size = new System.Drawing.Size(357, 17);
            this.cbRequirePassword.TabIndex = 7;
            this.cbRequirePassword.Text = "תחייב הקשת הסיסמה הבאה בפתיחת התוכנית";
            this.cbRequirePassword.UseVisualStyleBackColor = true;
            this.cbRequirePassword.CheckedChanged += new System.EventHandler(this.cbRequirePassword_CheckedChanged);
            // 
            // txtPassword
            // 
            this.txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPassword.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.txtPassword.ForeColor = System.Drawing.Color.Black;
            this.txtPassword.Location = new System.Drawing.Point(298, 73);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '•';
            this.txtPassword.Size = new System.Drawing.Size(326, 34);
            this.txtPassword.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Narkisim", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label4.ForeColor = System.Drawing.Color.SlateGray;
            this.label4.Location = new System.Drawing.Point(584, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 24);
            this.label4.TabIndex = 11;
            this.label4.Text = "העדפות";
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::Chashavshavon.Properties.Resources.BlueMarble;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 590);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(698, 74);
            this.panel1.TabIndex = 12;
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = global::Chashavshavon.Properties.Resources.BlueMarble;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(698, 49);
            this.panel2.TabIndex = 13;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rbOpenFileDialog);
            this.groupBox4.Controls.Add(this.rbOpenNewFile);
            this.groupBox4.Controls.Add(this.rbOpenLastFileName);
            this.groupBox4.Controls.Add(this.rbOpenLastFile);
            this.groupBox4.Location = new System.Drawing.Point(24, 121);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(628, 82);
            this.groupBox4.TabIndex = 13;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "בפתיחת התוכנה";
            // 
            // rbOpenFileDialog
            // 
            this.rbOpenFileDialog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rbOpenFileDialog.AutoSize = true;
            this.rbOpenFileDialog.Location = new System.Drawing.Point(21, 54);
            this.rbOpenFileDialog.Name = "rbOpenFileDialog";
            this.rbOpenFileDialog.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rbOpenFileDialog.Size = new System.Drawing.Size(222, 20);
            this.rbOpenFileDialog.TabIndex = 0;
            this.rbOpenFileDialog.Text = "פתח חלון בחירת קובץ לפתיחה";
            this.rbOpenFileDialog.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rbOpenFileDialog.UseVisualStyleBackColor = true;
            this.rbOpenFileDialog.CheckedChanged += new System.EventHandler(this.rbOpenLastFile_CheckedChanged);
            // 
            // rbOpenNewFile
            // 
            this.rbOpenNewFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rbOpenNewFile.AutoSize = true;
            this.rbOpenNewFile.Location = new System.Drawing.Point(119, 28);
            this.rbOpenNewFile.Name = "rbOpenNewFile";
            this.rbOpenNewFile.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rbOpenNewFile.Size = new System.Drawing.Size(124, 20);
            this.rbOpenNewFile.TabIndex = 0;
            this.rbOpenNewFile.Text = "פתח קובץ חדש";
            this.rbOpenNewFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rbOpenNewFile.UseVisualStyleBackColor = true;
            this.rbOpenNewFile.CheckedChanged += new System.EventHandler(this.rbOpenLastFile_CheckedChanged);
            // 
            // rbOpenLastFileName
            // 
            this.rbOpenLastFileName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rbOpenLastFileName.AutoSize = true;
            this.rbOpenLastFileName.Location = new System.Drawing.Point(273, 54);
            this.rbOpenLastFileName.Name = "rbOpenLastFileName";
            this.rbOpenLastFileName.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rbOpenLastFileName.Size = new System.Drawing.Size(322, 20);
            this.rbOpenLastFileName.TabIndex = 0;
            this.rbOpenLastFileName.Text = "פתח קובץ אחרון - גם אם זה אם זה קובץ מקוון";
            this.rbOpenLastFileName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rbOpenLastFileName.UseVisualStyleBackColor = true;
            this.rbOpenLastFileName.CheckedChanged += new System.EventHandler(this.rbOpenLastFile_CheckedChanged);
            // 
            // rbOpenLastFile
            // 
            this.rbOpenLastFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rbOpenLastFile.AutoSize = true;
            this.rbOpenLastFile.Checked = true;
            this.rbOpenLastFile.Location = new System.Drawing.Point(384, 28);
            this.rbOpenLastFile.Name = "rbOpenLastFile";
            this.rbOpenLastFile.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rbOpenLastFile.Size = new System.Drawing.Size(211, 20);
            this.rbOpenLastFile.TabIndex = 0;
            this.rbOpenLastFile.TabStop = true;
            this.rbOpenLastFile.Text = "פתח קובץ אחרון - במחשב זה";
            this.rbOpenLastFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rbOpenLastFile.UseVisualStyleBackColor = true;
            this.rbOpenLastFile.CheckedChanged += new System.EventHandler(this.rbOpenLastFile_CheckedChanged);
            // 
            // numberMonthsAheadToWarn
            // 
            this.numberMonthsAheadToWarn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numberMonthsAheadToWarn.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::Chashavshavon.Properties.Settings.Default, "numberMonthsAheadToWarn", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numberMonthsAheadToWarn.Location = new System.Drawing.Point(335, 22);
            this.numberMonthsAheadToWarn.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numberMonthsAheadToWarn.Name = "numberMonthsAheadToWarn";
            this.numberMonthsAheadToWarn.Size = new System.Drawing.Size(41, 24);
            this.numberMonthsAheadToWarn.TabIndex = 9;
            this.numberMonthsAheadToWarn.Value = global::Chashavshavon.Properties.Settings.Default.NumberMonthsAheadToWarn;
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox1.Checked = global::Chashavshavon.Properties.Settings.Default.DilugChodeshPastEnds;
            this.checkBox1.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Chashavshavon.Properties.Settings.Default, "DilugChodeshPastEnds", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBox1.Location = new System.Drawing.Point(159, 136);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(497, 17);
            this.checkBox1.TabIndex = 12;
            this.checkBox1.Text = "להמשיך קבוע של דילוג יום החודש אחר שהגיע לסוף או תחילת החודש";
            this.toolTip1.SetToolTip(this.checkBox1, "להמשיך קבוע של דילוג יום החודש אחר שהגיע לסוף או תחילת החודש");
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox3.Checked = global::Chashavshavon.Properties.Settings.Default.OnahBenIs24Hours;
            this.checkBox3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox3.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Chashavshavon.Properties.Settings.Default, "OnahBenIs24Hours", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBox3.Location = new System.Drawing.Point(267, 100);
            this.checkBox3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(389, 17);
            this.checkBox3.TabIndex = 11;
            this.checkBox3.Text = "עונה בינונית מעת לעת";
            this.toolTip1.SetToolTip(this.checkBox3, "האם להתריע על עונה בינונית (30 ו31) מעת לעת, או רק עונת הראייה?");
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // cbKeepLongerHaflagah
            // 
            this.cbKeepLongerHaflagah.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbKeepLongerHaflagah.Checked = global::Chashavshavon.Properties.Settings.Default.KeepLongerHaflagah;
            this.cbKeepLongerHaflagah.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbKeepLongerHaflagah.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Chashavshavon.Properties.Settings.Default, "keepLongerHaflagah", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cbKeepLongerHaflagah.Location = new System.Drawing.Point(231, 68);
            this.cbKeepLongerHaflagah.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cbKeepLongerHaflagah.Name = "cbKeepLongerHaflagah";
            this.cbKeepLongerHaflagah.Size = new System.Drawing.Size(425, 17);
            this.cbKeepLongerHaflagah.TabIndex = 9;
            this.cbKeepLongerHaflagah.Text = "הפלגה קצרה אינו מבטל ארוכה (הט\"ז ושו\"ע הרב)";
            this.toolTip1.SetToolTip(this.cbKeepLongerHaflagah, "באין וסת קבוע, האם להתריע על יום הפלגה גם אחרי ראייה נוספת כל זמן שלא היתה הפלגה " +
        "ארוכה יותר?");
            this.cbKeepLongerHaflagah.UseVisualStyleBackColor = true;
            // 
            // ShowOhrZeruah
            // 
            this.ShowOhrZeruah.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ShowOhrZeruah.Checked = global::Chashavshavon.Properties.Settings.Default.ShowOhrZeruah;
            this.ShowOhrZeruah.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ShowOhrZeruah.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Chashavshavon.Properties.Settings.Default, "ShowOhrZeruah", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.ShowOhrZeruah.Location = new System.Drawing.Point(235, 36);
            this.ShowOhrZeruah.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ShowOhrZeruah.Name = "ShowOhrZeruah";
            this.ShowOhrZeruah.Size = new System.Drawing.Size(421, 17);
            this.ShowOhrZeruah.TabIndex = 6;
            this.ShowOhrZeruah.Text = "הצג זמני אור זרוע";
            this.toolTip1.SetToolTip(this.ShowOhrZeruah, "האם להתריע על העונה שלפני עונת הראייה או הוסת קבוע?");
            this.ShowOhrZeruah.UseVisualStyleBackColor = true;
            // 
            // frmPreferences
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(698, 664);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Font = new System.Drawing.Font("Narkisim", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPreferences";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "העדפות";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPreferences_FormClosing);
            this.Load += new System.EventHandler(this.Preferences_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbShowPassword)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numberMonthsAheadToWarn)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbPlaces;
        private System.Windows.Forms.RadioButton rbPlacesInIsrael;
        private System.Windows.Forms.RadioButton rbPlacesInDiaspora;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox ShowOhrZeruah;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.CheckBox cbRequirePassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numberMonthsAheadToWarn;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox cbKeepLongerHaflagah;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pbShowPassword;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton rbOpenFileDialog;
        private System.Windows.Forms.RadioButton rbOpenNewFile;
        private System.Windows.Forms.RadioButton rbOpenLastFileName;
        private System.Windows.Forms.RadioButton rbOpenLastFile;
    }
}