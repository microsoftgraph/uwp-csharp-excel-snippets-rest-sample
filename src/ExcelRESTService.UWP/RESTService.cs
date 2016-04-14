/*
 *  Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license.
 *  See LICENSE in the source repository root for complete license information.
 */

using System;
using System.Threading.Tasks;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;

using Newtonsoft.Json.Linq;

using Office365Service.ViewModel;
using Office365Service.Model;

namespace Office365Service
{
    public class RESTService : WebService
    {
        protected Func<Task<string>> _getAccessTokenAsync;

        #region Constructor
        public RESTService() : base()
        {
        }

        public RESTService(Func<Task<string>> getAccessTokenAsync)
        {
            _getAccessTokenAsync = getAccessTokenAsync;
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
        
        // GET
        public async Task<JObject> GetAsync(Uri requestUri, ObservableDictionary<string, string> headers)
        {
            HttpClient httpClient = await this.GetWebRequestAsync(requestUri);
            AddHeaders(httpClient, headers);

            // Ensure that we don't get a cached response
            httpClient.DefaultRequestHeaders.IfModifiedSince = DateTime.UtcNow;

            // Call the service and GET the response
            HttpResponseMessage httpResponse = await httpClient.GetAsync(requestUri);
            var responseText = await httpResponse.Content.ReadAsStringAsync();
            LogResponse(httpResponse, string.Empty, responseText);
            EnsureSuccessStatusCode(requestUri.LocalPath, httpResponse);

            // Read and parse the response
            return JObject.Parse(responseText);
        }
        
        // POST
        public async Task<JObject> PostAsync(Uri requestUri, ObservableDictionary<string, string> headers, string body)
        {
            HttpClient httpClient = await this.GetWebRequestAsync(requestUri);
            AddHeaders(httpClient, headers);

            var request = new HttpRequestMessage(new HttpMethod("POST"), requestUri)
            {
                Content = new StringContent(body, Encoding.UTF8, "application/json"),
            };
            
            // Call the service and GET the response
            HttpResponseMessage httpResponse = await httpClient.SendAsync(request);
            var responseText = await httpResponse.Content.ReadAsStringAsync();
            LogResponse(httpResponse, body, responseText);
            EnsureSuccessStatusCode(requestUri.LocalPath, httpResponse);

            // Parse the response
            if ((responseText != null) && (responseText != string.Empty))
            {
                return JObject.Parse(responseText);
            }
            else
            {
                return new JObject();
            }
        }

        // PUT
        public async Task<JObject> PutAsync(Uri requestUri, ObservableDictionary<string, string> headers, Stream stream)
        {
            HttpClient httpClient = await this.GetWebRequestAsync(requestUri);
            AddHeaders(httpClient, headers);

            stream.Seek(0, SeekOrigin.Begin);
            var request = new HttpRequestMessage(new HttpMethod("PUT"), requestUri)
            {
                Content = new StreamContent(stream)
            };

            // Call the service and GET the response
            HttpResponseMessage httpResponse = await httpClient.SendAsync(request);
            var responseText = await httpResponse.Content.ReadAsStringAsync();
            LogResponse(httpResponse, "{file}", responseText);
            EnsureSuccessStatusCode(requestUri.LocalPath, httpResponse);

            // Parse the response
            if ((responseText != null) && (responseText != string.Empty))
            {
                return JObject.Parse(responseText);
            }
            else
            {
                return new JObject();
            }
        }

        // Patch
        public async Task<JObject> PatchAsync(Uri requestUri, ObservableDictionary<string, string> headers, string body)
        {
            HttpClient httpClient = await this.GetWebRequestAsync(requestUri);
            AddHeaders(httpClient, headers);

            var request = new HttpRequestMessage(new HttpMethod("PATCH"), requestUri)
            {
                Content = new StringContent(body, Encoding.UTF8, "application/json")
            };
            
            // Call the service and GET the response
            HttpResponseMessage httpResponse = await httpClient.SendAsync(request);
            var responseText = await httpResponse.Content.ReadAsStringAsync();
            LogResponse(httpResponse, body, responseText);
            EnsureSuccessStatusCode(requestUri.LocalPath, httpResponse);

            // Parse the response
            return JObject.Parse(responseText);
        }
        
        #endregion

        #region Web Request
        protected async override Task<HttpClient> GetWebRequestAsync(Uri uri)
        {
            // Add bearer token to the request
            var accessToken = await _getAccessTokenAsync();
            if (accessToken != null)
            {
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
                return httpClient;
            }
            else
            {
                throw new Exception("Could not get access token");
            }
        }

        private void AddHeaders(HttpClient httpClient, ObservableDictionary<string, string> headers)
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

        private void GrabResponseHeader(string key, HttpResponseHeaders headers, Exception ex)
        {
            if (headers.Contains(key))
            {
                ex.Data[key] = headers.GetValues(key).First();
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
                    headers.Add(new KeyValue() { Key = header.Key, Value = header.Value.First() });
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
                    headers.Add(new KeyValue() { Key = header.Key, Value = header.Value.First() });
                }
                responseVM.Headers = headers;
            }
        }
        #endregion
    }
}
