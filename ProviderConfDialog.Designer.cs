namespace PangeaMtTranslationProvider
{
    partial class ProviderConfDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProviderConfDialog));
            this.bnt_OK = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.openFile = new System.Windows.Forms.OpenFileDialog();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.chkResendDrafts = new System.Windows.Forms.CheckBox();
            this.chkPlainTextOnly = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnBrowseGlossary = new System.Windows.Forms.Button();
            this.txtGlossaryFile = new System.Windows.Forms.TextBox();
            this.chkUseGlossary = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnReverseLangs = new System.Windows.Forms.Button();
            this.txtTargetLang = new System.Windows.Forms.TextBox();
            this.txtSourceLang = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.listBoxEngines = new System.Windows.Forms.ListBox();
            this.txtEngine = new System.Windows.Forms.TextBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtDomain = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkSaveCreds = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClearCreds = new System.Windows.Forms.Button();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bnt_OK
            // 
            this.bnt_OK.Location = new System.Drawing.Point(323, 505);
            this.bnt_OK.Margin = new System.Windows.Forms.Padding(4);
            this.bnt_OK.Name = "bnt_OK";
            this.bnt_OK.Size = new System.Drawing.Size(100, 28);
            this.bnt_OK.TabIndex = 3;
            this.bnt_OK.Text = "&OK";
            this.bnt_OK.UseVisualStyleBackColor = true;
            this.bnt_OK.Click += new System.EventHandler(this.bnt_OK_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(431, 505);
            this.btn_Cancel.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(100, 28);
            this.btn_Cancel.TabIndex = 4;
            this.btn_Cancel.Text = "&Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // openFile
            // 
            this.openFile.FileName = "openFileDialog1";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.groupBox6);
            this.tabPage2.Controls.Add(this.groupBox5);
            this.tabPage2.Controls.Add(this.groupBox4);
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(512, 457);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Engines/Language pair";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.chkResendDrafts);
            this.groupBox6.Controls.Add(this.chkPlainTextOnly);
            this.groupBox6.Location = new System.Drawing.Point(9, 376);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(497, 75);
            this.groupBox6.TabIndex = 18;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Other options:";
            // 
            // chkResendDrafts
            // 
            this.chkResendDrafts.AutoSize = true;
            this.chkResendDrafts.Location = new System.Drawing.Point(6, 21);
            this.chkResendDrafts.Name = "chkResendDrafts";
            this.chkResendDrafts.Size = new System.Drawing.Size(277, 21);
            this.chkResendDrafts.TabIndex = 15;
            this.chkResendDrafts.Text = "Re-send draft and translated segments";
            this.chkResendDrafts.UseVisualStyleBackColor = true;
            // 
            // chkPlainTextOnly
            // 
            this.chkPlainTextOnly.AutoSize = true;
            this.chkPlainTextOnly.Location = new System.Drawing.Point(6, 48);
            this.chkPlainTextOnly.Name = "chkPlainTextOnly";
            this.chkPlainTextOnly.Size = new System.Drawing.Size(214, 21);
            this.chkPlainTextOnly.TabIndex = 16;
            this.chkPlainTextOnly.Text = "Send plain text only (no tags)";
            this.chkPlainTextOnly.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnBrowseGlossary);
            this.groupBox5.Controls.Add(this.txtGlossaryFile);
            this.groupBox5.Controls.Add(this.chkUseGlossary);
            this.groupBox5.Location = new System.Drawing.Point(9, 295);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(497, 75);
            this.groupBox5.TabIndex = 17;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Glossary:";
            // 
            // btnBrowseGlossary
            // 
            this.btnBrowseGlossary.Location = new System.Drawing.Point(413, 47);
            this.btnBrowseGlossary.Name = "btnBrowseGlossary";
            this.btnBrowseGlossary.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseGlossary.TabIndex = 7;
            this.btnBrowseGlossary.Text = "Browse...";
            this.btnBrowseGlossary.UseVisualStyleBackColor = true;
            this.btnBrowseGlossary.Click += new System.EventHandler(this.btnBrowseGlossary_Click);
            // 
            // txtGlossaryFile
            // 
            this.txtGlossaryFile.Location = new System.Drawing.Point(6, 48);
            this.txtGlossaryFile.Name = "txtGlossaryFile";
            this.txtGlossaryFile.ReadOnly = true;
            this.txtGlossaryFile.Size = new System.Drawing.Size(401, 22);
            this.txtGlossaryFile.TabIndex = 6;
            // 
            // chkUseGlossary
            // 
            this.chkUseGlossary.AutoSize = true;
            this.chkUseGlossary.Location = new System.Drawing.Point(6, 21);
            this.chkUseGlossary.Name = "chkUseGlossary";
            this.chkUseGlossary.Size = new System.Drawing.Size(370, 21);
            this.chkUseGlossary.TabIndex = 0;
            this.chkUseGlossary.Text = "Use glossary file (removes tags from source segment)";
            this.chkUseGlossary.UseVisualStyleBackColor = true;
            this.chkUseGlossary.CheckedChanged += new System.EventHandler(this.chkUseGlossary_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnReverseLangs);
            this.groupBox4.Controls.Add(this.txtTargetLang);
            this.groupBox4.Controls.Add(this.txtSourceLang);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Location = new System.Drawing.Point(9, 214);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(497, 75);
            this.groupBox4.TabIndex = 16;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Language pair:";
            // 
            // btnReverseLangs
            // 
            this.btnReverseLangs.Location = new System.Drawing.Point(149, 44);
            this.btnReverseLangs.Name = "btnReverseLangs";
            this.btnReverseLangs.Size = new System.Drawing.Size(192, 26);
            this.btnReverseLangs.TabIndex = 4;
            this.btnReverseLangs.Text = "Reverse languages <-->";
            this.btnReverseLangs.UseVisualStyleBackColor = true;
            this.btnReverseLangs.Click += new System.EventHandler(this.btnReverseLangs_Click);
            // 
            // txtTargetLang
            // 
            this.txtTargetLang.Location = new System.Drawing.Point(413, 46);
            this.txtTargetLang.Name = "txtTargetLang";
            this.txtTargetLang.ReadOnly = true;
            this.txtTargetLang.Size = new System.Drawing.Size(51, 22);
            this.txtTargetLang.TabIndex = 3;
            // 
            // txtSourceLang
            // 
            this.txtSourceLang.Location = new System.Drawing.Point(28, 46);
            this.txtSourceLang.Name = "txtSourceLang";
            this.txtSourceLang.ReadOnly = true;
            this.txtSourceLang.Size = new System.Drawing.Size(54, 22);
            this.txtSourceLang.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(410, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 17);
            this.label4.TabIndex = 1;
            this.label4.Text = "Target:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "Source:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnRefresh);
            this.groupBox3.Controls.Add(this.listBoxEngines);
            this.groupBox3.Controls.Add(this.txtEngine);
            this.groupBox3.Location = new System.Drawing.Point(9, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(497, 202);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Engine:";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(6, 171);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(122, 23);
            this.btnRefresh.TabIndex = 14;
            this.btnRefresh.Text = "Refresh list";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // listBoxEngines
            // 
            this.listBoxEngines.FormattingEnabled = true;
            this.listBoxEngines.ItemHeight = 16;
            this.listBoxEngines.Location = new System.Drawing.Point(6, 49);
            this.listBoxEngines.Name = "listBoxEngines";
            this.listBoxEngines.Size = new System.Drawing.Size(485, 116);
            this.listBoxEngines.TabIndex = 12;
            this.listBoxEngines.SelectedIndexChanged += new System.EventHandler(this.listBoxEngines_SelectedIndexChanged);
            // 
            // txtEngine
            // 
            this.txtEngine.Location = new System.Drawing.Point(6, 21);
            this.txtEngine.Name = "txtEngine";
            this.txtEngine.ReadOnly = true;
            this.txtEngine.Size = new System.Drawing.Size(485, 22);
            this.txtEngine.TabIndex = 13;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(512, 457);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Credentials/Domain";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtDomain);
            this.groupBox2.Location = new System.Drawing.Point(6, 276);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(500, 68);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Domain/URL:";
            // 
            // txtDomain
            // 
            this.txtDomain.Location = new System.Drawing.Point(9, 28);
            this.txtDomain.Name = "txtDomain";
            this.txtDomain.Size = new System.Drawing.Size(434, 22);
            this.txtDomain.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkSaveCreds);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnClearCreds);
            this.groupBox1.Controls.Add(this.txtUsername);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(500, 247);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Credentials:";
            // 
            // chkSaveCreds
            // 
            this.chkSaveCreds.AutoSize = true;
            this.chkSaveCreds.Location = new System.Drawing.Point(9, 152);
            this.chkSaveCreds.Name = "chkSaveCreds";
            this.chkSaveCreds.Size = new System.Drawing.Size(256, 21);
            this.chkSaveCreds.TabIndex = 14;
            this.chkSaveCreds.Text = "Save credentials for future sessions";
            this.chkSaveCreds.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Username:";
            // 
            // btnClearCreds
            // 
            this.btnClearCreds.Location = new System.Drawing.Point(6, 179);
            this.btnClearCreds.Name = "btnClearCreds";
            this.btnClearCreds.Size = new System.Drawing.Size(249, 23);
            this.btnClearCreds.TabIndex = 12;
            this.btnClearCreds.Text = "Clear saved credentials";
            this.btnClearCreds.UseVisualStyleBackColor = true;
            this.btnClearCreds.Click += new System.EventHandler(this.btnClearCreds_Click);
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(9, 49);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(434, 22);
            this.txtUsername.TabIndex = 7;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(9, 107);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(434, 22);
            this.txtPassword.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 17);
            this.label2.TabIndex = 9;
            this.label2.Text = "Password:";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(520, 486);
            this.tabControl1.TabIndex = 5;
            // 
            // ProviderConfDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 546);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.bnt_OK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ProviderConfDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PangeaMT Provider Options";
            this.tabPage2.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bnt_OK;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.OpenFileDialog openFile;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.CheckBox chkResendDrafts;
        private System.Windows.Forms.CheckBox chkPlainTextOnly;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnBrowseGlossary;
        private System.Windows.Forms.TextBox txtGlossaryFile;
        private System.Windows.Forms.CheckBox chkUseGlossary;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnReverseLangs;
        private System.Windows.Forms.TextBox txtTargetLang;
        private System.Windows.Forms.TextBox txtSourceLang;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.ListBox listBoxEngines;
        private System.Windows.Forms.TextBox txtEngine;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtDomain;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkSaveCreds;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClearCreds;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabControl tabControl1;
    }
}