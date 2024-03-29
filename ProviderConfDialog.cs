﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sdl.LanguagePlatform.TranslationMemoryApi;

namespace PangeaMtTranslationProvider
{
    /// <summary>
    /// This form is for entering user settings.
    /// </summary>
    public partial class ProviderConfDialog : Form
    {

        private List<PangeaEngine> enginesList;
        private string txtSourceLang;
        private string txtTargetLang;

        private ITranslationProviderCredentialStore credentialStore;
        
        #region "ProviderConfDialog"
        /// <summary>
        /// This form is for entering user settings.
        /// </summary>
        /// <param name="options">The set of options that this form will read/write.</param>
        public ProviderConfDialog(ProviderTranslationOptions options)
        {
            Options = options;
            InitializeComponent();
            UpdateDialog();
        }

        /// <summary>
        /// This form is for entering user settings.
        /// </summary>
        /// <param name="options">The set of options that this form will read/write.</param>
        /// <param name="credentialStore">A credential store object of the SDL library for storing credentials.</param>
        public ProviderConfDialog(ProviderTranslationOptions options, ITranslationProviderCredentialStore credentialStore)
        {
            this.credentialStore = credentialStore;
            Options = options;
            InitializeComponent();
            UpdateDialog();
        }

        /// <summary>
        /// Gets/sets the set of options that this form will reads/writes.
        /// </summary>
        public ProviderTranslationOptions Options
        {
            get;
            set;
        }
        #endregion

        /// <summary>
        /// Used to read credentials from the credential store.
        /// </summary>
        /// <param name="credentialStore">The credential store to read.</param>
        /// <returns>The credential from the credential store.</returns>
        private TranslationProviderCredential GetMyCredentials(ITranslationProviderCredentialStore credentialStore)
        {
            Uri myUri = new Uri("pangeamtprovider:///");
            TranslationProviderCredential cred = null;

            if (credentialStore.GetCredential(myUri) != null)
            {

                //get the credential to return
                cred = new TranslationProviderCredential(credentialStore.GetCredential(myUri).Credential, credentialStore.GetCredential(myUri).Persist);
            }

            return cred;

        }


        /// <summary>
        /// Used to set credentials in the credential store.
        /// </summary>
        /// <param name="credentialStore">The credential store to write to.</param>
        /// <param name="creds">The credential to write</param>
        /// <param name="persistCred">Whether or not the credentials should persist</param>
        private void SetMyCredentials(ITranslationProviderCredentialStore credentialStore, GenericCredentials creds, bool persistCred)
        { //used to set credentials
            // we are only setting and getting credentials for the uri with no parameters...kind of like a master credential
            Uri myUri = new Uri("pangeamtprovider:///");

            TranslationProviderCredential cred = new TranslationProviderCredential(creds.ToCredentialString(), persistCred);
            credentialStore.RemoveCredential(myUri);
            credentialStore.AddCredential(myUri, cred);


        }
        
        
        
        #region "UpdateDialog"
        /// <summary>
        /// Updates the dialog on load
        /// </summary>
        private void UpdateDialog()
        {
            
            
            ////get saved creds if there are any and put into options
            TranslationProviderCredential getCred = GetMyCredentials(credentialStore);
            if (getCred != null)
            {
                try
                {
                    GenericCredentials creds = new GenericCredentials(getCred.Credential); //parse credential into username and password
                    Options.un = creds.UserName;
                    Options.pwd = creds.Password;
                }
                catch { }  //if errors occur just don't load creds

            }
            
            
            //set controls based on options
            txtPassword.Text = Options.pwd;
            if (Options.domain==null || Options.domain.Equals("")) //if domain is empty try to get from settings
            {
                try { txtDomain.Text = Properties.Settings.Default.Domain; }
                catch { }
            }
            else
            {
                txtDomain.Text = Options.domain;
            }

            txtEngine.Text = Options.engineName;
            txtSourceLang = Options.sourceLang;
            txtTargetLang = Options.targetLang;

            chkUseGlossary.Checked = Options.useGlossary;
            txtGlossaryFile.Text = Options.glossaryFileName;
            chkResendDrafts.Checked = Options.resendDrafts;
            chkPlainTextOnly.Checked = Options.sendPlainTextOnly;
            chkSaveCreds.Checked = Options.saveCredentials;

            
            //enable/disable controls based on saved options 
            txtGlossaryFile.Enabled = Options.useGlossary;
            btnBrowseGlossary.Enabled = Options.useGlossary;

            //try to get list of engines
            if (!txtDomain.Text.Equals(""))
                try { GetEnginesList(4000); }
                catch { }

            // try loading glossary
            if (!txtGlossaryFile.Text.Equals("") && chkUseGlossary.Checked)
            {
                try
                {
                    Options.glossaryContent = PangeaConnecter.LoadGlossary(txtGlossaryFile.Text);
                } catch { }
            }


        }
        #endregion


