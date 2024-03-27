using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using Sdl.LanguagePlatform.Core;

namespace PangeaMtTranslationProvider
{
    /// <summary>
    /// Used as a wrapper for an SDL <c>Tag</c> object, so additional information can be associated with it.
    /// </summary>
    internal class MtTag  //for some reason extending the Tag class causes problems when casting back, so this class just wraps a Tag and adds properties
    {
        Tag tag;
        string padleft;
        string padright;

        /// <summary>
        /// Used as a wrapper for an SDL <c>Tag</c> object, so additional information can be associated with it.
        /// </summary>
        /// <param name="tag">The SDL <C>Tag</C> that this class will wrap.</param>
        internal MtTag(Tag tag)
        {
            this.tag = tag;
            padleft = "";
            padright = "";
        }

        /// <summary>
        /// Stores and returns a string representing the whitespace, tabs, and or carriage returns before a tag object in the source text
        /// </summary>
        internal string padLeft
        {
            get { return padleft; }
            set { padleft = value; }
        }

        /// <summary>
        /// Stores and returns a string representing the whitespace, tabs, and or carriage returns after a tag object in the source text
        /// </summary>
        internal string padRight
        {
            get { return padright; }
            set { padright = value; }
        }

        /// <summary>
        /// returns the SDL <c>Tag</c> object that the MtTag object wraps
        /// </summary>
        internal Tag SdlTag
        {
            get { return tag; }
        }
        
        /// <summary>
        /// Escape TextEquivalent attribute in the tag string representation
        /// </summary>
        public override string ToString()
        {
            Tag tag = new Tag(this.tag);
            tag.TextEquivalent = SecurityElement.Escape(tag.TextEquivalent);
            String key = tag.ToString();
            return key;
        }

    }
}
