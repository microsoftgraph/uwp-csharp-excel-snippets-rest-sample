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
    public class TableRow
    {
        #region Properties
        public int Index { get; set; }
        public object[][] Values { get; set; }
        #endregion

        #region Methods
        public static TableRow MapFromJson(JsonObject json)
        {
            var row = new TableRow();
            row.Index = (int)json.GetNamedNumber("index");
            row.Values = Range.MapValuesFromJson(json, "values");
            return row;
        }
        #endregion
    }
}
