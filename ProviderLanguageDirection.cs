using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sdl.LanguagePlatform.Core;
using Sdl.Core.Globalization;
using Sdl.LanguagePlatform.TranslationMemory;
using Sdl.LanguagePlatform.TranslationMemoryApi;

namespace PangeaMtTranslationProvider
{
    /// <summary>
    /// This class is a necessary implementation of the <c>ITranslationProviderLanguageDirection</c> interface.
    /// It is intantiated and its methods called by SDL studio at various times to perform searches.
    /// It is the starting point for all Pangea translation requests. 
    /// </summary>
    public class ProviderLanguageDirection : ITranslationProviderLanguageDirection
    {
        #region "PrivateMembers"
        private ProviderTranslationProvider _provider;
        private LanguagePair _languageDirection;
        private ProviderTranslationOptions _options;
        private TranslationUnit inputTu;
        #endregion

        #region "ITranslationProviderLanguageDirection Members"

        /// <summary>
        /// Instantiated by Trados Studio.
        /// </summary>
        /// <param name="provider">Passed by Trados Studio.</param>
        /// <param name="languages">Passed by Trados Studio.</param>
        #region "ProviderTranslationProviderLanguageDirection"
        public ProviderLanguageDirection(ProviderTranslationProvider provider, LanguagePair languages)
        {
            #region "Instantiate"
            _provider = provider;
            _languageDirection = languages;
            _options = _provider.Options;
            #endregion

            
        }

        #endregion

        /// <summary>
        /// A <see cref="System.Globalization.CultureInfo"/> object representing the source language.
        /// </summary>
        public System.Globalization.CultureInfo SourceLanguage
        {
            get { return _languageDirection.SourceCulture; }
        }

        /// <summary>
        /// A <see cref="System.Globalization.CultureInfo"/> object representing the target language.
        /// </summary>
        public System.Globalization.CultureInfo TargetLanguage
        {
            get { return _languageDirection.TargetCulture; }
        }

        /// <summary>
        /// An <c>ITranslationProvider</c> implementation representing the current translation provider.
        /// </summary>
        public ITranslationProvider TranslationProvider
        {
            get { return _provider; }
        }

        /// <summary>
        /// Performs the actual search.
        /// <para>Depending on the search mode, a segment lookup (with exact matching) or a source / target
        /// concordance search is done</para>.
        /// </summary>
        /// <param name="settings">A <c>SearchSettings</c> object passed by Trados Studio with the settings for the current search.</param>
        /// <param name="segment">A <c>Segment</c> object representing the current source segment.</param>
        /// <returns></returns>
        #region "SearchSegment"
        public SearchResults SearchSegment(SearchSettings settings, Segment segment)
        {
            

            #region "SearchResultsObject"
            SearchResults results = new SearchResults();
            results.SourceSegment = segment.Duplicate();
            #endregion

            //declare a target segment
            Segment targetSegment = null;

            #region "ConcordanceSearch"
            //if concordance do simple search and return
            if (settings.Mode == SearchMode.ConcordanceSearch)
            {
                targetSegment = new Segment();
                string targetText = PangeaConnecter.GetTranslation(segment.ToPlain(), false, _options);
                targetSegment.Add(targetText);
                results.Add(CreateSearchResult(segment, targetSegment));
                return results;
            }
            #endregion



            // Look up the currently selected segment on MT server.
            // In this implementation the lookup is done for both normal search and concordance search
            // for concordance search, no tag placement is done, even if tag option is checked...presumably b.c. Studio sends the source
            // text in a segment containing no tags for concordance search.
            #region "SegmentLookup"
            if (inputTu!=null && inputTu.ConfirmationLevel != ConfirmationLevel.Unspecified && !_options.resendDrafts) //if the segment is other than untranslated and we don't want to re-send them, just get out
            {
                return null;
            }
            else if (segment.HasTags && !_options.sendPlainTextOnly) //if we are dealing with tags and segment has tags, we need to place them
            {
                string targetText = PangeaConnecter.GetTranslation(TagPlacer.PreparedSourceText(segment), true, _options); //prepared source text from TagPlacer helps pangea deal with tags
                targetSegment = TagPlacer.GetTaggedTargetSegment(targetText, segment);
            }
            else //otherwise just add the text returned by portage directly to a target segment
            {
                targetSegment = new Segment();
                string targetText = PangeaConnecter.GetTranslation(segment.ToPlain(), false, _options);
                targetSegment.Add(targetText);
            }
            
            results.Add(CreateSearchResult(segment, targetSegment));
            return results;
            #endregion

        }
        #endregion

