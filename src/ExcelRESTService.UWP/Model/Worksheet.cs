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
    public class Worksheet
    {
        #region Properties
        public string Id { get; set; }
        public string Name { get; set; }
        public int Position { get; set; }
        public string Visibility { get; set; }
        public Chart[] Charts { get; set; }
        #endregion

        #region Methods
        public static Worksheet MapFromJson(JObject json)
        {
            var worksheet = new Worksheet();
            worksheet.Id = RestApi.MapStringFromJson(json, "id");
            worksheet.Name = RestApi.MapStringFromJson(json, "name");
            worksheet.Position = (int)RestApi.MapNumberFromJson(json, "position");
            worksheet.Visibility = RestApi.MapStringFromJson(json, "visibility");

            JToken token = null;
            if (json.TryGetValue("charts", out token))
            {
                var jsonCharts = token.ToArray();
                var charts = new List<Chart>();
                foreach (var jsonChart in jsonCharts)
                {
                    var chart = Chart.MapFromJson(jsonChart.ToObject<JObject>());
                    chart.Worksheet = worksheet;
                    charts.Add(chart);
                }
                worksheet.Charts = charts.ToArray();
            }
            else
            {
                worksheet.Charts = null;
            }
            return worksheet;
        }
        #endregion
    }
}
