/*
 *  Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license.
 *  See LICENSE in the source repository root for complete license information.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Newtonsoft.Json.Linq;

using Office365Service;
using Office365Service.ViewModel;

using Microsoft.Edm;

namespace Office365Service
{
    class RestApi : IRestApi
    {
        #region Constructor
        public RestApi(RESTService service, string title, string description, string method, string resourceFormat, Type resultType)
        {
            Service = service;
            Title = title;
            Description = description;
            Method = method;
            ResourceFormat = resourceFormat;
            QueryParameters = string.Empty;
            ResultType = resultType;

            Headers = new ObservableDictionary<string, string>();
            Headers["Accept"] = "application/json";

            BodyProperties = new ObservableDictionary<string, object>();
        }
        #endregion

        #region Properties
        // Service
        public RESTService Service { get; set; }
        // Title
        public string Title { get; set; }
        // Description
        public string Description { get; set; }
        // Method (GET, POST, PATCH, ...)
        public string Method { get; set; }
        // ResourceFormat
        public string ResourceFormat { get; set; }
        // Resource
        public virtual string Resource
        {
            get
            {
                return string.Empty;
            }
        }
        // QueryParameters
        public string QueryParameters { get; set; }
        // Headers
        public ObservableDictionary<string, string> Headers { get; set; }
        // BodyProperties
        public ObservableDictionary<string, object> BodyProperties { get; set; }
        // BodyAsJson
        public JObject BodyAsJson
        {
            get
            {
                var bodyJson = new JObject();
                foreach (var key in BodyProperties.Keys)
                {
                    var value = BodyProperties[key];
                    if (value != null)
                    {
                        switch (value.GetType().Name)
                        {
                            case "String":
                                //bodyJson.Add(key, JsonValue.CreateStringValue((string)value));
                                bodyJson.Add(key, (string)value);
                                break;
                            case "Boolean":
                                //bodyJson.Add(key, JsonValue.CreateBooleanValue((bool)value));
                                bodyJson.Add(key, (bool)value);
                                break;
                            case "Int32":
                                //bodyJson.Add(key, JsonValue.CreateNumberValue((int)value));
                                bodyJson.Add(key, (int)value);
                                break;
                            case "Object[]":
                                bodyJson.Add(key, MapValuesToJson((object[])value));
                                break;
                            default:
                                throw new ArgumentOutOfRangeException($"'{value.GetType().Name}' type not recognized.");
                        }
                    }
                }
                return bodyJson;
            }
        }
        // BodyAsText
        public string BodyAsText
        {
            get
            {
                if ((Method == "POST") || (Method == "PATCH"))
                {
                    return BodyAsJson.ToString();
                }
                else if (Method == "PUT")
                {
                    return "{file}";
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        // Filestream
        public Stream FileStream { get; set; }
        // ResultType
        public Type ResultType { get; set; }
        // RequestUri (https://graph.microsoft.com/...)
        public Uri RequestUri
        {
            get
            {
                var queryParams = string.Empty;
                if (QueryParameters != string.Empty)
                {
                    queryParams = $"?{QueryParameters}";
                }

                return new Uri(Uri.EscapeUriString($"{Service.Url}{Resource}{queryParams}"));
            }
            set { }
        }
        #endregion

        #region Methods
        public async Task<object> InvokeAsync()
        {
            switch (Method)
            {
                case "GET":
                    return MapResult(await Service.GetAsync(RequestUri, Headers));
                case "POST":
                    return MapResult(await Service.PostAsync(RequestUri, Headers, BodyAsText));
                case "PATCH":
                    return MapResult(await Service.PatchAsync(RequestUri, Headers, BodyAsText));
                case "PUT":
                    return MapResult(await Service.PutAsync(RequestUri, Headers, FileStream));
                default:
                    throw new ArgumentException($"'{Method}' is not a valid method");
            }
        }
        #endregion

        #region Mapping
        protected virtual object MapResult(JObject jsonResult)
        {
            if (ResultType != null)
            {
                object result;
                switch (ResultType.Name)
                {
                    case "EdmString":
                        result = EdmString.MapFromJson(jsonResult);
                        LogResult(result);
                        return result;
                    default:
                        throw new ArgumentOutOfRangeException($"'{ResultType.Name}' cannot be mapped");
                }
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region Map values to/from Json
        public static string MapStringFromJson(JObject json, string name)
        {
            JToken token = null;
            if (json.TryGetValue(name, out token))
            {
                return token.Value<string>();
            }
            else
            {
                return null;
            }
        }

        public static double? MapNumberFromJson(JObject json, string name)
        {
            JToken token = null;
            if (json.TryGetValue(name, out token))
            {
                return token.Value<double>();
            }
            else
            {
                return null;
            }
        }

        public static bool? MapBooleanFromJson(JObject json, string name)
        {
            JToken token = null;
            if (json.TryGetValue(name, out token))
            {
                return token.Value<bool>();
            }
            else
            {
                return null;
            }
        }

        public static object[][] MapValuesFromJson(JObject json, string name)
        {
            JToken token = null;

            if (json.TryGetValue(name, out token))
            {
                var rows = new List<object[]>();
                foreach (var jsonRow in token)
                {
                    var row = new List<object>();
                    foreach (var value in jsonRow)
                    {
                        switch (value.Type)
                        {
                            case JTokenType.String:
                                row.Add((string)(value));
                                break;
                            case JTokenType.Float:
                                row.Add((double)(value));
                                break;
                            case JTokenType.Integer:
                                row.Add((int)(value));
                                break;
                            case JTokenType.Boolean:
                                row.Add((bool)(value));
                                break;
                            default:
                                throw new ArgumentException("Unknown value type");
                        }
                    }
                    rows.Add(row.ToArray());
                }
                return rows.ToArray();
            }
            else
            {
                return null;
            }
        }

        private static JArray MapValuesToJson(object[] values)
        {
            var jsonValues = new JArray();
            if (values != null)
            {
                foreach (object[] row in values)
                {
                    var jsonRowValues = new JArray();
                    foreach (var value in row)
                    {
                        if (value != null)
                        {
                            switch (value.GetType().Name)
                            {
                                case "String":
                                    //jsonRowValues.Add(JsonValue.CreateStringValue((string)value));
                                    jsonRowValues.Add((string)value);
                                    break;
                                case "Double":
                                    //jsonRowValues.Add(JsonValue.CreateNumberValue((double)value));
                                    jsonRowValues.Add((double)value);
                                    break;
                                case "Int32":
                                    //jsonRowValues.Add(JsonValue.CreateNumberValue((Int32)value));
                                    jsonRowValues.Add((Int32)value);
                                    break;
                                case "Boolean":
                                    //jsonRowValues.Add(JsonValue.CreateBooleanValue((bool)value));
                                    jsonRowValues.Add((bool)value);
                                    break;
                                default:
                                    throw new ArgumentException($"Unknown type: {value.GetType().Name}");
                            }
                        }
                        else
                        {
                            //jsonRowValues.Add(JValue.Parse("null"));
                            jsonRowValues.Add(null);
                        }
                    }
                    jsonValues.Add(jsonRowValues);
                }
            }
            return jsonValues;
        }
        #endregion

        #region Logging
        protected void LogResult(object result)
        {
            if (Service.ResponseViewModel != null)
            {
                Service.ResponseViewModel.Result = result;
            }
        }
        #endregion
    }
}