        /// <summary>
        /// Creates a search result to send up to Trados Studio with attributes appropriate to an MT translation provider.
        /// </summary>
        /// <param name="sourceSegment">The source segment.</param>
        /// <param name="targetSegment">The target segment.</param>
        /// <returns>A SearchResult to be added to a SearchResults object.</returns>
        private SearchResult CreateSearchResult(Segment sourceSegment, Segment targetSegment)
        {
            TranslationUnit tu = new TranslationUnit();
            tu.SourceSegment = sourceSegment;
            tu.TargetSegment = targetSegment;
            tu.ResourceId = new PersistentObjectToken(tu.GetHashCode(), Guid.Empty);
            tu.Origin = TranslationUnitOrigin.MachineTranslation;
            tu.ConfirmationLevel = ConfirmationLevel.Draft;
            int score = 0;
            //the score to be applied to the search results...affects the order the result appears in the results from all providers
            //assumed to be 0 to return MT only if not found by other TMs..etc.. this can be changed as needed or even made into a user-selected option
            SearchResult searchResult = new SearchResult(tu);
            searchResult.ScoringResult = new ScoringResult();
            searchResult.ScoringResult.BaseScore = score;
            return searchResult;
        }
        
        /// <summary>
        /// Whether or not the provider can reverse language direction.
        /// This is not applicable to our implementation, since we can set and reverse the language direction as needed in our user settings form.
        /// </summary>
        public bool CanReverseLanguageDirection
        {
            get { return false; }
        }

        /// <summary>
        /// Called by Trados Studio in certain circumstances.
        /// </summary>
        /// <param name="settings">A <c>SearchSettings</c> object passed by Trados Studio with the settings for the current search.</param>
        /// <param name="segments">An array of <c>Segment</c> objects representing the source segments to be searched.</param>
        /// <returns>An array of <c>SearchResults</c> objects with the results of looking up the segments.</returns>
        public SearchResults[] SearchSegments(SearchSettings settings, Segment[] segments)
        {
            SearchResults[] results = new SearchResults[segments.Length];
            for (int p = 0; p < segments.Length; ++p)
            {
                results[p] = SearchSegment(settings, segments[p]);
            }
            return results;
        }

        /// <summary>
        /// Called by Trados Studio in certain circumstances.
        /// </summary>
        /// <param name="settings">A <c>SearchSettings</c> object passed by Trados Studio with the settings for the current search.</param>
        /// <param name="segments">An array of <c>Segment</c> objects representing the source segments to be searched.</param>
        /// <param name="mask">A value provided by Trados Studio.  False means it is not to be translated.</param>
        /// <returns>An array of <c>SearchResults</c> objects with the results of looking up the segments.</returns>
        public SearchResults[] SearchSegmentsMasked(SearchSettings settings, Segment[] segments, bool[] mask)
        {
            
            
            if (segments == null)
            {
                throw new ArgumentNullException("segments in SearchSegmentsMasked");
            }
            if (mask == null || mask.Length != segments.Length)
            {
                throw new ArgumentException("mask in SearchSegmentsMasked");
            }

            SearchResults[] results = new SearchResults[segments.Length];
            for (int p = 0; p < segments.Length; ++p)
            {
                if (mask[p])
                {
                    results[p] = SearchSegment(settings, segments[p]);
                }
                else
                {
                    results[p] = null;
                }
            }

            return results;
        }

