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
        public static Table MapFromJson(JsonObject json)
        {
            var table = new Table();
            table.Id = json.GetNamedString("id");
            table.Name = json.GetNamedString("name");
            table.ShowHeaders = json.GetNamedBoolean("showHeaders");
            table.ShowTotals = json.GetNamedBoolean("showTotals");
            table.Style = json.GetNamedString("style");
            return table;
        }

        public static JsonObject MapToJson(Table table)
        {
            var json = new JsonObject();
            if (table.Name != null)
            {
                json.Add("name", JsonValue.CreateStringValue(table.Name));
            }
            if (table.ShowHeaders != null)
            {
                json.Add("showHeaders", JsonValue.CreateBooleanValue((bool)(table.ShowHeaders)));
            }
            if (table.ShowTotals != null)
            {
                json.Add("showTotals", JsonValue.CreateBooleanValue((bool)(table.ShowTotals)));
            }
            if (table.Style != null)
            {
                json.Add("style", JsonValue.CreateStringValue(table.Style));
            }
            return json;
        }

        public static void DebugPrint(Table table)
        {
            Debug.WriteLine($"Id: {table.Id}");
            Debug.WriteLine($"Name: {table.Name}");
        }
        #endregion
    }
}
