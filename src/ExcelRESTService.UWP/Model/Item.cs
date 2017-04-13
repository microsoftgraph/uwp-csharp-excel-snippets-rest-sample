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

namespace Microsoft.OneDrive
{
    public class Item
    {
        #region Properties
        public string Id { get; set; }
        public string Name { get; set; }
        public string CTag { get; set; }
        public string ETag { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime LastModifiedDateTime { get; set; }
        public int Size { get; set; }
        public string WebUrl { get; set; }
        #endregion

        #region Methods
        public static Item MapFromJson(JObject json)
        {
            var item = new Item();
            try
            {
                item.Id = RestApi.MapStringFromJson(json, "id");
                item.Name = RestApi.MapStringFromJson(json, "name");
                item.CTag = RestApi.MapStringFromJson(json, "cTag");
                item.ETag = RestApi.MapStringFromJson(json, "eTag");                                
                item.CreatedDateTime = json.GetValue("createdDateTime").Value<DateTime>();                
                item.LastModifiedDateTime = json.GetValue("lastModifiedDateTime").Value<DateTime>();
                item.Size = (int)(RestApi.MapNumberFromJson(json, "size"));
                item.WebUrl = RestApi.MapStringFromJson(json, "webUrl");
            }
            catch (Exception e)
            {
                var exxx = e;
            }
            return item;
        }
        #endregion
    }
}
