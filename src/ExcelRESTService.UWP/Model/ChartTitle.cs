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
using Windows.Data.Json;

namespace Microsoft.ExcelServices
{
    public class ChartTitle
    {
        #region Properties
        public string WorksheetId { get; set; }
        public string ChartId { get; set; }
        public bool Overlay { get; set; }
        public string Text { get; set; }
        public bool Visible { get; set; }
        #endregion

        #region Methods
        public static ChartTitle MapFromJson(JsonObject json)
        {
            var chartTitle = new ChartTitle();
            chartTitle.WorksheetId = GetWorksheetId(json);
            chartTitle.ChartId = GetChartId(json);
            chartTitle.Overlay = json.GetNamedBoolean("overlay");
            chartTitle.Text = json.GetNamedString("text");
            chartTitle.Visible = json.GetNamedBoolean("visible");
            return chartTitle;
        }

        private static string GetWorksheetId(JsonObject json)
        {
            var odataId = Uri.UnescapeDataString(json.GetNamedString("@odata.id"));
            var id = odataId.Substring(odataId.IndexOf("worksheets(") + 12);
            id = id.Substring(0, id.IndexOf(')') - 1);
            return id;
        }

        private static string GetChartId(JsonObject json)
        {
            var odataId = Uri.UnescapeDataString(json.GetNamedString("@odata.id"));
            var id = odataId.Substring(odataId.IndexOf("charts(") + 8);
            id = id.Substring(0, id.IndexOf(')') - 1);
            return id;
        }
        #endregion
    }
}
