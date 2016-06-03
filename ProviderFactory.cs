using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sdl.LanguagePlatform.Core;
using Sdl.LanguagePlatform.TranslationMemory;
using Sdl.LanguagePlatform.TranslationMemoryApi;
using PangeaMtTranslationProvider;

namespace PangeaMtTranslationProvider
{
    #region "Declaration"
    /// <summary>
    /// This class is a necessary implementation of the <c>ITranslationProviderFactory</c> interface.  Its methods are called by Trados Studio at various times.  
    /// </summary>
    [TranslationProviderFactory(
        Id = "TranslationProviderFactory",
        Name = "TranslationProviderFactory",
        Description = "A template for creating translation providers")]
    #endregion

    public class ProviderFactory : ITranslationProviderFactory
    {
        #region ITranslationProviderFactory Members

        #region "CreateTranslationProvider"
        /// <summary>
        /// This method is called by Trados Studio at various times, including when first adding the plugin to a project.  We also use it to call our settings form when no credentials are set.
        /// </summary>
        /// <param name="translationProviderUri">Passed by Trados Studio to the plugin.</param>
        /// <param name="translationProviderState">Passed by Trados Studio to the plugin.</param>
        /// <param name="credentialStore">Passed by Trados Studio to the plugin.</param>
        /// <returns>Returns the created <c>TranslationProvider</c> to Trados Studio</returns>
        public ITranslationProvider CreateTranslationProvider(Uri translationProviderUri, string translationProviderState, ITranslationProviderCredentialStore credentialStore)
        {
            if (!SupportsTranslationProviderUri(translationProviderUri))
            {
                throw new Exception("Cannot handle URI.");
            }

            ProviderTranslationProvider tp = new ProviderTranslationProvider(new ProviderTranslationOptions(translationProviderUri));

            Uri credsUri = new Uri("pangeamtprovider:///"); //we are only setting and getting credential based on uri format without parameters
            //bool to check whether there are any stored creds, i.e. if not stored and also no value is stored in options for the current session
            //need to check options for current session in addition to credential store because Studio calls this method
            //multiple times..when changing settings, when opening a file..etc. and user may have set creds without saving them to the store

            bool noSavedCreds = credentialStore.GetCredential(credsUri) == null && (tp.Options.un == null || tp.Options.un.Equals(""))
                && (tp.Options.pwd == null || tp.Options.pwd.Equals(""));

            //if (System.Diagnostics.Debugger.IsAttached)
                //System.Diagnostics.Debugger.Break();

            if (noSavedCreds)
            {
                ProviderConfDialog dialog = new ProviderConfDialog(tp.Options, credentialStore);
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    tp.Options = dialog.Options;
                    return tp;
                }
            }
            else
            {
                //try to get credentials from store
                try
                {
                    GenericCredentials creds = new GenericCredentials(new TranslationProviderCredential(credentialStore.GetCredential(credsUri).Credential, credentialStore.GetCredential(credsUri).Persist).Credential);
                    //put in options
                    tp.Options.un = creds.UserName;
                    tp.Options.pwd = creds.Password;
                }
                catch { } //just create the provider..not absolutely necessary to throw exception here..if credentials are bad at this point a web exception will occur and be reported to the user when trying to translate
            }
            return tp;
        }
        #endregion

        #region "SupportsTranslationProviderUri"
        /// <summary>
        /// A necessary interface method that can be used to specify whether the URI passed is supported.
        /// In our cases makes sure it is not null and that the base URI is proper.
        /// </summary>
        /// <param name="translationProviderUri">A <see cref="Uri"/> object representing the current translation provider.</param>
        /// <returns>True if the URI is proper and false otherwise.</returns>
        public bool SupportsTranslationProviderUri(Uri translationProviderUri)
        {
            if (translationProviderUri == null)
            {
                throw new ArgumentNullException("Translation provider URI not supported.");
            }
            return String.Equals(translationProviderUri.Scheme, ProviderTranslationProvider.TranslationProviderScheme, StringComparison.OrdinalIgnoreCase);
        }
        #endregion

        #region "GetTranslationProviderInfo"
        /// <summary>
        /// A necessary interface method that returns information about the translation provider
        /// </summary>
        /// <param name="translationProviderUri">A <see cref="Uri"/> object representing the current translation provider.</param>
        /// <param name="translationProviderState">A string that can be used to pass the serialized state of the provider.</param>
        /// <returns>A <c>TranslationProviderInfo</c> object containing the necessary information.</returns>
        public TranslationProviderInfo GetTranslationProviderInfo(Uri translationProviderUri, string translationProviderState)
        {
            TranslationProviderInfo info = new TranslationProviderInfo();

            #region "TranslationMethod"
            info.TranslationMethod = ProviderTranslationOptions.ProviderTranslationMethod;
            #endregion

            #region "Name"
            info.Name = PluginResources.Plugin_NiceName;
            #endregion

            return info;
        }
        #endregion

        #endregion
    }
}
