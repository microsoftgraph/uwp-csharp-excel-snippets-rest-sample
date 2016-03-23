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
    public class Chart
    {
        #region Properties
        public Worksheet Worksheet { get; set; }
        public string WorksheetId { get; set; }
        public string ChartId { get; set; }
        public string Name { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Left { get; set; }
        public double Top { get; set; }
        #endregion

        #region Methods
        public static Chart MapFromJson(JsonObject json)
        {
            var chart = new Chart();
            chart.WorksheetId = GetWorksheetId(json);
            chart.ChartId = json.GetNamedString("id");
            chart.Name = json.GetNamedString("name");
            chart.Width = (double)json.GetNamedNumber("width");
            chart.Height = (double)json.GetNamedNumber("height");
            chart.Left = (double)json.GetNamedNumber("left");
            chart.Top = (double)json.GetNamedNumber("top");
            return chart;
        }

        private static string GetWorksheetId(JsonObject json)
        {
            var odataId = Uri.UnescapeDataString(json.GetNamedString("@odata.id"));
            var id = odataId.Substring(odataId.IndexOf("worksheets(")+12);
            id = id.Substring(0, id.IndexOf(')')-1);
            return id;
        }
        #endregion
    }
}
