using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SahadevUtilities.WebAPI
{
    public class HttpClientRestAPI
    {
        #region Variables
        static string _className = "SahadevUtilities.WebAPI.HttpClientRestAPI";
        private string baseUrl = string.Empty;
        #endregion

        #region Properties
        #endregion

        #region Constructor
        public HttpClientRestAPI() { }

        public HttpClientRestAPI(string baseurl)
        {
            baseUrl = baseurl;
        }
        #endregion

        #region Methods
        #region Common Method
        public static List<KeyValuePair<string, string>> GetAPIParameterList(Dictionary<string, string> dicParamList)
        {
            List<KeyValuePair<string, string>> lstAPIParam = new List<KeyValuePair<string, string>>();
            try
            {
                foreach (KeyValuePair<string, string> pair in dicParamList)
                {
                    lstAPIParam.Add(new KeyValuePair<string, string>(pair.Key, string.IsNullOrEmpty(pair.Value) ? DBNull.Value.ToString() : pair.Value));
                }
            }
            catch (Exception)
            {

            }
            return lstAPIParam;
        }
        #endregion Common Method

        #region HttpClientGetMethod

        /// <summary>
        /// This methos is used to call get method of Http Client
        /// </summary>
        /// <param name="url">api name with query string parameter</param>
        /// <returns></returns>
        public HttpResponseMessage HttpClientGetMethod(string url)
        {
            HttpResponseMessage response = null;
            try
            {
                response = HttpClientGetMethod(url, null);
            }
            catch (Exception ex)
            {
                Log.Error(ex, _className, "HttpClientGetMethod(string url)");
            }
            return response;
        }

        /// <summary>
        /// This methos is used to call get method of Http Client
        /// </summary>
        /// <param name="url">api name with query string parameter</param>
        /// <param name="headerName">header name</param>
        /// <param name="headerValue">header value</param>
        /// <returns></returns>
        public HttpResponseMessage HttpClientGetMethod(string url, string headerName, string headerValue)
        {
            HttpResponseMessage response = null;
            try
            {
                Dictionary<string, string> dictHeaders = new Dictionary<string, string>();
                dictHeaders.Add(headerName, headerValue);
                response = HttpClientGetMethod(url, dictHeaders);
            }
            catch (Exception ex)
            {
                Log.Error(ex, _className, "HttpClientGetMethod(string url, string headerName, string headerValue)");
            }
            return response;
        }

        /// <summary>
        /// This methos is used to call get method of Http Client
        /// </summary>
        /// <param name="url">api name with query string parameter</param>
        /// <param name="firstHeaderName">first header name</param>
        /// <param name="firstHeaderValue">first header value</param>
        /// <param name="secondHeaderName">second header name</param>
        /// <param name="secondHeaderValue">second header value</param>
        /// <returns></returns>
        public HttpResponseMessage HttpClientGetMethod(string url, string firstHeaderName, string firstHeaderValue, string secondHeaderName, string secondHeaderValue)
        {
            HttpResponseMessage response = null;
            try
            {
                Dictionary<string, string> dictHeaders = new Dictionary<string, string>();
                dictHeaders.Add(firstHeaderName, firstHeaderValue);
                dictHeaders.Add(secondHeaderName, secondHeaderValue);
                response = HttpClientGetMethod(url, dictHeaders);
            }
            catch (Exception ex)
            {
                Log.Error(ex, _className, "HttpClientGetMethod(string url, string firstHeaderName, string firstHeaderValue, string secondHeaderName, string secondHeaderValue)");
            }
            return response;
        }

        /// <summary>
        /// This methos is used to call get method of Http Client
        /// </summary>
        /// <param name="url">api name with query string parameter</param>
        /// <param name="headers">Dictionary of type string containing header name and value as a keypair</param>
        /// <returns></returns>
        public HttpResponseMessage HttpClientGetMethod(string url, Dictionary<string, string> headers)
        {
            HttpResponseMessage response = null;
            try
            {
                using (var client = new HttpClient())
                {
                    string uriString = baseUrl;
                    if (!string.IsNullOrEmpty(uriString))
                    {
                        client.BaseAddress = new Uri(uriString);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        if (headers != null)
                        {
                            foreach (KeyValuePair<string, string> parameter in headers)
                            {
                                client.DefaultRequestHeaders.Add(parameter.Key, parameter.Value);
                            }
                        }
                        response = client.GetAsync(url).Result;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, _className, "HttpClientGetMethod(string url, Dictionary<string, string> headers)");
            }
            return response;
        }
        #endregion

        #region HttpClientPostMethod

        /// <summary>
        /// This methos is used to call post method of Http Client
        /// </summary>
        /// <param name="url">api name with query string parameter</param>
        /// <param name="obj">post object</param>
        /// <returns></returns>
        public HttpResponseMessage HttpClientPostMethod(string url, List<KeyValuePair<string, string>> values)
        {
            HttpResponseMessage response = null;
            try
            {
                response = HttpClientPostMethod(url, values, null);
            }
            catch (Exception ex)
            {
                Log.Error(ex, _className, "HttpClientPostMethod(string url, List<KeyValuePair<string, string>> values)");
            }
            return response;
        }

        /// <summary>
        /// This methos is used to call post method of Http Client
        /// </summary>
        /// <param name="url">api name with query string parameter</param>
        /// <param name="obj">post object</param>
        /// <param name="headerName">header name</param>
        /// <param name="headerValue">header value</param>
        /// <returns></returns>
        public HttpResponseMessage HttpClientPostMethod(string url, List<KeyValuePair<string, string>> values, string headerName, string headerValue)
        {
            HttpResponseMessage response = null;
            try
            {
                Dictionary<string, string> dictHeaders = new Dictionary<string, string>();
                dictHeaders.Add(headerName, headerValue);
                response = HttpClientPostMethod(url, values, dictHeaders);
            }
            catch (Exception ex)
            {
                Log.Error(ex, _className, "HttpClientPostMethod(string url, object obj, string headerName, string headerValue)");
            }
            return response;
        }

        /// <summary>
        /// This methos is used to call post method of Http Client
        /// </summary>
        /// <param name="url">api name with query string parameter</param>
        /// <param name="obj">post object</param>
        /// <param name="firstHeaderName">first header name</param>
        /// <param name="firstHeaderValue">first header value</param>
        /// <param name="secondHeaderName">second header name</param>
        /// <param name="secondHeaderValue">second header value</param>
        /// <returns></returns>
        public HttpResponseMessage HttpClientPostMethod(string url, List<KeyValuePair<string, string>> values, string firstHeaderName, string firstHeaderValue, string secondHeaderName, string secondHeaderValue)
        {
            HttpResponseMessage response = null;
            try
            {
                Dictionary<string, string> dictHeaders = new Dictionary<string, string>();
                dictHeaders.Add(firstHeaderName, firstHeaderValue);
                dictHeaders.Add(secondHeaderName, secondHeaderValue);
                response = HttpClientPostMethod(url, values, dictHeaders);
            }
            catch (Exception ex)
            {
                Log.Error(ex, _className, "HttpClientPostMethod(string url, List<KeyValuePair<string, string>> values, string firstHeaderName, string firstHeaderValue, string secondHeaderName, string secondHeaderValue)");
            }
            return response;
        }

        /// <summary>
        /// This methos is used to call post method of Http Client
        /// </summary>
        /// <param name="url">api name with query string parameter</param>
        /// <param name="obj">post object</param>
        /// <param name="headers">Dictionary of type string containing header name and value as a keypair</param>
        /// <returns></returns>
        public HttpResponseMessage HttpClientPostMethod(string url, List<KeyValuePair<string, string>> values, Dictionary<string, string> headers)
        {
            HttpResponseMessage response = null;
            try
            {
                using (var client = new HttpClient())
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                    string uriString = baseUrl;
                    if (!string.IsNullOrEmpty(uriString))
                    {
                        client.BaseAddress = new Uri(uriString);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        if (headers != null)
                        {
                            foreach (KeyValuePair<string, string> parameter in headers)
                            {
                                client.DefaultRequestHeaders.Add(parameter.Key, parameter.Value);
                            }
                        }

                        var content = new FormUrlEncodedContent(values);
                        response = client.PostAsync(url, content).Result;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, _className, "HttpClientPostMethod(string url, object obj, Dictionary<string, string> headers)");
            }
            return response;
        }

        public HttpResponseMessage HttpClientPostMethod(string url, object values)
        {
            HttpResponseMessage response = null;
            try
            {
                using (var client = new HttpClient())
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                    string uriString = baseUrl;
                    if (!string.IsNullOrEmpty(uriString))
                    {
                        client.BaseAddress = new Uri(uriString);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                       
                        
                        response = client.PostAsJsonAsync(url, values).Result;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, _className, "HttpClientPostMethod(string url, object values)");
            }
            return response;
        }

        /// <summary>
        /// This methos is used to call post method of Http Client
        /// </summary>
        /// <param name="url">api name with query string parameter</param>
        /// <param name="obj">post object</param>
        /// <param name="headers">Dictionary of type string containing header name and value as a keypair</param>
        /// <param name="file">file to be uploaded</param>
        /// <returns></returns>
        public HttpResponseMessage HttpClientPostMethod(string url, List<KeyValuePair<string, string>> values, Dictionary<string, string> headers, IFormFile file)
        {
            HttpResponseMessage response = null;
            try
            {
                using (var client = new HttpClient())
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                    string uriString = baseUrl;
                    if (!string.IsNullOrEmpty(uriString))
                    {
                        using (var content = new MultipartFormDataContent())
                        {
                            client.BaseAddress = new Uri(uriString);
                            client.DefaultRequestHeaders.Accept.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                            if (headers != null)
                            {
                                foreach (KeyValuePair<string, string> parameter in headers)
                                {
                                    client.DefaultRequestHeaders.Add(parameter.Key, parameter.Value);
                                }
                            }

                            foreach (var keyValuePair in values)
                            {
                                content.Add(new StringContent(keyValuePair.Value), keyValuePair.Key);
                            }
                            using (var ms = new MemoryStream())
                            {
                                file.CopyTo(ms);
                                var fileBytes = ms.ToArray();
                                string s = Convert.ToBase64String(fileBytes);
                                var fileContent = new ByteArrayContent(fileBytes);
                                fileContent.Headers.Add("Content-Type", "application/octet-stream");
                                fileContent.Headers.Add("Content-Disposition", "form-data; name=\"photo\"; filename=\"" + Path.GetFileName(file.FileName) + "\"");
                                content.Add(fileContent, "photo");

                                response = client.PostAsync(url, content).Result;
                            }
                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, _className, "HttpClientPostMethod(string url, List<KeyValuePair<string, string>> values, Dictionary<string, string> headers, IFormFile file)");
            }
            return response;
        }
        #endregion
        #endregion
    }
}
