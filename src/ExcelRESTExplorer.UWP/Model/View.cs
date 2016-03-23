/*
 *  Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license.
 *  See LICENSE in the source repository root for complete license information.
 */

using System;

using Office365Service;

namespace ExcelServiceExplorer.Model
{
    public class View
    {
        #region Constructor
        public View(Type page, IRestApi api)
        {
            Page = page;
            Api = api;
        }
        #endregion

        #region Properties
        public Type Page { get; set; }
        public IRestApi Api { get; set; }
        #endregion
    }
}
