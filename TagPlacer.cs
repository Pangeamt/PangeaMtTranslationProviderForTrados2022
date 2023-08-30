using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sdl.LanguagePlatform.TranslationMemory;
using Sdl.LanguagePlatform.Core;
using System.Text.RegularExpressions;

namespace PangeaMtTranslationProvider
{
    
    /// <summary>
    /// This utility class has functions for dealing with tags.
    /// A utility class is used to avoid instantiating a new object, and thus an additional memory space, for each segment.
    /// </summary>
    internal static class TagPlacer
    {
        
        /// <summary>
        /// PANGEA-SPECIFIC: since Pangea only seems to translate text when there are spaces between the text and tag markup,
        /// this method can return a source string with tag markup and text with spaces between them.
        /// </summary>
        /// <param name="segment">The source <c>Segment</c> to be processed.</param>
        internal static string PreparedSourceText(Segment segment)
        {
            string result = "";
            foreach (var element in segment.Elements)
            {
                System.Type elType = element.GetType();
                if (elType.ToString().Equals("Sdl.LanguagePlatform.Core.Tag")) //if a tag 
                    //add a leading and trailing space or otherwise pangea seems not to handle it
                    result += " " + element.ToString() + " ";
                    //result += element.ToString();
                else
                    result += element.ToString(); //just add if anything besides tag
            }
            return result;
        }


        
        /// <summary>
        /// Returns a <c>Segment</c> object from a target text containing tag markup, replacing that markup with the corresponding <c>Tag</c> object.
        /// In this implementation, it Trims out the extra spaces added to the string <c>PreparedSourceText</c> for Pangea to handle it better.
        /// </summary>
        /// <param name="targetText">The target text to convert into a tagged <c>Segment</c> object.</param>
        /// <param name="sourceSegment">The source Segment to be used to copy tag objects to target segment.</param>
        /// <returns><c>Segment</c> object containing tags based on raw target text input.</returns>
        internal static Segment GetTaggedTargetSegment(string targetText, Segment sourceSegment)
        {
            
            //populate all segment tags into a dictionary of tag markup and and corresponding tag object for replacement in target
            Dictionary<string, MtTag> dict = new Dictionary<string, MtTag>();
            
            for (int i=0; i<sourceSegment.Elements.Count; i++)
            {
                System.Type elType = sourceSegment.Elements[i].GetType();
                if (elType.ToString().Equals("Sdl.LanguagePlatform.Core.Tag"))
                {
                    
                     MtTag theTag = new MtTag((Tag)sourceSegment.Elements[i].Duplicate());
                    //now we have to jump through a bunch of hoops to figure out whether this tag is preceded and/or followed by whitespace
                    //this is partly related to our need to pad with whitespace earlier so Pangea can handle it
                     if (i > 0 && !sourceSegment.Elements[i - 1].GetType().ToString().Equals("Sdl.LanguagePlatform.Core.Tag"))
                    {
                        //if element before is not a tag 
                        //elType = sourceSegment.Elements[i - 1].GetType();
                        string prevText = sourceSegment.Elements[i - 1].ToString();
                        if (!prevText.Trim().Equals(""))//and not just whitespace
                        {
                            //get number of trailing spaces for that segment
                            int whitespace = prevText.Length - prevText.TrimEnd().Length;
                            //add that trailing space to our tag as leading space
                            theTag.padLeft = prevText.Substring(prevText.Length - whitespace);
                        }
                    }
                     if (i < sourceSegment.Elements.Count - 1 && !sourceSegment.Elements[i + 1].GetType().ToString().Equals("Sdl.LanguagePlatform.Core.Tag"))
                    {
                        //now we are checking if element after is not a tag
                        //elType = sourceSegment.Elements[i + 1].GetType();
                        //here we don't care whether it is only whitespace
                        //get number of leading spaces for that segment
                        string nextText = sourceSegment.Elements[i + 1].ToString(); 
                        int whitespace = nextText.Length - nextText.TrimStart().Length;
                        //add that trailing space to our tag as leading space
                        theTag.padRight = nextText.Substring(0, whitespace);
                    }
                    dict.Add(theTag.SdlTag.ToString().ToLower(), theTag);
                }
            }

            
            Segment targetOut = new Segment();
            //now for each tag in the source, find the corresponding markup in the target and convert it to a tag
            string[] targetElements = GetSegmentElements(targetText);//get our array of elements..it will be array of tagtexts and text in the order they appear in target text
            //build our segment looping through elements
            for (int i = 0; i < targetElements.Length; i++)
            {
                //if the matching markup exists in source text, add its corresponding tag from the dictionary
                if (dict.ContainsKey(targetElements[i].ToLower())) //ToLower in case MT engine changes case, which sometimes happens
                {
                    string padleft = dict[targetElements[i].ToLower()].padLeft;
                    string padright = dict[targetElements[i].ToLower()].padRight;
                    if (padleft.Length > 0) targetOut.Add(padleft); //add leading space if applicable in the source text
                    targetOut.Add(dict[targetElements[i].ToLower()].SdlTag); //add the actual tag element after casting it back to a Tag
                    if (padright.Length > 0) targetOut.Add(padright); //add trailing space if applicable in the source text
                }
                else //if anything other than one of our tags
                {
                    string toAdd = targetElements[i];
                    if (toAdd.Trim().Length > 0) //if the element is something other than whitespace, i.e. some text in addition
                    {
                        toAdd = toAdd.Trim(); //trim out extra spaces, since they are dealt with by associating them with the tags
                        targetOut.Add(toAdd); //add to the segment
                    }
                }
            }
            return targetOut;

        }

        /// <summary>
        /// Uses regular expression functions to split the raw target text into text elements and tag elements.
        /// </summary>
        /// <param name="inString">The raw target text to split into elements.</param>
        /// <returns>An array of strings representing the target elements.</returns>
        private static string[] GetSegmentElements(string inString)
        {
            //puts our returned string into an array of elements

            //first create a regex to put our array separators around the tags

            string str = inString;
            string pattern = @"\<.*?\>"; //finds anything between <>
            Regex rgx = new Regex(pattern);
            MatchCollection matches = rgx.Matches(inString);

            foreach (Match myMatch in matches)
            {
                str = str.Replace(myMatch.Value, "***" + myMatch.Value + "***"); //puts our separator around tagtexts
            }


            string[] stringSeparators = new string[] { "***" }; //split at our inserted marker
            string[] strAr = str.Split(stringSeparators, StringSplitOptions.None);
            return strAr;

        }

    }
}
