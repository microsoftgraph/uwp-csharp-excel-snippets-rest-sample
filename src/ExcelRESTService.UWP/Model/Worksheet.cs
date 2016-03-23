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
        public static Worksheet MapFromJson(JsonObject json)
        {
            var worksheet = new Worksheet();
            worksheet.Id = json.GetNamedString("id");
            worksheet.Name = json.GetNamedString("name");
            worksheet.Position = (int)json.GetNamedNumber("position");
            worksheet.Visibility = json.GetNamedString("visibility");

            if (json.ContainsKey("charts"))
            {
                var jsonCharts = json.GetNamedArray("charts");
                var charts = new List<Chart>();
                foreach (var jsonChart in jsonCharts)
                {
                    var chart = Chart.MapFromJson(jsonChart.GetObject());
                    chart.Worksheet = worksheet;
                    charts.Add(chart);
                }
                worksheet.Charts = charts.ToArray();
            }
            return worksheet;
        }
        public static void DebugPrint(Worksheet worksheet)
        {
            Debug.WriteLine($"Name: {worksheet.Name}");
            Debug.WriteLine($"Position: {worksheet.Position}");
            Debug.WriteLine($"Visibility: {worksheet.Visibility}");
        }
        #endregion
    }
}
