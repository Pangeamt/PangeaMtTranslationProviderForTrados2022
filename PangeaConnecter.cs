﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;
using System.Security.Policy;
using System.Web;
using System.CodeDom.Compiler;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms.VisualStyles;

namespace PangeaMtTranslationProvider
{
    /// <summary>
    /// This utility class contains functions for connecting with Pangea
    /// A utility class is used to avoid instantiating a new object, and thus an additional memory space, for each segment
    /// </summary>
    internal static class PangeaConnecter
    {
        private static JavaScriptSerializer jss = new JavaScriptSerializer();
        
        /// <summary>
        /// This method can be accessed to send a text to Pangea.
        /// It performs encoding/decoding of text based on the options selected and various corresponding limitations of the engine
        /// </summary>
        /// <param name="sourcetext">The text to be translated</param>
        /// <param name="hasTags">Whether or not the segment has tag markup to be sent to Pangea</param>
        /// <param name="options">A <see cref="ProviderTranslationOptions"/> instance containing the set of options for translating</param>
        /// <returns></returns>
        internal static string GetTranslation(string sourcetext, bool hasTags, string filepath, ProviderTranslationOptions options)
        {

            List<string> sourceTextList = new List<string>();
            sourceTextList.Add(sourcetext);
            return GetTranslation(sourceTextList, hasTags, filepath, options)[0];
        }

        /// <summary>
        /// This method can be accessed to send a text to Pangea.
        /// It performs encoding/decoding of text based on the options selected and various corresponding limitations of the engine
        /// </summary>
        /// <param name="textList">Array of strings  to be translated</param>
        /// <param name="hasTags">Whether or not the segment has tag markup to be sent to Pangea</param>
        /// <param name="options">A <see cref="ProviderTranslationOptions"/> instance containing the set of options for translating</param>
        /// <returns></returns>
        internal static List<string> GetTranslation(List<string> textList, bool hasTags, string filepath, ProviderTranslationOptions options)
        {
            var result = new List<string>();
            if (textList.Count == 0)
            {
                return result;
            }

            string url = options.domain + @"/NexRelay/v1/translate";

            string filename = Path.GetFileName(filepath); 
            //options.engineID, options.sourceLang, options.targetLang
            var data = new Dictionary<string, object>
            {
                { "src", options.sourceLang },
                { "tgt", options.targetLang },
                { "apikey",  options.pwd},
                { "engine", options.engineID },
                { "filename",  filename},
                { "text", textList.ToArray() }
            };

            // Add glossary entries matching given source texts
            if (options.useGlossary)
            {
                var entries = new List<Dictionary<string, object>>();
                foreach (string[] glossaryEntry in ParseFilterGlossary(options.glossaryContent, textList))
                {
                   entries.Add(new Dictionary<string, object> {
                        { "src", glossaryEntry[0] },
                        { "tgt", glossaryEntry[1] },
                        { "caseSensitive", false },
                    });
                }
                data["entries"] = entries;
            }

            try
            {
                var start_time = DateTime.Now;
                var jsonContent = jss.Serialize(data);

                //create request
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                //add credentials to header - DEPRECATED - using apikey in JSON
                //byte[] credentialsAuth = new UTF8Encoding().GetBytes(options.un + ":" + options.pwd);
                //req.Headers["Authorization"] = "Basic " + Convert.ToBase64String(credentialsAuth);
                //add others
                req.ContentType = "application/json";
                req.Method = "POST";

                //try setting a timeout in case of errors
                req.KeepAlive = false;
                req.Timeout = 50000;
                req.ServicePoint.ConnectionLeaseTimeout = 50000;
                req.ServicePoint.MaxIdleTime = 50000;


                using (var streamWriter = new StreamWriter(req.GetRequestStream()))
                {
                    streamWriter.Write(jsonContent);
                }


                using (HttpWebResponse response = (HttpWebResponse)req.GetResponse())
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        var content =  new StreamReader(stream).ReadToEnd();
                        if (response.StatusCode != HttpStatusCode.OK)
                        {
                            throw new Exception(response.StatusDescription + ": " + (string)content);
                        }

                        var translations =  jss.Deserialize<List<List<Dictionary<string, string>>>>(content);

                        foreach (var translation in translations)
                        {
                            if (translation.Count > 0 && translation[0].ContainsKey("tgt"))
                            {
                                result.Add(translation[0]["tgt"]);
                            } else
                            {
                                result.Add(null);
                            }
                        }

                    }
                }
 
