using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PangeaMtTranslationProvider
{
    /// <summary>
    /// This class is used to hold information about available MT engines.
    /// </summary>
    internal class PangeaEngine
    {
        /// <summary>
        /// Holds information about available MT engines.
        /// </summary>
        /// <param name="id">The engine ID.</param>
        /// <param name="lang1">Language 1.</param>
        /// <param name="lang2">Language 2.</param>
        /// <param name="name">The engine name.</param>
        internal PangeaEngine(string id, string lang1, string lang2, string name)
        {
            this.id = id;
            this.lang1 = lang1;
            this.lang2 = lang2;
            this.name = name;

        }
        /// <summary>
        /// Sets and returns the id.
        /// </summary>
        internal string id { get; set; }
        /// <summary>
        /// Sets and returns language 1.
        /// </summary>
        internal string lang1 { get; set; }
        /// <summary>
        /// Sets and returns language 2.
        /// </summary>
        internal string lang2 { get; set; }
        /// <summary>
        /// Sets and returns the name.
        /// </summary>
        internal string name { get; set; }
    }
}
