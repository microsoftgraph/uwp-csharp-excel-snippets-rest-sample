/*
 *  Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license.
 *  See LICENSE in the source repository root for complete license information.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;

using Microsoft.OneDrive;

using Office365Service;

namespace Office365Service.OneDrive
{
    class ItemApi : RestApi, IRestApi
    {
        public static string LastItemId = string.Empty;

        #region Constructor
        public ItemApi(RESTService service, string title, string description, string method, string resourceFormat, Type resultType) 
            : base(service, title, description, method, string.Empty, resultType)
        {
            ResourceFormat = resourceFormat;

            ItemPath = string.Empty;
            ItemName = string.Empty;
        }
        #endregion

        #region Properties
        // ResourceFormat
        public new string ResourceFormat { get; set; }
        // Resource
        public override string Resource
        {
            get
            {
                if (ItemId != string.Empty)
                {
                    return base.Resource + $"/me/drive/items/{ItemId}{ResourceFormat}";
                }
                else if (ItemPath != string.Empty)
                {
                    // TODO: Need to escape / and : in filename
                    return base.Resource + $"/me/drive/items/root:{ItemPath}/{ItemName}:{ResourceFormat}";
                }
                else
                {
                    // TODO: Need to escape / and : in filename
                    return base.Resource + $"/me/drive/items/root:/{ItemName}:{ResourceFormat}";
                }
            }
        }
        // ItemId
        private string itemId = LastItemId;
        public virtual string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LastItemId = itemId;
            }
        }
        // ItemPath
        public virtual string ItemPath { get; set; }
        // ItemName
        public virtual string ItemName { get; set; }
        #endregion

        #region Methods
        public async Task<object> InvokeAsync(string id)
        {
            ItemId = id;
            ItemPath = string.Empty;
            ItemName = string.Empty;

            return await InvokeAsync();
        }

        public async Task<object> InvokeAsync(string path, string filename)
        {
            ItemId = string.Empty;
            ItemPath = path;
            ItemName = filename;

            return await InvokeAsync();
        }
        #endregion

        #region Mapping
        protected override object MapResult(JObject jsonResult)
        {
            if (ResultType != null)
            {
                object result;
                switch (ResultType.Name)
                {
                    case "Item":
                        result = Item.MapFromJson(jsonResult);
                        LogResult(result);
                        LastItemId = ((Item)result).Id;
                        return result;

                    default:
                        return base.MapResult(jsonResult);
                }
            }
            else
            {
                return null;
            }
        }
        #endregion

    }
}
