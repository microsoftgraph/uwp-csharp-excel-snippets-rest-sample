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

using Office365Service;

namespace Microsoft.ExcelServices
{
    public class Range
    {
        #region Properties
        public string Address { get; set; }
        public int? CellCount { get; set; }
        public int? ColumnCount { get; set; }
        public int? ColumnIndex { get; set; }
        public int? RowCount { get; set; }
        public int? RowIndex { get; set; }
        public object[][] Values { get; set; }
        public object[][] Text { get; set; }
        public object[][] ValueTypes { get; set; }
        public object[][] Formulas { get; set; }
        public object[][] FormulasR1C1 { get; set; }
        public object[][] NumberFormat { get; set; }
        #endregion

        #region Methods
        public static Range MapFromJson(JsonObject json)
        {
            var range = new Range();
            range.Address = RestApi.MapStringFromJson(json, "address");
            range.CellCount = (int?)RestApi.MapNumberFromJson(json, "cellCount");
            range.ColumnCount = (int?)RestApi.MapNumberFromJson(json, "columnCount");
            range.ColumnIndex = (int?)RestApi.MapNumberFromJson(json, "columnIndex");
            range.RowCount = (int?)RestApi.MapNumberFromJson(json, "rowCount");
            range.RowIndex = (int?)RestApi.MapNumberFromJson(json, "rowIndex");
            range.Values = Range.MapValuesFromJson(json, "values");
            range.Text = Range.MapValuesFromJson(json, "text");
            range.ValueTypes = Range.MapValuesFromJson(json, "valueTypes");
            range.Formulas = Range.MapValuesFromJson(json, "formulas");
            range.FormulasR1C1 = Range.MapValuesFromJson(json, "formulasR1C1");
            range.NumberFormat = Range.MapValuesFromJson(json, "numberFormat");
            return range;
        }

        public static object[][] MapValuesFromJson(JsonObject json, string name)
        {
            if (json.ContainsKey(name))
            {
                var rows = new List<object[]>();
                foreach (var jsonRow in json.GetNamedArray(name))
                {
                    var row = new List<object>();
                    foreach (var value in jsonRow.GetArray())
                    {
                        switch (value.ValueType)
                        {
                            case JsonValueType.String:
                                row.Add((string)(value.GetString()));
                                break;
                            case JsonValueType.Number:
                                row.Add((double)(value.GetNumber()));
                                break;
                            case JsonValueType.Boolean:
                                row.Add((bool)(value.GetBoolean()));
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
        #endregion
    }
}
