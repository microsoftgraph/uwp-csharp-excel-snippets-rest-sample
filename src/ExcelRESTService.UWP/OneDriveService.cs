/*
 *  Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license.
 *  See LICENSE in the source repository root for complete license information.
 */

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Linq;
using System.Net;

using Windows.Storage.Streams;

using Microsoft.OneDrive;

namespace Office365Service.OneDrive
{
    public partial class OneDriveService : RESTService 
    {
        #region Constructor
        public OneDriveService(Func<Task<string>> getAccessTokenAsync) : base(getAccessTokenAsync)
        {
        }

        public OneDriveService(Func<Task<CookieContainer>> getCookieContainerAsync) : base(getCookieContainerAsync)
        {
        }
        #endregion

        #region Properties
        #endregion

        #region Methods

        #region File
        // UploadFileAsync
        ItemApi uploadFileApi;

        public IRestApi UploadFileApi
        {
            get
            {
                if (uploadFileApi == null)
                    uploadFileApi =
                        new ItemApi(
                            this,
                            "Upload File",
                            "Upload contents of a new file or update the contents of an existing file.",
                            "PUT",
                            "/content",
                            typeof(Item)
                        );
                return uploadFileApi;
            }
        }
        public async Task<Item> UploadFileAsync(string path, string filename, IRandomAccessStreamWithContentType fileStream)
        {
            ((ItemApi)UploadFileApi).FileStream = fileStream;
            return (Item)(await ((ItemApi)UploadFileApi).InvokeAsync(path, filename));
        }

        // GetItemMetadata
        ItemApi getItemMetadataApi;

        public IRestApi GetItemMetadataApi
        {
            get
            {
                if (getItemMetadataApi == null)
                    getItemMetadataApi =
                        new ItemApi(
                            this,
                            "Get Item Metadata",
                            "Retrieve the metadata for an Item on OneDrive.",
                            "GET",
                            string.Empty,
                            typeof(Item)
                        );
                return getItemMetadataApi;
            }
        }
        public async Task<Item> GetItemMetadataAsync(string path, string filename, string expand = "")
        {
            return (Item)(await ((ItemApi)GetItemMetadataApi).InvokeAsync(path, filename));
        }
        public async Task<Item> GetItemMetadataAsync(string id)
        {
            return (Item)(await ((ItemApi)GetItemMetadataApi).InvokeAsync(id));
        }
        #endregion

        #endregion
    }
}

