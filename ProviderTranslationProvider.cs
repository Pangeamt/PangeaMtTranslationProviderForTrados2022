using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sdl.LanguagePlatform.Core;
using Sdl.LanguagePlatform.TranslationMemory;
using Sdl.LanguagePlatform.TranslationMemoryApi;
using PangeaMtTranslationProvider;

namespace PangeaMtTranslationProvider
{
    /// <summary>
    /// A necessary implementation of the <c>ITranslationProvider</c> interface
    /// </summary>
    public class ProviderTranslationProvider : ITranslationProvider
    {
        ///<summary>
        /// This string needs to be a unique value.
        /// This is the string that precedes the plug-in URI.
        ///</summary>    
        public static readonly string TranslationProviderScheme = "pangeamtprovider";

        /// <summary>
        /// The set of options associated with this provider
        /// </summary>
        public ProviderTranslationOptions Options
        {
            get;
            set;
        }

        /// <summary>
        /// Instantiated when a new Provider needs to be created and returned to Studio.
        /// </summary>
        /// <param name="options">The <see cref="ProviderTranslationOptions"/> object containing the set of options for the current translation provider.</param>
        public ProviderTranslationProvider(ProviderTranslationOptions options)
        {
            Options = options;
        }
        
        #region "ITranslationProvider Members"
        /// <summary>
        /// Gets the language direction of this provider.  A necessary interface method.
        /// </summary>
        /// <param name="languageDirection">A LanguagePair object</param>
        /// <returns>A <c>ProviderLanguageDirection</c> object.</returns>
        public ITranslationProviderLanguageDirection GetLanguageDirection(LanguagePair languageDirection)
        {
            return new ProviderLanguageDirection(this, languageDirection);
        }

        /// <summary>
        /// Whether or not the plugin can accept the "Update" option in the Trados Studio project options screen (not the options of the individual provider but in the list of all providers).
        /// A necessary interface method.
        /// </summary>
        public bool IsReadOnly
        {
            get { return true; }
        }

        /// <summary>
        /// A necessary interface method. It is not used in our implementation.
        /// </summary>
        /// <param name="translationProviderState">A string representing the serialized state.</param>
        public void LoadState(string translationProviderState)
        {
            
        }

        /// <summary>
        /// This is the name that will be associated with the translation provider in the SDL Trados menu.
        /// It is retrieved from our resources file.
        /// </summary>
        public string Name
        {
            get { return PluginResources.Plugin_NiceName; }
        }

        /// <summary>
        /// A necessary interface method, not used in our implementation.
        /// </summary>
        public void RefreshStatusInfo()
        {

        }

        /// <summary>
        /// A necessary interface method, not used in our implementation.
        /// </summary>
        /// <returns></returns>
        public string SerializeState()
        {
            // Save settings
            return null;
        }

        /// <summary>
        /// A necessary interface property, used by Trados Studio.
        /// </summary>
        public ProviderStatusInfo StatusInfo
        {
            get { return new ProviderStatusInfo(true, PluginResources.Plugin_NiceName); }
        }

        /// <summary>
        /// Whether or not the provider supports concordance searching.
        /// In our case it is true.
        /// </summary>
        public bool SupportsConcordanceSearch
        {
            get { return true; }
        }
        
        /// <summary>
        /// A necessary interface property, used by Trados Studio. Whether or not the provider supports Document Seraches.
        /// </summary>
        public bool SupportsDocumentSearches
        {
            get { return false; }
        }

        /// <summary>
        /// Whether or not the provider supports filters, in our case false.
        /// </summary>
        public bool SupportsFilters
        {
            get { return false; }
        }

        /// <summary>
        ///  Whether or not the provider supports fuzzy search, in our case false.
        ///  For some reason, even though this is set to false, Trados Studio will still send searches of the <c>FullSearch</c> type.
        ///  This has to be borne in mind when doing the search in the <see cref="ProviderLanguageDirection"/> class.
        /// </summary>
        public bool SupportsFuzzySearch
        {
            get { return false; }
        }
        

        /// <summary>
        /// In our case we are assuming that the user will select the appropriate language settings.
        /// So we just return true for this method
        /// </summary>
        public bool SupportsLanguageDirection(LanguagePair languageDirection)
        {
            return true;
        }
        

        /// <summary>
        /// In our implementation, we are only returning one result per translation request. So this is False.
        /// </summary>
        public bool SupportsMultipleResults
        {
            get { return false; }
        }
        
        /// <summary>
        /// Allows the user to specify a penalty to be applied for different translation providers, in our case it is false
        /// </summary>
        public bool SupportsPenalties
        {
            get { return false; }
        }
        
        /// <summary>
        /// Whether or not the provider supports placeables.  In our implementation we only deal with text and tags.
        /// </summary>
        public bool SupportsPlaceables
        {
            get { return false; }
        }

        /// <summary>
        /// Scoring is not applicable to our specific MT implementation.
        /// </summary>
        public bool SupportsScoring
        {
            get { return false; }
        }

        /// <summary>
        /// We are using it for searches, i.e., not just concordance, so this is true.
        /// </summary>
        public bool SupportsSearchForTranslationUnits
        {
            get { return true; }
        }
        
        /// <summary>
        /// Our implementation allows the user to do a source "concordance" search of selected text.
        /// Which is really just a translation request of the selected text alone, and not a true concordance search.
        /// </summary>
        public bool SupportsSourceConcordanceSearch
        {
            get { return true; }
        }

        /// <summary>
        /// A target concordance search is not applicable to our implementation.
        /// It could, in fact, be implemented but it is hard to see where the user would ever need to do a reverse lookup.
        /// </summary>
        public bool SupportsTargetConcordanceSearch
        {
            get { return false; }
        }
        
        /// <summary>
        /// False in our implementation.
        /// </summary>
        public bool SupportsStructureContext
        {
            get { return false; }
        }

        /// <summary>
        /// Our provider supports tagged input.
        /// </summary>
        public bool SupportsTaggedInput
        {
            get { return true; }
        }
        
        /// <summary>
        /// True in our implementation.
        /// </summary>
        public bool SupportsTranslation
        {
            get { return true; }
        }

        /// <summary>
        /// Our provider does not support updating, so it will not allow the user to select the Update box in the Studio list of providers.
        /// </summary>
        public bool SupportsUpdate
        {
            get { return false; }
        }
        
        /// <summary>
        /// False in our implementation.
        /// </summary>
        public bool SupportsWordCounts
        {
            get { return false; }
        }

        /// <summary>
        /// The translation method.
        /// </summary>
        public TranslationMethod TranslationMethod
        {
            get { return ProviderTranslationOptions.ProviderTranslationMethod; }
        }

        /// <summary>
        /// The URI of the provider.
        /// </summary>
        public Uri Uri
        {
            get { return Options.Uri; }
        }
        
        #endregion
    }
}

