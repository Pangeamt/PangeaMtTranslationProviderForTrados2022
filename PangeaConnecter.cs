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
        internal static string GetTranslation(string sourcetext, bool hasTags, ProviderTranslationOptions options)
        {

            List<string> sourceTextList = new List<string>();
            sourceTextList.Add(sourcetext);
            return GetTranslation(sourceTextList, hasTags, options)[0];
        }

        /// <summary>
        /// This method can be accessed to send a text to Pangea.
        /// It performs encoding/decoding of text based on the options selected and various corresponding limitations of the engine
        /// </summary>
        /// <param name="sourcetext">Array of strings  to be translated</param>
        /// <param name="hasTags">Whether or not the segment has tag markup to be sent to Pangea</param>
        /// <param name="options">A <see cref="ProviderTranslationOptions"/> instance containing the set of options for translating</param>
        /// <returns></returns>
        internal static List<string> GetTranslation(List<string> textList, bool hasTags, ProviderTranslationOptions options)
        {
            string url = options.domain + @"/NexRelay/v1/translate";

            //options.engineID, options.sourceLang, options.targetLang
            var data = new Dictionary<string, object>
            {
                { "src", options.sourceLang },
                { "tgt", options.targetLang },
                { "mode", 2 },
                { "engine", options.engineID },
                { "text", textList.ToArray() }
            };

            try
            {
                var start_time = DateTime.Now;
                var jsonContent = jss.Serialize(data);

                //create request
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                //add credentials to header
                byte[] credentialsAuth = new UTF8Encoding().GetBytes(options.un + ":" + options.pwd);
                req.Headers["Authorization"] = "Basic " + Convert.ToBase64String(credentialsAuth);
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

                var result = new List<string>();

                using (WebResponse response = req.GetResponse())
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        var content =  new StreamReader(stream).ReadToEnd();
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
                throw new Exception($"Failed to translate: {string.Join(",", textList.ToArray())}", e);
            }
        }


        /// <summary>
        /// Retrieves the list of engines based on the given credentials and domain.
        /// </summary>
        /// <param name="username">The name of the user for which to retrive the available engines</param>
        /// <param name="password">The user's password</param>
        /// <param name="domain">The domain to connect to Pangea</param>
        /// <param name="timeout">This overloaded method allows specifying a timeout for the connection. 
        /// In this implementation, the timeout is useful when getting engines for the initial form load. 
        /// The reason is that if incorrect settings have been applied, it will take too long for the form to load while waiting for the timeout.</param>
        /// <returns>Returns a List of <see cref="PangeaEngine"/> objects for each available engine</returns>
        internal static List<PangeaEngine> GetEnginesList(string username, string password, string domain, int timeout)
        {
            List<PangeaEngine> enginesList = new List<PangeaEngine>();

            //get data
            string json = "";
            try
            {
                json = GetEnginesJson(username, password, domain, timeout);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            try
            {
                Dictionary<string, Dictionary<string, object>> sData = jss.Deserialize<Dictionary<string, Dictionary<string, object>>>(json);
                Object result = sData["response"]["result"];
                System.Collections.ArrayList resultArray = new System.Collections.ArrayList();
                if (result is System.Collections.ArrayList)
                {
                    resultArray = (System.Collections.ArrayList)result; // list of dictionaries
                } 
                else
                {
                    resultArray.Add(result); // single dictionary
                }

                foreach (Dictionary<string, object> engine in resultArray)
                    if (engine["@xsi.type"].Equals("translation-engine"))
                        enginesList.Add(new PangeaEngine(engine["@id"].ToString(), engine["@lang1"].ToString(), engine["@lang2"].ToString(), engine["@name"].ToString()));
            }
            catch (Exception ex)
            {
                throw ex;

            }

            return enginesList;

        }

        /// <summary>
        /// Retrieves the list of engines based on the given credentials and domain
        /// </summary>
        /// <param name="username">The name of the user for which to retrive the available engines</param>
        /// <param name="password">The user's password</param>
        /// <param name="domain">The domain to connect to Pangea</param>
        /// <returns>Returns a List of <see cref="PangeaEngine"/> objects for each available engine</returns>
        internal static List<PangeaEngine> GetEnginesList(string username, string password, string domain)
        {
            List<PangeaEngine> enginesList = new List<PangeaEngine>();

            //get data
            string json = "";
            try
            {
                json = GetEnginesJson(username, password, domain, -1);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            try
            {
                Dictionary<string, Dictionary<string, object>> sData = jss.Deserialize<Dictionary<string, Dictionary<string, object>>>(json);
                foreach (Dictionary<string, object> engine in (System.Collections.ArrayList)sData["response"]["result"])
                    if (engine["@xsi.type"].Equals("translation-engine"))
                        enginesList.Add(new PangeaEngine(engine["@id"].ToString(), engine["@lang1"].ToString(), engine["@lang2"].ToString(), engine["@name"].ToString()));
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
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="domain">The domain.</param>
        /// <param name="timeout">The timeout...if set to 0 it will be the default.</param>
        /// <returns></returns>
        private static string GetEnginesJson(string username, string password, string domain, int timeout)
        {
            string url = domain + @"/pangeamt/api/rest/translation-engines";

            
            //to deal with certificate problems 
            ServicePointManager.ServerCertificateValidationCallback = (sender1, certificate, chain, sslPolicyErrors) => true;

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            //set timeout if applicable
            if (timeout >= 0)
                req.Timeout = (timeout);
            req.PreAuthenticate = false;
            //add credentials to header
            byte[] credentialsAuth = new UTF8Encoding().GetBytes(username + ":" + password);
            req.Headers["Authorization"] = "Basic " + Convert.ToBase64String(credentialsAuth);


            //create response
            using (WebResponse response = req.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    return new StreamReader(stream).ReadToEnd();
                }
            }

        }

        /// <summary>
        /// Builds a multipart form data post for sending to Pangea to translate.
        /// </summary>
        /// <param name="sourcetext">The source text.</param>
        /// <param name="boundary">The boundary to use in the post.</param>
        /// <param name="options">The translation options for the different post parameters.</param>
        /// <returns>A byte array with the form data ready to be posted to Pangea.</returns>
        private static byte[] GetMultipartFormData(string sourcetext, string boundary, ProviderTranslationOptions options)
        {
            //build first part of post
            string contents = String.Format("{0}\r\nContent-Disposition: form-data; name=\"engine\"\r\n\r\n{1}"
                    + "\r\n{0}\r\nContent-Disposition: form-data; name=\"langFrom\"\r\n\r\n{2}"
                    + "\r\n{0}\r\nContent-Disposition: form-data; name=\"langTo\"\r\n\r\n{3}"
                    + "\r\n{0}\r\nContent-Disposition: form-data; name=\"text\"\r\n\r\n{4}\r\n", "--" + boundary, options.engineID, options.sourceLang, options.targetLang, sourcetext);
            //add glossary data if applicable
            if (options.useGlossary)
            {
                string filecontent = "";
                using (StreamReader reader = new StreamReader(options.glossaryFileName, System.Text.Encoding.UTF8))
                    filecontent = reader.ReadToEnd();
                contents += String.Format("{0}\r\nContent-Disposition: form-data; name=\"glossary\"; filename=\"{1}\"\r\nContent-Type: text/plain\r\n\r\n{2}\r\n\r\n\r\n", "--" + boundary, options.glossaryFileName, filecontent);
            }
            //add closing boundary footer to post
            contents += "--" + boundary + "--";

            //DEBUG: File.AppendAllText("C:\\Users\\Pangeanic\\Desktop\\" + "PangeaMT.log", "################" + Environment.NewLine + contents + Environment.NewLine);
            //return byte array from post string
            return Encoding.UTF8.GetBytes(contents);
        }

    }
}