        /// <summary>
        /// Gets the list of engines to put in the form
        /// </summary>
        /// <param name="timeout">The timeout to be used for the connection</param>
        private void GetEnginesList(int timeout)
        {
            //get the engines list
            enginesList = PangeaConnecter.GetEnginesList(txtPassword.Text, txtDomain.Text, timeout);
            // DEBUG
            // enginesList.Add(new PangeaEngine("1147", "en", "de", "ENDE_TMP"));
            
            //now fill the list box
            if (enginesList != null) 
            { //in case of error
                var autocompleteSource = new AutoCompleteStringCollection();
                foreach (PangeaEngine entry in enginesList)
                {
                    listBoxEngines.Items.Add(entry.title);
                    autocompleteSource.Add(entry.title);
                }
                txtEngine.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtEngine.AutoCompleteCustomSource = autocompleteSource;
            }


            //select item if the text box already has one populated from options and it is in list returned
            if (!txtEngine.Text.Equals(""))
                for (int i = 0; i < listBoxEngines.Items.Count; i++)
                    if (listBoxEngines.Items[i].ToString().Equals(txtEngine.Text))
                        listBoxEngines.SetSelected(i, true);

        }
                
        
        
        #region "OK"
        //ok button
        private void bnt_OK_Click(object sender, EventArgs e)
        {
            
            //first run our validation checks
            if (!validateForm())
                return;
            
            //otherwise....

            //update options based on form settings
            Options.pwd = txtPassword.Text;
            Options.saveCredentials = chkSaveCreds.Checked;
            if (chkSaveCreds.Checked) //if user selects to save credentials
            {
                //set credentials to save in store
                GenericCredentials creds2 = new GenericCredentials("", txtPassword.Text);
                SetMyCredentials(credentialStore, creds2, true);
            }
            else //otherwise clear any saved ones
            {
                Uri myUri = new Uri("pangeamtprovider:///");
                if (credentialStore.GetCredential(myUri) != null)
                    credentialStore.RemoveCredential(myUri);
            }

            Options.domain = txtDomain.Text;

            //update user settings to save domain
            try { Properties.Settings.Default.Domain = txtDomain.Text; }
            catch { }

            Options.engineName = txtEngine.Text;
            //get id to save for the selected engine from our list

            if (enginesList != null) //this will be null in some cases, but should also be unnecessary since the id will have already been set
                foreach (PangeaEngine en in enginesList)
                    if (en.title.Equals(txtEngine.Text))
                        Options.engineID = en.id;
                
            Options.sourceLang = txtSourceLang;
            Options.targetLang = txtTargetLang;
            Options.useGlossary = chkUseGlossary.Checked;
            Options.glossaryFileName = txtGlossaryFile.Text;
            Options.resendDrafts = chkResendDrafts.Checked;
            Options.sendPlainTextOnly = chkPlainTextOnly.Checked;

            

            //......close form with dialog result ok
            this.DialogResult = DialogResult.OK;
            this.Close();
            



        }
        #endregion

