/*
 *  Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license.
 *  See LICENSE in the source repository root for complete license information.
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;

using Office365Service;

namespace Microsoft.ExcelServices
{
    public class NamedItem
    {
        #region Properties
        public string Name { get; set; }
        public string Type { get; set; }
        public object Value { get; set; }
        public bool Visible { get; set; }
        #endregion

        #region Methods
        public static NamedItem MapFromJson(JObject json)
        {
            var namedItem = new NamedItem();
            namedItem.Name = RestApi.MapStringFromJson(json, "name");
            namedItem.Type = RestApi.MapStringFromJson(json, "type");

            switch (json.SelectToken("value").Type)
            {
                case JTokenType.String:
                    namedItem.Value = RestApi.MapStringFromJson(json, "value");
                    break;
                case JTokenType.Integer:
                    namedItem.Value = (int)(RestApi.MapNumberFromJson(json, "value"));
                    break;
                case JTokenType.Float:
                    namedItem.Value = RestApi.MapNumberFromJson(json, "value");
                    break;
                case JTokenType.Boolean:
                    namedItem.Value = RestApi.MapBooleanFromJson(json, "value");
                    break;
                default:
                    throw new Exception($"{json.SelectToken("value").Type} is an unknown type");
            }
            
            namedItem.Visible = (bool)RestApi.MapBooleanFromJson(json, "visible");
            return namedItem;
        }
        #endregion
    }
}
