/*
 *  Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license.
 *  See LICENSE in the source repository root for complete license information.
 */

using System;
using System.Windows;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Text;
using System.Linq;
using System.Xml.Linq;
using System.Globalization;
using System.Xml.Serialization;
using System.Xml;
using System.Reflection;
using System.Collections.Generic;
using System.Net;

using Windows.Storage.Streams;
using Windows.Data.Json;
using Windows.Web.Http;
using Windows.Web.Http.Filters;
using Windows.Web.Http.Headers;

using Office365Service.ViewModel;
using Office365Service.Model;

namespace Office365Service
{
    public class RESTService : WebService
    {
        protected Func<Task<string>> _getAccessTokenAsync;
        protected Func<Task<CookieContainer>> _getCookieContainerAsync;

        #region Constructor
        public RESTService() : base()
        {
        }

        public RESTService(Func<Task<string>> getAccessTokenAsync)
        {
            _getAccessTokenAsync = getAccessTokenAsync;
        }

        public RESTService(Func<Task<CookieContainer>> getCookieContainerAsync)
        {
            _getCookieContainerAsync = getCookieContainerAsync;
        }

        #endregion

        #region Properties
        // RequestViewModel
        private RequestViewModel requestVM = null;
        public RequestViewModel RequestViewModel
        {
            get
            {
                return requestVM;
            }
            set
            {
                requestVM = value;
            }
        }
        // ResponseViewModel
        private ResponseViewModel responseVM = null;
        public ResponseViewModel ResponseViewModel
        {
            get
            {
                return responseVM;
            }
            set
            {
                responseVM = value;
            }
        }
        #endregion

        #region Http Requests
        
        // Get
        public async Task<JsonObject> GetAsync(Uri requestUri, ObservableDictionary headers)
        {
            HttpClient httpClient = await this.GetWebRequestAsync(requestUri);
            AddHeaders(httpClient, headers);

            // Call the service and GET the response
            HttpResponseMessage httpResponse = await httpClient.GetAsync(requestUri);
            var responseText = await httpResponse.Content.ReadAsStringAsync();
            LogResponse(httpResponse, string.Empty, responseText);
            EnsureSuccessStatusCode(requestUri.LocalPath, httpResponse);

            // Read and parse the response
            return JsonObject.Parse(responseText);
        }
        
        // Post
        public async Task<JsonObject> PostAsync(Uri requestUri, ObservableDictionary headers, string body)
        {
            HttpClient httpClient = await this.GetWebRequestAsync(requestUri);
            AddHeaders(httpClient, headers);

            var method = new HttpMethod("POST");
            var request = new HttpRequestMessage(method, requestUri)
            {
                Content = new HttpStringContent(body)
            };
            request.Content.Headers["Content-Type"] = "application/json";

            // Call the service and GET the response
            HttpResponseMessage httpResponse = await httpClient.SendRequestAsync(request);
            var responseText = await httpResponse.Content.ReadAsStringAsync();
            LogResponse(httpResponse, body, responseText);
            EnsureSuccessStatusCode(requestUri.LocalPath, httpResponse);

            // Parse the response
            if ((responseText != null) && (responseText != string.Empty))
            {
                return JsonObject.Parse(responseText);
            }
            else
            {
                return new JsonObject();
            }
        }

        // Put
        public async Task<JsonObject> PutAsync(Uri requestUri, ObservableDictionary headers, IRandomAccessStreamWithContentType fileStream)
        {
            HttpClient httpClient = await this.GetWebRequestAsync(requestUri);
            AddHeaders(httpClient, headers);

            var method = new HttpMethod("PUT");
            fileStream.Seek(0);
            var request = new HttpRequestMessage(method, requestUri)
            {
                Content = new HttpStreamContent(fileStream)
            };
            request.Content.Headers["Content-Type"] = "application/json";

            // Call the service and GET the response
            HttpResponseMessage httpResponse = await httpClient.SendRequestAsync(request);
            var responseText = await httpResponse.Content.ReadAsStringAsync();
            LogResponse(httpResponse, "{file}", responseText);
            EnsureSuccessStatusCode(requestUri.LocalPath, httpResponse);

            // Parse the response
            if ((responseText != null) && (responseText != string.Empty))
            {
                return JsonObject.Parse(responseText);
            }
            else
            {
                return new JsonObject();
            }
        }

        // Patch
        public async Task<JsonObject> PatchAsync(Uri requestUri, ObservableDictionary headers, string body)
        {
            HttpClient httpClient = await this.GetWebRequestAsync(requestUri);
            AddHeaders(httpClient, headers);

            var method = new HttpMethod("PATCH");
            var request = new HttpRequestMessage(method, requestUri)
            {
                Content = new HttpStringContent(body)
            };
            request.Content.Headers["Content-Type"] = "application/json";

            // Call the service and GET the response
            HttpResponseMessage httpResponse = await httpClient.SendRequestAsync(request);
            var responseText = await httpResponse.Content.ReadAsStringAsync();
            LogResponse(httpResponse, body, responseText);
            EnsureSuccessStatusCode(requestUri.LocalPath, httpResponse);

            // Parse the response
            return JsonObject.Parse(responseText);
        }
        
        #endregion 

