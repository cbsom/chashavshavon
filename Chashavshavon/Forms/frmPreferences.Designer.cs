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
            this.rbLocsInDiaspora = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.cbLocations = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbRequirePassword = new System.Windows.Forms.CheckBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.cbSummerTime = new System.Windows.Forms.CheckBox();
            this.rbLocsInIsrael = new System.Windows.Forms.RadioButton();
            this.cbOpenLastfile = new System.Windows.Forms.CheckBox();
            this.cbOB24Hours = new System.Windows.Forms.CheckBox();
            this.ShowOhrZeruah = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rbLocsInDiaspora
            // 
            this.rbLocsInDiaspora.AutoSize = true;
            this.rbLocsInDiaspora.Location = new System.Drawing.Point(360, 53);
            this.rbLocsInDiaspora.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.rbLocsInDiaspora.Name = "rbLocsInDiaspora";
            this.rbLocsInDiaspora.Size = new System.Drawing.Size(52, 17);
            this.rbLocsInDiaspora.TabIndex = 2;
            this.rbLocsInDiaspora.TabStop = true;
            this.rbLocsInDiaspora.Text = "בחו\"ל";
            this.rbLocsInDiaspora.UseVisualStyleBackColor = true;
            this.rbLocsInDiaspora.CheckedChanged += new System.EventHandler(this.rbLocsInDiaspora_CheckedChanged);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.BurlyWood;
            this.button1.Location = new System.Drawing.Point(369, 496);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(88, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "שמור";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.BurlyWood;
            this.button2.Location = new System.Drawing.Point(275, 496);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(88, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "בטל";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // cbLocations
            // 
            this.cbLocations.DisplayMember = "InnerText";
            this.cbLocations.ForeColor = System.Drawing.Color.SaddleBrown;
            this.cbLocations.FormattingEnabled = true;
            this.cbLocations.Location = new System.Drawing.Point(26, 76);
            this.cbLocations.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cbLocations.Name = "cbLocations";
            this.cbLocations.Size = new System.Drawing.Size(386, 21);
            this.cbLocations.TabIndex = 0;
            this.cbLocations.Text = "קרית ספר";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.cbLocations);
            this.groupBox1.Controls.Add(this.cbSummerTime);
            this.groupBox1.Controls.Add(this.rbLocsInIsrael);
            this.groupBox1.Controls.Add(this.rbLocsInDiaspora);
            this.groupBox1.Location = new System.Drawing.Point(18, 18);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Size = new System.Drawing.Size(437, 145);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "בחר מקום";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbOB24Hours);
            this.groupBox2.Controls.Add(this.ShowOhrZeruah);
            this.groupBox2.Location = new System.Drawing.Point(18, 169);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox2.Size = new System.Drawing.Size(437, 74);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "העדפות הלכיות";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbOpenLastfile);
            this.groupBox3.Controls.Add(this.cbRequirePassword);
            this.groupBox3.Controls.Add(this.txtPassword);
            this.groupBox3.Location = new System.Drawing.Point(18, 258);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox3.Size = new System.Drawing.Size(437, 129);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "העדפות כלליות";
            // 
            // cbRequirePassword
            // 
            this.cbRequirePassword.Checked = true;
            this.cbRequirePassword.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbRequirePassword.Location = new System.Drawing.Point(24, 30);
            this.cbRequirePassword.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cbRequirePassword.Name = "cbRequirePassword";
            this.cbRequirePassword.Size = new System.Drawing.Size(388, 17);
            this.cbRequirePassword.TabIndex = 7;
            this.cbRequirePassword.Text = "תחייב הקשת הסיסמה הבאה בפתיחת התוכנית";
            this.cbRequirePassword.UseVisualStyleBackColor = true;
            this.cbRequirePassword.CheckedChanged += new System.EventHandler(this.cbRequirePassword_CheckedChanged);
            // 
            // txtPassword
            // 
            this.txtPassword.ForeColor = System.Drawing.Color.SaddleBrown;
            this.txtPassword.Location = new System.Drawing.Point(26, 53);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(386, 20);
            this.txtPassword.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Location = new System.Drawing.Point(-5, 53);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(475, 407);
            this.panel1.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Narkisim", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label4.ForeColor = System.Drawing.Color.Maroon;
            this.label4.Location = new System.Drawing.Point(11, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 16);
            this.label4.TabIndex = 11;
            this.label4.Text = "העדפות";
            // 
            // cbSummerTime
            // 
            this.cbSummerTime.Checked = global::Chashavshavon.Properties.Settings.Default.IsSummerTime;
            this.cbSummerTime.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSummerTime.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Chashavshavon.Properties.Settings.Default, "IsSummerTime", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cbSummerTime.Location = new System.Drawing.Point(227, 110);
            this.cbSummerTime.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cbSummerTime.Name = "cbSummerTime";
            this.cbSummerTime.Size = new System.Drawing.Size(185, 17);
            this.cbSummerTime.TabIndex = 5;
            this.cbSummerTime.Text = "שעון קיץ";
            this.cbSummerTime.UseVisualStyleBackColor = true;
            // 
            // rbLocsInIsrael
            // 
            this.rbLocsInIsrael.AutoSize = true;
            this.rbLocsInIsrael.Checked = global::Chashavshavon.Properties.Settings.Default.UserInIsrael;
            this.rbLocsInIsrael.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Chashavshavon.Properties.Settings.Default, "UserInIsrael", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.rbLocsInIsrael.Location = new System.Drawing.Point(362, 30);
            this.rbLocsInIsrael.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.rbLocsInIsrael.Name = "rbLocsInIsrael";
            this.rbLocsInIsrael.Size = new System.Drawing.Size(50, 17);
            this.rbLocsInIsrael.TabIndex = 1;
            this.rbLocsInIsrael.TabStop = true;
            this.rbLocsInIsrael.Text = "בארץ";
            this.rbLocsInIsrael.UseVisualStyleBackColor = true;
            this.rbLocsInIsrael.CheckedChanged += new System.EventHandler(this.rbLocsInIsrael_CheckedChanged);
            // 
            // cbOpenLastfile
            // 
            this.cbOpenLastfile.Checked = global::Chashavshavon.Properties.Settings.Default.OpenLastFile;
            this.cbOpenLastfile.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbOpenLastfile.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Chashavshavon.Properties.Settings.Default, "OpenLastFile", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cbOpenLastfile.Location = new System.Drawing.Point(24, 89);
            this.cbOpenLastfile.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cbOpenLastfile.Name = "cbOpenLastfile";
            this.cbOpenLastfile.Size = new System.Drawing.Size(388, 17);
            this.cbOpenLastfile.TabIndex = 8;
            this.cbOpenLastfile.Text = "בפתיחת התוכנית, פתח קובץ האחרון ";
            this.cbOpenLastfile.UseVisualStyleBackColor = true;
            // 
            // cbOB24Hours
            // 
            this.cbOB24Hours.Checked = global::Chashavshavon.Properties.Settings.Default.OnahBenIs24Hours;
            this.cbOB24Hours.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbOB24Hours.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Chashavshavon.Properties.Settings.Default, "OnahBenIs24Hours", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cbOB24Hours.Location = new System.Drawing.Point(141, 36);
            this.cbOB24Hours.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cbOB24Hours.Name = "cbOB24Hours";
            this.cbOB24Hours.Size = new System.Drawing.Size(142, 17);
            this.cbOB24Hours.TabIndex = 7;
            this.cbOB24Hours.Text = "עונה בינונית מעת לעת?";
            this.cbOB24Hours.UseVisualStyleBackColor = true;
            // 
            // ShowOhrZeruah
            // 
            this.ShowOhrZeruah.Checked = global::Chashavshavon.Properties.Settings.Default.ShowOhrZeruah;
            this.ShowOhrZeruah.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ShowOhrZeruah.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Chashavshavon.Properties.Settings.Default, "ShowOhrZeruah", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.ShowOhrZeruah.Location = new System.Drawing.Point(291, 36);
            this.ShowOhrZeruah.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ShowOhrZeruah.Name = "ShowOhrZeruah";
            this.ShowOhrZeruah.Size = new System.Drawing.Size(121, 17);
            this.ShowOhrZeruah.TabIndex = 6;
            this.ShowOhrZeruah.Text = "הצג זמני אור זרוע?";
            this.ShowOhrZeruah.UseVisualStyleBackColor = true;
            // 
            // frmPreferences
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Tan;
            this.ClientSize = new System.Drawing.Size(468, 530);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Font = new System.Drawing.Font("Narkisim", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
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
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbLocations;
        private System.Windows.Forms.RadioButton rbLocsInIsrael;
        private System.Windows.Forms.RadioButton rbLocsInDiaspora;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox cbSummerTime;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox ShowOhrZeruah;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.CheckBox cbRequirePassword;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox cbOpenLastfile;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox cbOB24Hours;

    }
}