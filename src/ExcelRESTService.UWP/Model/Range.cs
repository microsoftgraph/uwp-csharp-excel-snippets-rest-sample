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
        public static Range MapFromJson(JObject json)
        {
            var range = new Range();
            range.Address = RestApi.MapStringFromJson(json, "address");
            range.CellCount = (int?)RestApi.MapNumberFromJson(json, "cellCount");
            range.ColumnCount = (int?)RestApi.MapNumberFromJson(json, "columnCount");
            range.ColumnIndex = (int?)RestApi.MapNumberFromJson(json, "columnIndex");
            range.RowCount = (int?)RestApi.MapNumberFromJson(json, "rowCount");
            range.RowIndex = (int?)RestApi.MapNumberFromJson(json, "rowIndex");
            range.Values = RestApi.MapValuesFromJson(json, "values");
            range.Text = RestApi.MapValuesFromJson(json, "text");
            range.ValueTypes = RestApi.MapValuesFromJson(json, "valueTypes");
            range.Formulas = RestApi.MapValuesFromJson(json, "formulas");
            range.FormulasR1C1 = RestApi.MapValuesFromJson(json, "formulasR1C1");
            range.NumberFormat = RestApi.MapValuesFromJson(json, "numberFormat");
            return range;
        }


        #endregion
    }
}
