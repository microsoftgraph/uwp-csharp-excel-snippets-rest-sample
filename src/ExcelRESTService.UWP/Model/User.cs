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

namespace Microsoft.User
{
    public class User
    {
        #region Properties
        public string Id { get; set; }
        public string DisplayName { get; set; }
        #endregion

        #region Methods
        public static User MapFromJson(JObject json)
        {
            var user = new User();
            user.Id = RestApi.MapStringFromJson(json, "id");
            user.DisplayName = RestApi.MapStringFromJson(json, "displayName");
            return user;
        }
        #endregion
    }
}
