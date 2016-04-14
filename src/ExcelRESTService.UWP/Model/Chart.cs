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
        public static Chart MapFromJson(JObject json)
        {
            var chart = new Chart();
            chart.WorksheetId = GetWorksheetId(json);
            chart.ChartId = RestApi.MapStringFromJson(json, "id");
            chart.Name = RestApi.MapStringFromJson(json, "name");
            chart.Width = (double)RestApi.MapNumberFromJson(json, "width");
            chart.Height = (double)RestApi.MapNumberFromJson(json, "height");
            chart.Left = (double)RestApi.MapNumberFromJson(json, "left");
            chart.Top = (double)RestApi.MapNumberFromJson(json, "top");
            return chart;
        }

        private static string GetWorksheetId(JObject json)
        {
            var odataId = Uri.UnescapeDataString(RestApi.MapStringFromJson(json, "@odata.id"));
            var id = odataId.Substring(odataId.IndexOf("worksheets(")+12);
            id = id.Substring(0, id.IndexOf(')')-1);
            return id;
        }
        #endregion
    }
}
