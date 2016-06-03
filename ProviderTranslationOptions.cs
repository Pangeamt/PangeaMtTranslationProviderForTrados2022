using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sdl.LanguagePlatform.TranslationMemoryApi;
using System.Windows.Forms;

namespace PangeaMtTranslationProvider
{
    /// <summary>
    /// This class is used to hold the provider plug-in settings. 
    /// All settings are automatically stored in a URI.
    /// </summary>
    public class ProviderTranslationOptions
    {

        private static string _un; //username
        private static string _pwd; //password  -- only persist for the session and are never saved to credential store or URI from this class
        
        
        #region "TranslationMethod"
        /// <summary>
        /// The translation provider method.  In our case it is MachineTranslation
        /// </summary>
        public static readonly TranslationMethod ProviderTranslationMethod = TranslationMethod.MachineTranslation;
        #endregion

        #region "TranslationProviderUriBuilder"
        TranslationProviderUriBuilder _uriBuilder;

        /// <summary>
        /// An instance of this class is used to hold the provider plugin-settings. 
        /// </summary>
        public ProviderTranslationOptions()
        {
            _uriBuilder = new TranslationProviderUriBuilder(ProviderTranslationProvider.TranslationProviderScheme);
        }

        /// <summary>
        /// An instance of this class is used to hold the provider plugin-settings.
        /// </summary>
        /// <param name="uri">Takes a URI containing previously set parameters and values for the current options instance.</param>
        public ProviderTranslationOptions(Uri uri)
        {
            _uriBuilder = new TranslationProviderUriBuilder(uri);
        }
        #endregion

        /// <summary>
        /// The username...not stored in the URI...only persists for the current work session unless saved in the credential store
        /// </summary>
        internal string un
        {
            get { return _un; } //the creds are going to be held in static variables so we don't have to get it from credential store all the time
            set { _un = value; }
        }

        /// <summary>
        /// The password...not stored in the URI...only persists for the current work session unless saved in the credential store
        /// </summary>
        internal string pwd
        {
            get { return _pwd; }
            set { _pwd = value; }
        }

        /// <summary>
        /// The engine name.
        /// </summary>
        internal string engineName
        {
            get { return GetStringParameter("enginename"); }
            set { SetStringParameter("enginename", value); }
        }

        /// <summary>
        /// The engine ID.
        /// </summary>
        internal string engineID
        {
            get { return GetStringParameter("engineid"); }
            set { SetStringParameter("engineid", value); }
        }
        
        /// <summary>
        /// The domain.
        /// </summary>
        internal string domain
        {
            get { return GetStringParameter("domain"); }
            set { SetStringParameter("domain", value); }
        }

        /// <summary>
        /// The source language.
        /// </summary>
        internal string sourceLang
        {
            get { return GetStringParameter("sourcelang"); }
            set { SetStringParameter("sourcelang", value); }
        }
        
        /// <summary>
        /// The target language.
        /// </summary>
        internal string targetLang
        {
            get { return GetStringParameter("targetlang"); }
            set { SetStringParameter("targetlang", value); }
        }
        
        
        #region useGlossary
        
        /// <summary>
        /// Whether or not to use a glossary file.
        /// </summary>
        internal bool useGlossary
        {
            get { return Convert.ToBoolean(GetStringParameter("useglossary")); }
            set { SetStringParameter("useglossary", value.ToString()); }
        }
        /// <summary>
        /// A glossary file path to be used if useGlossary is true.
        /// </summary>
        internal string glossaryFileName
        {
            get { return GetStringParameter("glossaryfile"); }
            set { SetStringParameter("glossaryfile", value); }
        }
        
        #endregion

        /// <summary>
        /// Whether or not to send only plain text (i.e., no tags) to Pangea.
        /// </summary>
        internal bool sendPlainTextOnly
        {
            get { return Convert.ToBoolean(GetStringParameter("sendplaintextonly")); }
            set { SetStringParameter("sendplaintextonly", value.ToString()); }
        }

        /// <summary>
        /// Whether or not to save the credentials that the user has entered in the credential store.
        /// </summary>
        internal bool saveCredentials
        {
            get { return Convert.ToBoolean(GetStringParameter("savecredentials")); }
            set { SetStringParameter("savecredentials", value.ToString()); }
        }

        /// <summary>
        /// Whether or not to re-send to Pangea any segments whose confirmation status is other than "Untranslated"
        /// </summary>
        internal bool resendDrafts
        {
            get { return Convert.ToBoolean(GetStringParameter("resenddrafts")); }
            set { SetStringParameter("resenddrafts", value.ToString()); }
        }

        #region "SetStringParameter"
        private void SetStringParameter(string p, string value)
        {
            _uriBuilder[p] = value;
        }
        #endregion

        #region "GetStringParameter"
        private string GetStringParameter(string p)
        {
            string paramString = _uriBuilder[p];
            return paramString;
        }
        #endregion


        #region "Uri"
        /// <summary>
        /// The URI resulting from putting all the paramters and values together
        /// </summary>
        internal Uri Uri
        {
            get
            {
                return _uriBuilder.Uri;
            }
        }
        #endregion
    }
}
