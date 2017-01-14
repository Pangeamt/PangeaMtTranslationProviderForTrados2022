using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;

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
            //a try block could be placed in this method, but for now the plugin will just throw any error up to studio
            
            //determine which API method to use
            string method = "translate";
            bool urlencode = false;
            if (hasTags && !options.sendPlainTextOnly && !options.useGlossary)
            {
                method = "translate-segment"; //we can use translate-segment if glossary is not used
                urlencode = true; //need to url encode if we are sending tags
            }
           if (urlencode)
                sourcetext = System.Web.HttpUtility.UrlEncode(sourcetext);
            
            //get results from Pangea
            string jsonResult = GetTranslationJson(sourcetext, method, options);

            //now need to deserialize
            string resultText = "";
            Dictionary<string, Dictionary<string, string>> sData = jss.Deserialize<Dictionary<string, Dictionary<string, string>>>(jsonResult);
            if (sData["response"]["@code"].Equals("OK"))
            {
                resultText = sData["response"]["@result"];
                //ureldecode if we have url encoded
                if (urlencode)
                {
                    //pangea seems to return spaces next to the plus signs sent in the URL encoded request...so we want to remove them
                    //this doesn't completely fix the problem in all cases however
                    resultText = resultText.Replace("+ ", "+");
                    resultText = System.Web.HttpUtility.UrlDecode(resultText);
                }
            }

            //now...we need to take out newline that Pangea inserts at the end..because Studio segments are generally not going to end in a newline
            if (resultText.EndsWith("\n"))
                resultText=resultText.Remove(resultText.Length-1, 1);
            
            return resultText;
        }

        /// <summary>
        /// Returns a json string from Pangea as a result of a translation.
        /// </summary>
        /// <param name="sourcetext">The source text.</param>
        /// <param name="method">The translation method to use on the server</param>
        /// <param name="options">The set of options to use for the translation</param>
        /// <returns>A joson string with the translation result from Pangea</returns>
        private static string GetTranslationJson(string sourcetext, string method, ProviderTranslationOptions options)
        {
            //build url
            string url = options.domain + @"/pangeamt/api/rest/" + method;

            //to deal with certificate problems 
            ServicePointManager.ServerCertificateValidationCallback = (sender1, certificate, chain, sslPolicyErrors) => true;
            
            //create request
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            //add credentials to header
            byte[] credentialsAuth = new UTF8Encoding().GetBytes(options.un + ":" + options.pwd);
            req.Headers["Authorization"] = "Basic " + Convert.ToBase64String(credentialsAuth);
            //add others
            string boundary = Guid.NewGuid().ToString(); //we'll use this again later
            req.ContentType = "multipart/form-data; boundary=" + boundary;
            req.Method = "POST";

            //try setting a timeout in case of errors
            //req.Timeout = 10000;

            //form into valid multipart form data
            byte[] bytedata = GetMultipartFormData(sourcetext, boundary, options);
            req.ContentLength = bytedata.Length;

            Stream requestStream = req.GetRequestStream();
            requestStream.Write(bytedata, 0, bytedata.Length);
            requestStream.Close();


            using (WebResponse response = req.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    return new StreamReader(stream).ReadToEnd();
                }
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
            //return byte array from post string
            return Encoding.UTF8.GetBytes(contents);
        }

    }
}
