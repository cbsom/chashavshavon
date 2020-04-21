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
            this.rbPlacesInDiaspora = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.cbPlaces = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbSummerTime = new System.Windows.Forms.CheckBox();
            this.rbPlacesInIsrael = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.cbKeepLongerHaflagah = new System.Windows.Forms.CheckBox();
            this.ShowOhrZeruah = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numberMonthsAheadToWarn = new System.Windows.Forms.NumericUpDown();
            this.cbOpenLastfile = new System.Windows.Forms.CheckBox();
            this.cbRequirePassword = new System.Windows.Forms.CheckBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numberMonthsAheadToWarn)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // rbPlacesInDiaspora
            // 
            this.rbPlacesInDiaspora.AutoSize = true;
            this.rbPlacesInDiaspora.Location = new System.Drawing.Point(276, 35);
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
            this.cbPlaces.DisplayMember = "InnerText";
            this.cbPlaces.ForeColor = System.Drawing.Color.SaddleBrown;
            this.cbPlaces.FormattingEnabled = true;
            this.cbPlaces.Location = new System.Drawing.Point(25, 61);
            this.cbPlaces.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cbPlaces.Name = "cbPlaces";
            this.cbPlaces.Size = new System.Drawing.Size(386, 24);
            this.cbPlaces.TabIndex = 0;
            this.cbPlaces.Text = "קרית ספר";
            this.cbPlaces.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.cbPlacess_Format);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.cbPlaces);
            this.groupBox1.Controls.Add(this.cbSummerTime);
            this.groupBox1.Controls.Add(this.rbPlacesInIsrael);
            this.groupBox1.Controls.Add(this.rbPlacesInDiaspora);
            this.groupBox1.Location = new System.Drawing.Point(18, 57);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Size = new System.Drawing.Size(437, 131);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "בחר מקום";
            // 
            // cbSummerTime
            // 
            this.cbSummerTime.Checked = global::Chashavshavon.Properties.Settings.Default.IsSummerTime;
            this.cbSummerTime.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSummerTime.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Chashavshavon.Properties.Settings.Default, "IsSummerTime", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cbSummerTime.Location = new System.Drawing.Point(226, 95);
            this.cbSummerTime.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cbSummerTime.Name = "cbSummerTime";
            this.cbSummerTime.Size = new System.Drawing.Size(185, 17);
            this.cbSummerTime.TabIndex = 5;
            this.cbSummerTime.Text = "שעון קיץ";
            this.cbSummerTime.UseVisualStyleBackColor = true;
            // 
            // rbPlacesInIsrael
            // 
            this.rbPlacesInIsrael.AutoSize = true;
            this.rbPlacesInIsrael.Location = new System.Drawing.Point(349, 35);
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
            this.groupBox2.Controls.Add(this.checkBox3);
            this.groupBox2.Controls.Add(this.cbKeepLongerHaflagah);
            this.groupBox2.Controls.Add(this.ShowOhrZeruah);
            this.groupBox2.Location = new System.Drawing.Point(18, 200);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox2.Size = new System.Drawing.Size(437, 148);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "העדפות הלכיות";
            // 
            // checkBox3
            // 
            this.checkBox3.Checked = global::Chashavshavon.Properties.Settings.Default.OnahBenIs24Hours;
            this.checkBox3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox3.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Chashavshavon.Properties.Settings.Default, "OnahBenIs24Hours", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBox3.Location = new System.Drawing.Point(40, 100);
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
            this.cbKeepLongerHaflagah.Checked = global::Chashavshavon.Properties.Settings.Default.KeepLongerHaflagah;
            this.cbKeepLongerHaflagah.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbKeepLongerHaflagah.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Chashavshavon.Properties.Settings.Default, "keepLongerHaflagah", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cbKeepLongerHaflagah.Location = new System.Drawing.Point(4, 68);
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
            this.ShowOhrZeruah.Checked = global::Chashavshavon.Properties.Settings.Default.ShowOhrZeruah;
            this.ShowOhrZeruah.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ShowOhrZeruah.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Chashavshavon.Properties.Settings.Default, "ShowOhrZeruah", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.ShowOhrZeruah.Location = new System.Drawing.Point(8, 36);
            this.ShowOhrZeruah.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ShowOhrZeruah.Name = "ShowOhrZeruah";
            this.ShowOhrZeruah.Size = new System.Drawing.Size(421, 17);
            this.ShowOhrZeruah.TabIndex = 6;
            this.ShowOhrZeruah.Text = "הצג זמני אור זרוע";
            this.toolTip1.SetToolTip(this.ShowOhrZeruah, "האם להתריע על העונה שלפני עונת הראייה או הוסת קבוע?");
            this.ShowOhrZeruah.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.numberMonthsAheadToWarn);
            this.groupBox3.Controls.Add(this.cbOpenLastfile);
            this.groupBox3.Controls.Add(this.cbRequirePassword);
            this.groupBox3.Controls.Add(this.txtPassword);
            this.groupBox3.Location = new System.Drawing.Point(18, 359);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox3.Size = new System.Drawing.Size(437, 136);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "העדפות כלליות";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(155, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(242, 16);
            this.label1.TabIndex = 10;
            this.label1.Text = "מספר חודשים קדימה לחשב התראות:";
            // 
            // numberMonthsAheadToWarn
            // 
            this.numberMonthsAheadToWarn.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::Chashavshavon.Properties.Settings.Default, "numberMonthsAheadToWarn", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numberMonthsAheadToWarn.Location = new System.Drawing.Point(108, 22);
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
            // cbOpenLastfile
            // 
            this.cbOpenLastfile.Checked = global::Chashavshavon.Properties.Settings.Default.OpenLastFile;
            this.cbOpenLastfile.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbOpenLastfile.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Chashavshavon.Properties.Settings.Default, "OpenLastFile", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cbOpenLastfile.Location = new System.Drawing.Point(42, 109);
            this.cbOpenLastfile.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cbOpenLastfile.Name = "cbOpenLastfile";
            this.cbOpenLastfile.Size = new System.Drawing.Size(355, 17);
            this.cbOpenLastfile.TabIndex = 8;
            this.cbOpenLastfile.Text = "בפתיחת התוכנית, פתח קובץ האחרון ";
            this.cbOpenLastfile.UseVisualStyleBackColor = true;
            // 
            // cbRequirePassword
            // 
            this.cbRequirePassword.Checked = true;
            this.cbRequirePassword.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbRequirePassword.Location = new System.Drawing.Point(40, 54);
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
            this.txtPassword.ForeColor = System.Drawing.Color.SaddleBrown;
            this.txtPassword.Location = new System.Drawing.Point(42, 74);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(355, 24);
            this.txtPassword.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Narkisim", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label4.ForeColor = System.Drawing.Color.SlateGray;
            this.label4.Location = new System.Drawing.Point(372, 10);
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
            this.panel1.Location = new System.Drawing.Point(0, 504);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(468, 74);
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
            this.panel2.Size = new System.Drawing.Size(468, 49);
            this.panel2.TabIndex = 13;
            // 
            // frmPreferences
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(468, 578);
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
            ((System.ComponentModel.ISupportInitialize)(this.numberMonthsAheadToWarn)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbPlaces;
        private System.Windows.Forms.RadioButton rbPlacesInIsrael;
        private System.Windows.Forms.RadioButton rbPlacesInDiaspora;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox cbSummerTime;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox ShowOhrZeruah;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.CheckBox cbRequirePassword;
        private System.Windows.Forms.CheckBox cbOpenLastfile;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numberMonthsAheadToWarn;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox cbKeepLongerHaflagah;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;

    }
}