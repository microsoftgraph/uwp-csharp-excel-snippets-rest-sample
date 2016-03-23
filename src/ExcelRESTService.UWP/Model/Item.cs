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
        //public string WebDavUrl { get; set; }
        #endregion

        #region Methods
        public static Item MapFromJson(JsonObject json)
        {
            var item = new Item();
            item.Id = json.GetNamedString("id");
            item.Name = json.GetNamedString("name");
            item.CTag = json.GetNamedString("cTag");
            item.ETag = json.GetNamedString("eTag");
            item.CreatedDateTime = DateTime.Parse(json.GetNamedString("createdDateTime")).ToLocalTime();
            item.LastModifiedDateTime = DateTime.Parse(json.GetNamedString("lastModifiedDateTime")).ToLocalTime();
            item.Size = (int)(json.GetNamedNumber("size"));
            item.WebUrl = json.GetNamedString("webUrl");
            //item.WebDavUrl = json.GetNamedString("webDavUrl");
            return item;
        }
        #endregion
    }
}
