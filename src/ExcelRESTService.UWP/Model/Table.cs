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
    public class Table
    {
        #region Properties
        public string Id { get; set; }
        public string Name { get; set; }
        public bool? ShowHeaders { get; set; }
        public bool? ShowTotals { get; set; }
        public string Style { get; set; }
        #endregion

        #region Methods
        public static Table MapFromJson(JObject json)
        {
            var table = new Table();
            table.Id = RestApi.MapStringFromJson(json, "id");
            table.Name = RestApi.MapStringFromJson(json, "name");
            table.ShowHeaders = RestApi.MapBooleanFromJson(json, "showHeaders");
            table.ShowTotals = RestApi.MapBooleanFromJson(json, "showTotals");
            table.Style = RestApi.MapStringFromJson(json, "style");
            return table;
        }

        public static JObject MapToJson(Table table)
        {
            var json = new JObject();
            if (table.Name != null)
            {
                json.Add("name", table.Name);
            }
            if (table.ShowHeaders != null)
            {
                json.Add("showHeaders", table.ShowHeaders);
            }
            if (table.ShowTotals != null)
            {
                json.Add("showTotals", table.ShowTotals);
            }
            if (table.Style != null)
            {
                json.Add("style", table.Style);
            }
            return json;
        }
        #endregion
    }
}
