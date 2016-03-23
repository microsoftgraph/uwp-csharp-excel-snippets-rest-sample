/*
 *  Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license.
 *  See LICENSE in the source repository root for complete license information.
 */

using System;
using System.Diagnostics;
using System.Threading.Tasks;

using Windows.Web.Http;

namespace Office365Service
{
    public class WebService
    {
        #region Constructor
        public WebService()
        {
        }
        #endregion

        #region Properties
        // Url
        private string url = null;
        public virtual string Url
        {
            get
            {
                return url;
            }
            set
            {
                if (value != url)
                {
                    url = value;
                }
            }
        }
        #endregion

        protected virtual HttpClient GetWebRequest(Uri uri)
        {
            return new HttpClient();
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        protected async virtual Task<HttpClient> GetWebRequestAsync(Uri uri)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            return new HttpClient();
        }
    }
}
