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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.listBoxEngines = new System.Windows.Forms.ListBox();
            this.txtEngine = new System.Windows.Forms.TextBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtDomain = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkSaveCreds = new System.Windows.Forms.CheckBox();
            this.btnClearCreds = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.aboutBox = new System.Windows.Forms.TextBox();
            this.tabPage2.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // bnt_OK
            // 
            this.bnt_OK.Location = new System.Drawing.Point(242, 410);
            this.bnt_OK.Name = "bnt_OK";
            this.bnt_OK.Size = new System.Drawing.Size(75, 23);
            this.bnt_OK.TabIndex = 3;
            this.bnt_OK.Text = "&OK";
            this.bnt_OK.UseVisualStyleBackColor = true;
            this.bnt_OK.Click += new System.EventHandler(this.bnt_OK_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(323, 410);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
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
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage2.Size = new System.Drawing.Size(382, 369);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Engines/Language pair";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.chkResendDrafts);
            this.groupBox6.Controls.Add(this.chkPlainTextOnly);
            this.groupBox6.Location = new System.Drawing.Point(7, 238);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox6.Size = new System.Drawing.Size(373, 61);
            this.groupBox6.TabIndex = 18;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Other options:";
            // 
            // chkResendDrafts
            // 
            this.chkResendDrafts.AutoSize = true;
            this.chkResendDrafts.Location = new System.Drawing.Point(4, 17);
            this.chkResendDrafts.Margin = new System.Windows.Forms.Padding(2);
            this.chkResendDrafts.Name = "chkResendDrafts";
            this.chkResendDrafts.Size = new System.Drawing.Size(208, 17);
            this.chkResendDrafts.TabIndex = 15;
            this.chkResendDrafts.Text = "Re-send draft and translated segments";
            this.chkResendDrafts.UseVisualStyleBackColor = true;
            // 
            // chkPlainTextOnly
            // 
            this.chkPlainTextOnly.AutoSize = true;
            this.chkPlainTextOnly.Location = new System.Drawing.Point(4, 39);
            this.chkPlainTextOnly.Margin = new System.Windows.Forms.Padding(2);
            this.chkPlainTextOnly.Name = "chkPlainTextOnly";
            this.chkPlainTextOnly.Size = new System.Drawing.Size(184, 17);
            this.chkPlainTextOnly.TabIndex = 16;
            this.chkPlainTextOnly.Text = "Send plain text only (without tags)";
            this.chkPlainTextOnly.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnBrowseGlossary);
            this.groupBox5.Controls.Add(this.txtGlossaryFile);
            this.groupBox5.Controls.Add(this.chkUseGlossary);
            this.groupBox5.Location = new System.Drawing.Point(7, 173);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox5.Size = new System.Drawing.Size(373, 61);
            this.groupBox5.TabIndex = 17;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Glossary:";
            // 
            // btnBrowseGlossary
            // 
            this.btnBrowseGlossary.Location = new System.Drawing.Point(310, 38);
            this.btnBrowseGlossary.Margin = new System.Windows.Forms.Padding(2);
            this.btnBrowseGlossary.Name = "btnBrowseGlossary";
            this.btnBrowseGlossary.Size = new System.Drawing.Size(56, 19);
            this.btnBrowseGlossary.TabIndex = 7;
            this.btnBrowseGlossary.Text = "Browse...";
            this.btnBrowseGlossary.UseVisualStyleBackColor = true;
            this.btnBrowseGlossary.Click += new System.EventHandler(this.btnBrowseGlossary_Click);
            // 
            // txtGlossaryFile
            // 
            this.txtGlossaryFile.Location = new System.Drawing.Point(4, 39);
            this.txtGlossaryFile.Margin = new System.Windows.Forms.Padding(2);
            this.txtGlossaryFile.Name = "txtGlossaryFile";
            this.txtGlossaryFile.ReadOnly = true;
            this.txtGlossaryFile.Size = new System.Drawing.Size(302, 20);
            this.txtGlossaryFile.TabIndex = 6;
            // 
            // chkUseGlossary
            // 
            this.chkUseGlossary.AutoSize = true;
            this.chkUseGlossary.Location = new System.Drawing.Point(4, 17);
            this.chkUseGlossary.Margin = new System.Windows.Forms.Padding(2);
            this.chkUseGlossary.Name = "chkUseGlossary";
            this.chkUseGlossary.Size = new System.Drawing.Size(346, 17);
            this.chkUseGlossary.TabIndex = 0;
            this.chkUseGlossary.Text = "Use glossary file (TAB-delimited, removes tags from source segment)";
            this.chkUseGlossary.UseVisualStyleBackColor = true;
            this.chkUseGlossary.CheckedChanged += new System.EventHandler(this.chkUseGlossary_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnRefresh);
            this.groupBox3.Controls.Add(this.listBoxEngines);
            this.groupBox3.Controls.Add(this.txtEngine);
            this.groupBox3.Location = new System.Drawing.Point(7, 5);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(373, 164);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Engine:";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(4, 139);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(2);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(92, 19);
            this.btnRefresh.TabIndex = 14;
            this.btnRefresh.Text = "Refresh list";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // listBoxEngines
            // 
            this.listBoxEngines.FormattingEnabled = true;
            this.listBoxEngines.Location = new System.Drawing.Point(4, 40);
            this.listBoxEngines.Margin = new System.Windows.Forms.Padding(2);
            this.listBoxEngines.Name = "listBoxEngines";
            this.listBoxEngines.Size = new System.Drawing.Size(365, 95);
            this.listBoxEngines.TabIndex = 12;
            this.listBoxEngines.SelectedIndexChanged += new System.EventHandler(this.listBoxEngines_SelectedIndexChanged);
            // 
            // txtEngine
            // 
            this.txtEngine.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtEngine.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtEngine.Location = new System.Drawing.Point(4, 17);
            this.txtEngine.Margin = new System.Windows.Forms.Padding(2);
            this.txtEngine.Name = "txtEngine";
            this.txtEngine.Size = new System.Drawing.Size(365, 20);
            this.txtEngine.TabIndex = 13;
            this.txtEngine.TextChanged += new System.EventHandler(this.txtEngine_TextChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.txtDomain);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage1.Size = new System.Drawing.Size(382, 369);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Credentials/Domain";
            // 
            // txtDomain
            // 
            this.txtDomain.Location = new System.Drawing.Point(11, 169);
            this.txtDomain.Margin = new System.Windows.Forms.Padding(2);
            this.txtDomain.Name = "txtDomain";
            this.txtDomain.Size = new System.Drawing.Size(326, 20);
            this.txtDomain.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(0, 149);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(375, 55);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Domain/URL:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkSaveCreds);
            this.groupBox1.Controls.Add(this.btnClearCreds);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(4, 5);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(375, 139);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Credentials:";
            // 
            // chkSaveCreds
            // 
            this.chkSaveCreds.AutoSize = true;
            this.chkSaveCreds.Location = new System.Drawing.Point(7, 75);
            this.chkSaveCreds.Margin = new System.Windows.Forms.Padding(2);
            this.chkSaveCreds.Name = "chkSaveCreds";
            this.chkSaveCreds.Size = new System.Drawing.Size(193, 17);
            this.chkSaveCreds.TabIndex = 14;
            this.chkSaveCreds.Text = "Save credentials for future sessions";
            this.chkSaveCreds.UseVisualStyleBackColor = true;
            this.chkSaveCreds.CheckedChanged += new System.EventHandler(this.chkSaveCreds_CheckedChanged);
            // 
            // btnClearCreds
            // 
            this.btnClearCreds.Location = new System.Drawing.Point(4, 96);
            this.btnClearCreds.Margin = new System.Windows.Forms.Padding(2);
            this.btnClearCreds.Name = "btnClearCreds";
            this.btnClearCreds.Size = new System.Drawing.Size(187, 19);
            this.btnClearCreds.TabIndex = 12;
            this.btnClearCreds.Text = "Clear saved credentials";
            this.btnClearCreds.UseVisualStyleBackColor = true;
            this.btnClearCreds.Click += new System.EventHandler(this.btnClearCreds_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(7, 38);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(2);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(326, 20);
            this.txtPassword.TabIndex = 8;
            this.txtPassword.TextChanged += new System.EventHandler(this.txtPassword_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 22);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "API Key:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(9, 10);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(390, 395);
            this.tabControl1.TabIndex = 5;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.aboutBox);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(382, 369);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "About";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // aboutBox
            // 
            this.aboutBox.Location = new System.Drawing.Point(30, 53);
            this.aboutBox.Margin = new System.Windows.Forms.Padding(2);
            this.aboutBox.Multiline = true;
            this.aboutBox.Name = "aboutBox";
            this.aboutBox.ReadOnly = true;
            this.aboutBox.Size = new System.Drawing.Size(302, 123);
            this.aboutBox.TabIndex = 7;
            this.aboutBox.Text = "Version: 2023.8.28";
            // 
            // ProviderConfDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(408, 444);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.bnt_OK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ProviderConfDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PangeaMT Provider Options";
            this.tabPage2.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
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
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.ListBox listBoxEngines;
        private System.Windows.Forms.TextBox txtEngine;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtDomain;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkSaveCreds;
        private System.Windows.Forms.Button btnClearCreds;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox aboutBox;
    }
}