                return result;
            }
            catch (Exception e)
            {
                throw new Exception($"Exception {e.Message} while translating {string.Join(",", textList.ToArray())}", e);
            }
        }


        /// <summary>
        /// Retrieves the list of engines based on the given credentials and domain.
        /// </summary>
        /// <param name="password">The user's API key</param>
        /// <param name="domain">The domain to connect to Pangea</param>
        /// <param name="timeout">This overloaded method allows specifying a timeout for the connection. 
        /// In this implementation, the timeout is useful when getting engines for the initial form load. 
        /// The reason is that if incorrect settings have been applied, it will take too long for the form to load while waiting for the timeout.</param>
        /// <returns>Returns a List of <see cref="PangeaEngine"/> objects for each available engine</returns>
        internal static List<PangeaEngine> GetEnginesList(string password, string domain, int timeout=-1)
        {
            List<PangeaEngine> enginesList = new List<PangeaEngine>();

            //get data
            string json = "";
            try
            {
                json = GetEnginesJson(password, domain, timeout);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            try
            {
                Dictionary<string, List<Dictionary<string, object>>> sData = jss.Deserialize<Dictionary<string, List<Dictionary<string, object>>>>(json);

                foreach (Dictionary<string, object> engine in sData["engines"])
                    enginesList.Add(new PangeaEngine(engine["id"].ToString(), engine["src"].ToString(), engine["tgt"].ToString(), engine["descr"].ToString()));
            }
            catch (Exception ex)
            {
                throw ex;

            }

            return enginesList;

        }

        
        /// <summary>
        /// Returns a json string from pangea with the list of available engines.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="domain">The domain.</param>
        /// <param name="timeout">The timeout...if set to 0 it will be the default.</param>
        /// <returns></returns>
        private static string GetEnginesJson(string password, string domain, int timeout)
        {
            string url = domain + @"/NexRelay/v1/corp/engines";

            
            //to deal with certificate problems 
            ServicePointManager.ServerCertificateValidationCallback = (sender1, certificate, chain, sslPolicyErrors) => true;

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            //set timeout if applicable
            if (timeout >= 0)
                req.Timeout = (timeout);
            req.PreAuthenticate = false;
            req.Method = "POST";


            using (var streamWriter = new StreamWriter(req.GetRequestStream()))
            {
                streamWriter.Write(jss.Serialize(new Dictionary<string, object>
                                    {
                                        { "apikey",  password},

                                    }));
            }
            //add credentials to header
            //byte[] credentialsAuth = new UTF8Encoding().GetBytes(username + ":" + password);
            //req.Headers["Authorization"] = "Basic " + Convert.ToBase64String(credentialsAuth);


            //create response
            using (WebResponse response = req.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    return new StreamReader(stream).ReadToEnd();
                }
            }

        }


        public static string LoadGlossary(string filename)
        {
            string filecontent = "";
            using (StreamReader reader = new StreamReader(filename, System.Text.Encoding.UTF8))
                filecontent = reader.ReadToEnd();
            return filecontent;
        }

        private  static IEnumerable<string[]> ParseGlossary(string input, char delimiter = '\t')
        {
            if (input == null)
            {
                yield break;
            }

            using (System.IO.StringReader reader = new System.IO.StringReader(input))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    yield return line.Split(delimiter);
                }
            }
        }

        private static List<string[]> ParseFilterGlossary(string glossaryContent, List<string> sourceTexts)
        {

            List<string[]> filteredGlossary = new List<string[]>();

            if (glossaryContent == null)
            {
                return filteredGlossary;
            }

            foreach (string[] glossaryEntry in ParseGlossary(glossaryContent))
            {
                foreach (string sourceText in sourceTexts)
                {
                    Match m = Regex.Match(sourceText, @"\b" + glossaryEntry[0] + @"\b", RegexOptions.IgnoreCase);
                    if (m.Success)
                        filteredGlossary.Add(glossaryEntry);
                }
            }
            return filteredGlossary;
        }

    }
}
