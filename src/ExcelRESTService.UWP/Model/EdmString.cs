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

namespace Microsoft.Edm
{
    public class EdmString
    {
        #region Properties
        public string Value { get; set; }
        #endregion

        #region Methods
        public static EdmString MapFromJson(JsonObject json)
        {
            var edmString = new EdmString();
            edmString.Value = json.GetNamedString("value");
            return edmString;
        }
        #endregion
    }
}