        #region Web Request
        protected override HttpClient GetWebRequest(Uri uri)
        {
            if (_getAccessTokenAsync != null)
            {
                // Add bearer token to the request
                var accessToken = _getAccessTokenAsync().Result;
                if (accessToken != null)
                {
                    var filter = new HttpBaseProtocolFilter();
                    filter.CacheControl.ReadBehavior = HttpCacheReadBehavior.MostRecent;
                        
                    var httpClient = new HttpClient(filter);
                    httpClient.DefaultRequestHeaders.Authorization = new HttpCredentialsHeaderValue("Bearer", accessToken);
                    return httpClient;
                }
                else
                {
                    throw new Exception("Could not get access token");
                }
            }
            else
            {
                // Add cookie container to the request
                CookieContainer cookieContainer = GetCookieContainer();

                HttpBaseProtocolFilter filter = new HttpBaseProtocolFilter();
                var cookieManager = filter.CookieManager;
                foreach (Cookie c in cookieContainer.GetCookies(uri))
                {
                    HttpCookie hc = new HttpCookie(c.Name, c.Domain, c.Path);
                    hc.Value = c.Value;
                    cookieManager.SetCookie(hc);
                }
                filter.CacheControl.ReadBehavior = HttpCacheReadBehavior.MostRecent;
                return new HttpClient(filter);
            }
        }

        private CookieContainer GetCookieContainer()
        {
            CookieContainer cookieContainer = null;
            Task<CookieContainer> task = _getCookieContainerAsync().ContinueWith(antecedent => cookieContainer = antecedent.Result);
            if (task != null)
                task.Wait();

            return cookieContainer;
        }

        protected async override Task<HttpClient> GetWebRequestAsync(Uri uri)
        {
            if (_getAccessTokenAsync != null)
            {
                // Add bearer token to the request
                var accessToken = await _getAccessTokenAsync();
                if (accessToken != null)
                {
                    var filter = new HttpBaseProtocolFilter();
                    filter.CacheControl.ReadBehavior = HttpCacheReadBehavior.MostRecent;

                    var httpClient = new HttpClient(filter);
                    httpClient.DefaultRequestHeaders.Authorization = new HttpCredentialsHeaderValue("Bearer", accessToken);
                    return httpClient;
                }
                else
                {
                    throw new Exception("Could not get access token");
                }
            }
            else
            {
                // Add cookie container to the request
                CookieContainer cookieContainer = await _getCookieContainerAsync();

                HttpBaseProtocolFilter filter = new HttpBaseProtocolFilter();
                var cookieManager = filter.CookieManager;
                foreach (Cookie c in cookieContainer.GetCookies(uri))
                {
                    //Debug.WriteLine("Cookie: Name=\{c.Name}, Domain=\{c.Domain}, Path=\{c.Path}");
                    HttpCookie hc = new HttpCookie(c.Name, c.Domain, c.Path);
                    hc.Value = c.Value;
                    cookieManager.SetCookie(hc);
                }
                filter.CacheControl.ReadBehavior = HttpCacheReadBehavior.MostRecent;
                return new HttpClient(filter);
            }
            
        }

        private void AddHeaders(HttpClient httpClient, ObservableDictionary headers)
        {
            foreach (var key in headers.Keys)
            {
                if (key != "Authorization")
                {
                    httpClient.DefaultRequestHeaders.Add(key, (string)headers[key]);
                }
            }
        }

        #endregion

        #region Exceptions
        private void EnsureSuccessStatusCode(string resourceLocation, HttpResponseMessage httpResponse)
        {
            if (!(httpResponse.IsSuccessStatusCode))
            {
                var ex = new Exception(httpResponse.ReasonPhrase);
                ex.Data["Method"] = httpResponse.RequestMessage.Method.ToString();
                ex.Data["ResourceLocation"] = resourceLocation;
                ex.Data["StatusCode"] = httpResponse.StatusCode;
                GrabResponseHeader("X-CorrelationId", httpResponse.Headers, ex);
                GrabResponseHeader("SPRequestID", httpResponse.Headers, ex);
                GrabResponseHeader("SPRequestGuid", httpResponse.Headers, ex);
                GrabResponseHeader("X-UserSessionId", httpResponse.Headers, ex);
                GrabResponseHeader("request-id", httpResponse.Headers, ex);
                GrabResponseHeader("XlsWfeCId", httpResponse.Headers, ex);
                GrabResponseHeader("XlsEcsCid", httpResponse.Headers, ex);
                GrabResponseHeader("X-OfficeCluster", httpResponse.Headers, ex);
                GrabResponseHeader("X-OfficeVersion", httpResponse.Headers, ex);
                GrabResponseHeader("X-XLSVersion", httpResponse.Headers, ex);
                throw ex;
            }
        }

        private void GrabResponseHeader(string key, HttpResponseHeaderCollection headers, Exception ex)
        {
            if (headers.ContainsKey(key))
            {
                ex.Data[key] = headers[key];
            }
        }
        #endregion

        #region Logging
        private void LogResponse(HttpResponseMessage response, string requestBodyText, string responseText)
        {
            // Log the reponse
            if (requestVM != null)
            {
                requestVM.Body = requestBodyText;

                var headers = new List<KeyValue>();
                foreach (var header in response.RequestMessage.Headers)
                {
                    headers.Add(new KeyValue() { Key = header.Key, Value = header.Value });
                }
                requestVM.Headers = headers;
            }
            if (responseVM != null)
            {
                responseVM.StatusCode = response.StatusCode.ToString();
                responseVM.ReasonPhrase = response.ReasonPhrase;
                responseVM.Body = responseText;

                var headers = new List<KeyValue>();
                foreach (var header in response.Headers)
                {
                    headers.Add(new KeyValue() { Key = header.Key, Value = header.Value });
                }
                responseVM.Headers = headers;
            }
        }


        #endregion
    }
}
