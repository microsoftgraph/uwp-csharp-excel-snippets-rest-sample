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
    public class TableRow
    {
        #region Properties
        public int? Index { get; set; }
        public object[][] Values { get; set; }
        #endregion

        #region Methods
        public static TableRow MapFromJson(JObject json)
        {
            var row = new TableRow();
            row.Index = (int?)RestApi.MapNumberFromJson(json, "index");
            row.Values = RestApi.MapValuesFromJson(json, "values");
            return row;
        }
        #endregion
    }
}