        private void btn_Cancel_Click(object sender, EventArgs e)
        {

        }

        
        private void btnClearCreds_Click(object sender, EventArgs e)
        {
            Options.un = "";
            Options.pwd = "";

            txtPassword.Text = "";
            
            Uri myUri = new Uri("pangeamtprovider:///");
            if (credentialStore.GetCredential(myUri) != null)
            {
                credentialStore.RemoveCredential(myUri);
                string prompt = "Credentials cleared";
                MessageBox.Show(prompt);
            }
            else
            {
                string prompt = "Credentials already empty";
                MessageBox.Show(prompt);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            listBoxEngines.Items.Clear(); //clear existing

            try { GetEnginesList(10000); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void chkUseGlossary_CheckedChanged(object sender, EventArgs e)
        {
            //enable/disable controls based on user selection 
            txtGlossaryFile.Enabled = chkUseGlossary.Checked;
            btnBrowseGlossary.Enabled = chkUseGlossary.Checked;
        }

        /// <summary>
        /// Runs checks to make sure form can be validated
        /// </summary>
        /// <returns>True if user input is valid, and false otherwise.</returns>
        private bool validateForm()
        {
            //this only checks that some value has been entered
            //more specific validation tasks can be added as needed
            //could even add a test connection to server to check validity up front
            
            string prompt = "The following errors occurred:" + System.Environment.NewLine;
            bool validate = true;

            //check for glossary filename if selected
            if (chkUseGlossary.Checked && txtGlossaryFile.Text.Equals(""))
            {
                prompt += System.Environment.NewLine + "Please choose a glossary file or disable the glossary option";
                validate = false;
            }
            //check for credentials
            if (txtPassword.Text.Equals(""))
            {
                prompt += System.Environment.NewLine + "Please enter a valid API key";
                validate = false;
            }
            //check for a domain
            if (txtDomain.Text.Equals("")) //this could be changed to a regex to check for validity
            {
                prompt += System.Environment.NewLine + "Please enter a valid domain/url";
                validate = false;
            }
            //check for engine
            if (txtEngine.Text.Equals(""))
            {
                prompt += System.Environment.NewLine + "Please select an available MT engine";
                validate = false;
            }

            if (!validate)
            {
                MessageBox.Show(prompt);
                return false;
            }
            else
                return true;
         
        }

        private void btnBrowseGlossary_Click(object sender, EventArgs e)
        {
            openFile.FileName = "";
            openFile.Title = "Select a glossary file to use...";
            openFile.Filter = "Glossary files|*.txt";
            openFile.ShowDialog(); //show open file dialog to get glossary file
            string filename = openFile.FileName;

            if (filename != "")
            {
                txtGlossaryFile.Text = filename; //set to textbox
                Options.glossaryContent = PangeaConnecter.LoadGlossary(txtGlossaryFile.Text);
            }
            else return; //have to get out

            //test for valid file here???
            
        }
        

        private void listBoxEngines_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (PangeaEngine entry in enginesList)
                if (listBoxEngines.SelectedItem.ToString().Equals(entry.title))
                {
                    txtEngine.Text = entry.title;
                    txtSourceLang = entry.lang1;
                    txtTargetLang = entry.lang2;
                }

            //update source and target languages
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void chkSaveCreds_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtEngine_TextChanged(object sender, EventArgs e)
        {
            if (txtEngine.AutoCompleteCustomSource.Contains(txtEngine.Text))
            {
                listBoxEngines.SelectedIndexChanged -= listBoxEngines_SelectedIndexChanged;
                for (int i = 0; i < listBoxEngines.Items.Count; i++)
                    if (listBoxEngines.Items[i].ToString().Equals(txtEngine.Text))
                        listBoxEngines.SetSelected(i, true);
                listBoxEngines.SelectedIndexChanged += listBoxEngines_SelectedIndexChanged;

            }


            /*            if (!txtEngine.Text.Equals(""))
                            for (int i = 0; i < listBoxEngines.Items.Count; i++)
                                if (listBoxEngines.Items[i].ToString().Contains(txtEngine.Text))
                                    listBoxEngines.SetSelected(i, true);
            */
        }
    }
}