        /// <summary>
        /// Called by Trados Studio in certain circumstances.
        /// </summary>
        /// <param name="settings">A <c>SearchSettings</c> object passed by Trados Studio with the settings for the current search.</param>
        /// <param name="segment">A <c>Segment</c> object representing the current source segment.</param>
        /// <returns>A <c>SearchResults</c> object containg the results of the current lookup.</returns>
        public SearchResults SearchText(SearchSettings settings, string segment)
        {
            Segment s = new Sdl.LanguagePlatform.Core.Segment(_languageDirection.SourceCulture);
            s.Add(segment);
            return SearchSegment(settings, s);
        }

        /// <summary>
        /// A necessary interface method called from other methods in this class to do a lookup for an individual <c>TranslationUnit</c> object.
        /// </summary>
        /// <param name="settings">A <c>SearchSettings</c> object passed by Trados Studio with the settings for the current search.</param>
        /// <param name="translationUnit">A <c>TranslationUnit</c> object containing a <c>Segment</c> to look up.</param>
        /// <returns>A <c>SearchResults</c> object containg the results of the current lookup.</returns>
        public SearchResults SearchTranslationUnit(SearchSettings settings, TranslationUnit translationUnit)
        {
            //need to use the tu confirmation level in searchsegment method
            inputTu = translationUnit;
            return SearchSegment(settings, translationUnit.SourceSegment);
        }

        /// <summary>
        /// Called by Trados Studio in certain circumstances.
        /// </summary>
        /// <param name="settings">A <c>SearchSettings</c> object passed by Trados Studio with the settings for the current search.</param>
        /// <param name="translationUnits">An array of <c>TranslationUnit</c> objects, each containing a <c>Segment</c> to look up.</param>
        /// <returns>An array of <c>SearchResults</c> object containg the results of the current lookup.</returns>
        public SearchResults[] SearchTranslationUnits(SearchSettings settings, TranslationUnit[] translationUnits)
        {
            


            SearchResults[] results = new SearchResults[translationUnits.Length];
            for (int p = 0; p < translationUnits.Length; ++p)
            {
                //need to use the tu confirmation level in searchsegment method
                inputTu = translationUnits[p]; 
                results[p] = SearchSegment(settings, translationUnits[p].SourceSegment);
            }
            return results;
        }

