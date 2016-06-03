using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sdl.LanguagePlatform.Core;
using Sdl.LanguagePlatform.TranslationMemory;
using Sdl.LanguagePlatform.TranslationMemoryApi;

namespace PangeaMtTranslationProvider
{
    #region "Declaration"
    /// <summary>
    /// A necessary implemention of the ITranslationProviderWinFormsUI interface.
    /// </summary>
    [TranslationProviderWinFormsUi(
        Id = "ProviderWinFormsUI",
        Name = "ProviderWinFormsUI",
        Description = "ProviderWinFormsUI")]
    #endregion
    
    public class ProviderWinFormsUI : ITranslationProviderWinFormsUI
    {
        #region ITranslationProviderWinFormsUI Members

        
        /// <summary>
        /// Show the plug-in settings form when the user is adding the translation provider plug-in
        /// through the GUI of SDL Trados Studio
        /// </summary>
        /// <param name="owner">Passed by Studio.</param>
        /// <param name="languagePairs">Passed by Studio.</param>
        /// <param name="credentialStore">Passed by Studio.</param>
        /// <returns>An array of <c>ITranslationProvider</c> implementations, in our case it will just be the one.</returns>
        public ITranslationProvider[] Browse(IWin32Window owner, LanguagePair[] languagePairs, ITranslationProviderCredentialStore credentialStore)
        {

            //instantiate a set of blank options to send to new form
            ProviderTranslationOptions loadOptions = new ProviderTranslationOptions();


            
            
            
            ProviderConfDialog dialog = new ProviderConfDialog(loadOptions, credentialStore);
            if (dialog.ShowDialog(owner) == DialogResult.OK)
            {
                ProviderTranslationProvider testProvider = new ProviderTranslationProvider(dialog.Options);
                return new ITranslationProvider[] { testProvider };
            }
            return null;
        }
        


        /// <summary>
        /// Determines whether the plug-in settings can be changed
        /// by displaying the Settings button in SDL Trados Studio.
        /// </summary>
        public bool SupportsEditing
        {
            get { return true; }
        }
        
        /// <summary>
        /// If the plug-in settings can be changed by the user,
        /// SDL Trados Studio will display a Settings button.
        /// By clicking this button, users raise the plug-in user interface,
        /// in which they can modify any applicable settings.
        /// </summary>
        /// <param name="owner">Passed by Studio.</param>
        /// <param name="translationProvider">Passed by Studio.</param>
        /// <param name="languagePairs">Passed by Studio.</param>
        /// <param name="credentialStore">Passed by Studio.</param>
        /// <returns>True if the settings change was confirmed by the user and false otherwise.</returns>
        public bool Edit(IWin32Window owner, ITranslationProvider translationProvider, LanguagePair[] languagePairs, ITranslationProviderCredentialStore credentialStore)
        {
            ProviderTranslationProvider editProvider = translationProvider as ProviderTranslationProvider;
            if (editProvider == null)
            {
                return false;
            }

                        
            ProviderConfDialog dialog = new ProviderConfDialog(editProvider.Options, credentialStore);
            if (dialog.ShowDialog(owner) == DialogResult.OK)
            {
                editProvider.Options = dialog.Options;
                return true;
            }

            return false;
        }
        
        /// <summary>
        /// Can be used in implementations in which a user login is required, e.g.
        /// for connecting to an online translation provider.
        /// It is called when a TranslationProviderAuthenticationException is thrown.
        /// In this implementation it is not used, because no Provider instance is passed
        /// Making it impossible to change other options for the current provider when the form opens
        /// Instead, we just open the form from the factory if credentials are not set.
        /// That way the user can update options at the same time
        /// </summary>
        /// <param name="owner">The current process owning the window. This is passed from Studio.</param>
        /// <param name="translationProviderUri">The URI of the current translation provider.</param>
        /// <param name="translationProviderState">The serialized state of the provider, if applicable.</param>
        /// <param name="credentialStore">A credential store object.</param>
        /// <returns>True if the process is confirmed by the user and false otherwise.</returns>
        public bool GetCredentialsFromUser(IWin32Window owner, Uri translationProviderUri, string translationProviderState, ITranslationProviderCredentialStore credentialStore)
        {
            return true;
        }
        
        /// <summary>
        /// Used for displaying the plug-in info such as the plug-in name,
        /// tooltip, and icon.
        /// </summary>
        /// <param name="translationProviderUri">Passed by Studio.</param>
        /// <param name="translationProviderState">Passed by Studio.</param>
        /// <returns>Display info for the provider.</returns>
        public TranslationProviderDisplayInfo GetDisplayInfo(Uri translationProviderUri, string translationProviderState)
        {
            TranslationProviderDisplayInfo info = new TranslationProviderDisplayInfo();
            info.Name = PluginResources.Plugin_NiceName;
            info.TranslationProviderIcon = PluginResources.band_aid_icon;
            info.TooltipText = PluginResources.Plugin_Tooltip;

            info.SearchResultImage = PluginResources.band_aid_symbol;

            return info;
        }
        


        /// <summary>
        /// A necessary interface method.
        /// </summary>
        /// <param name="translationProviderUri">The URI of the provider</param>
        /// <returns>True if srrported and false otherwise.</returns>
        public bool SupportsTranslationProviderUri(Uri translationProviderUri)
        {
            if (translationProviderUri == null)
            {
                throw new ArgumentNullException("URI not supported by the plug-in.");
            }
            return String.Equals(translationProviderUri.Scheme, ProviderTranslationProvider.TranslationProviderScheme, StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// A necessary interface method. Retrieves the plugin description for the Studio UI.
        /// </summary>
        public string TypeDescription
        {
            get { return PluginResources.Plugin_Description; }
        }

        /// <summary>
        /// A necessary interface method. Retrieves the plugin name for the Studio UI.
        /// </summary>
        public string TypeName
        {
            get { return PluginResources.Plugin_NiceName; }
        }

        #endregion
    }
}
