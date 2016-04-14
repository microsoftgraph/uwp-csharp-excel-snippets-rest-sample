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
    public class ChartTitle
    {
        #region Properties
        public string WorksheetId { get; set; }
        public string ChartId { get; set; }
        public bool? Overlay { get; set; }
        public string Text { get; set; }
        public bool? Visible { get; set; }
        #endregion

        #region Methods
        public static ChartTitle MapFromJson(JObject json)
        {
            var chartTitle = new ChartTitle();
            chartTitle.WorksheetId = GetWorksheetId(json);
            chartTitle.ChartId = GetChartId(json);
            chartTitle.Overlay = RestApi.MapBooleanFromJson(json, "overlay");
            chartTitle.Text = RestApi.MapStringFromJson(json, "text");
            chartTitle.Visible = RestApi.MapBooleanFromJson(json, "visible");
            return chartTitle;
        }

        private static string GetWorksheetId(JObject json)
        {
            var odataId = Uri.UnescapeDataString(RestApi.MapStringFromJson(json, "@odata.id"));
            var id = odataId.Substring(odataId.IndexOf("worksheets(") + 12);
            id = id.Substring(0, id.IndexOf(')') - 1);
            return id;
        }

        private static string GetChartId(JObject json)
        {
            var odataId = Uri.UnescapeDataString(RestApi.MapStringFromJson(json, "@odata.id"));
            var id = odataId.Substring(odataId.IndexOf("charts(") + 8);
            id = id.Substring(0, id.IndexOf(')') - 1);
            return id;
        }
        #endregion
    }
}