        /// <summary>
        /// This method is used to call Pangea once for every 10 segments when in batch mode, intead of one at a time.
        /// </summary>
        /// <param name="tus">The translation units to be translated by Pangea</param>
        /// <param name="mask">A value provided by Trados Studio.  False means it is not to be translated.</param>
        /// <returns>A List of <c>SearchResults</c> objects to be sent up to Trados Studio after translating</returns>
        private List<SearchResults> BatchResults(TranslationUnit[] tus, bool[] mask)
        {
            List<SearchResults> results = new List<SearchResults>();
            List<string> toSend = new List<string>();
            
            for (int i = 0; i < tus.Length; i++)
            {
                //don't resend segments in the batch if they are already translated and user has not selected option to re-send
                bool dontsend = tus[i].ConfirmationLevel != ConfirmationLevel.Unspecified && !_options.resendDrafts;
                if (mask[i] && !dontsend)
                {
                    SearchResults singleresult = new SearchResults();
                    //We have to make the SourceSegment of the new SearchResults object into the SourceSegment of the current TU
                    //This fixes a problem that occurs in Studio 2011, but not Studio 2009
                    singleresult.SourceSegment = tus[i].SourceSegment;
                    results.Add(singleresult); //add new to hold place of those with mask = true
                    //if (_options.sendPlainTextOnly) //send plain text only if user selects
                    toSend.Add(tus[i].SourceSegment.ToPlain());
                    //else //send tag markup
                        //toSend += TagPlacer.PreparedSourceText(tus[i].SourceSegment) + " \r\n ";
                }
                else
                    results.Add(null);
            }

            //if (toSend.Trim().Equals("")) return results; //if our send string is empty then we have nothing to send....so just get out

            List<string> returnedStrings = PangeaConnecter.GetTranslation(toSend, false, _options);
            //returnedString = returnedString.Replace('\u00A0', '\n'); //sometimes pangea sends back both \r\n and sometimes just \n .. so we will deal with just \n
            //returnedString = returnedString.TrimEnd(); //sometimes pangea returns a line return at the end..and sometimes with spaces after..which we don't want
            
            //string[] returnedStrings = returnedString.Split(new char[] { '\n' });
            int returnedStringNumber = 0;


            for (int i = 0; i < results.Count; i++) //loop through results array
            {
                if (results[i] != null)
                {
                    //if (!_options.sendPlainTextOnly && tus[i].SourceSegment.HasTags)
                        //results[i].Add(CreateSearchResult(tus[i].SourceSegment, TagPlacer.GetTaggedTargetSegment(returnedStrings[returnedStringNumber], tus[i].SourceSegment)));
                    //else
                    //{
                        Segment targetSegment = new Segment();
                        targetSegment.Add(returnedStrings[returnedStringNumber].Trim());
                        results[i].Add(CreateSearchResult(tus[i].SourceSegment, targetSegment));
                    //}
                    returnedStringNumber++; //move to the next string in our list of returned strings
                }
            }

            return results;
        }
        
        
        /// <summary>
        /// This method is called by Trados Studio in the following general way:
        /// <para><b>Interactive Editor mode</b>: when the user is in the Editor, navigating from segment to segment, 
        /// this method is called with one <c>TranslationUnit</c> (TU) in the translationUnits array or two; when it is two, the
        /// first one is generally the previous segment and the second is the current segment...the mask array has two corresponding 
        /// boolean values, the first one false and the second one true...that allows doing the lookup for the current (second element in the array)
        /// only.</para>
        /// <para><b>Batch translate mode</b>: in a batch pre-translate, this method generally gets called with up to 10 or 11 TUs; 
        /// for a file with many TUs, Studio calls the method multiple times..generally the first time has 10 TUs, all with 
        /// corresponding mask of 'true'...then if there are more to be translated, it gets called again, this time
        /// with 11 TUs (one with a mask of 'false' representing the last TU from the previous call and the next 10 TUs with a mask of 'true')</para>
        /// </summary>
        /// <param name="settings">Passed by Studio as the set of search settings.</param>
        /// <param name="translationUnits">The set of translation units sent by Trados Studio.</param>
        /// <param name="mask">A set of boolean values passed by Trados Studio</param>
        /// <returns></returns>
        public SearchResults[] SearchTranslationUnitsMasked(SearchSettings settings, TranslationUnit[] translationUnits, bool[] mask)
        {
            List<SearchResults> results = new List<SearchResults>();
            //we want to do things differently if this is a batch translate...this will generally mean that this method
            //is being called with more than 2 or 3
            bool isBatch = translationUnits.Length > 3;
            if (isBatch)
            {                
                int BATCH_SIZE = 10;
                for (int bi = 0; bi < translationUnits.Length; bi += BATCH_SIZE)
                {
                    int batchEndIndex = Math.Min(BATCH_SIZE, translationUnits.Length - bi);
                    results.AddRange(BatchResults(new ArraySegment<TranslationUnit>(translationUnits, bi, batchEndIndex).ToArray(), 
                                                  new ArraySegment<bool>(mask, bi, batchEndIndex).ToArray()));

                }
                return results.ToArray();
            }
            

            int i = 0;
            foreach (var tu in translationUnits)
            {
                if (mask == null || mask[i])
                {
                    try
                    {
                        var result = SearchTranslationUnit(settings, tu);
                        results.Add(result);
                    }
                    catch
                    {
                        if (isBatch) results.Add(null); //if we are in a batch and this one won't work..then we don't want to kill the whole batch
                        else throw; //otherwise, if we are in editor mode, throw the error up to user
                    }
                }
                else
                {
                    results.Add(null);
                }
                i++;
            }

            return results.ToArray();
        }



        #region "NotForThisImplementation"
        /// <summary>
        /// Can be used to send updated translation units to a translation provider (i.e., containing both the source and the target entered/edited by the user. 
        /// It is a required interface method, but is not used in this implementation.
        /// </summary>
        /// <param name="translationUnits"></param>
        /// <param name="settings"></param>
        /// <param name="mask"></param>
        /// <returns></returns>
        public ImportResult[] AddTranslationUnitsMasked(TranslationUnit[] translationUnits, ImportSettings settings, bool[] mask)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Can be used to send updated translation units to a translation provider (i.e., containing both the source and the target entered/edited by the user. 
        /// It is a required interface method, but is not used in this implementation.
        /// </summary>
        /// <param name="translationUnit">Not required for this implementation.</param>
        /// <returns>Not required for this implementation.</returns>
        public ImportResult UpdateTranslationUnit(TranslationUnit translationUnit)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Can be used to send updated translation units to a translation provider (i.e., containing both the source and the target entered/edited by the user. 
        /// It is a required interface method, but is not used in this implementation.
        /// </summary>
        /// <param name="translationUnits">Not required for this implementation.</param>
        /// <returns>Not required for this implementation.</returns>
        public ImportResult[] UpdateTranslationUnits(TranslationUnit[] translationUnits)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Can be used to send updated translation units to a translation provider (i.e., containing both the source and the target entered/edited by the user. 
        /// It is a required interface method, but is not used in this implementation.
        /// </summary>
        /// <param name="translationUnits">Not required for this implementation.</param>
        /// <param name="previousTranslationHashes">Not required for this implementation.</param>
        /// <param name="settings">Not required for this implementation.</param>
        /// <param name="mask">Not required for this implementation.</param>
        /// <returns>Not required for this implementation.</returns>
        public ImportResult[] AddOrUpdateTranslationUnitsMasked(TranslationUnit[] translationUnits, int[] previousTranslationHashes, ImportSettings settings, bool[] mask)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Can be used to send updated translation units to a translation provider (i.e., containing both the source and the target entered/edited by the user. 
        /// It is a required interface method, but is not used in this implementation.
        /// </summary>
        /// <param name="translationUnit">Not required for this implementation.</param>
        /// <param name="settings">Not required for this implementation.</param>
        /// <returns>Not required for this implementation.</returns>
        public ImportResult AddTranslationUnit(TranslationUnit translationUnit, ImportSettings settings)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Can be used to send updated translation units to a translation provider (i.e., containing both the source and the target entered/edited by the user. 
        /// It is a required interface method, but is not used in this implementation.
        /// </summary>
        /// <param name="translationUnits">Not required for this implementation.</param>
        /// <param name="settings">Not required for this implementation.</param>
        /// <returns>Not required for this implementation.</returns>
        public ImportResult[] AddTranslationUnits(TranslationUnit[] translationUnits, ImportSettings settings)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Can be used to send updated translation units to a translation provider (i.e., containing both the source and the target entered/edited by the user. 
        /// It is a required interface method, but is not used in this implementation.
        /// </summary>
        /// <param name="translationUnits">Not required for this implementation.</param>
        /// <param name="previousTranslationHashes">Not required for this implementation.</param>
        /// <param name="settings">Not required for this implementation.</param>
        /// <returns>Not required for this implementation.</returns>
        public ImportResult[] AddOrUpdateTranslationUnits(TranslationUnit[] translationUnits, int[] previousTranslationHashes, ImportSettings settings)
        {
            throw new NotImplementedException();
        }
        #endregion

        #endregion
    }
}
