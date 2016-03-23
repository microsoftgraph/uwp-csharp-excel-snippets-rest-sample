/*
 *  Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license.
 *  See LICENSE in the source repository root for complete license information.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Office365Service;

namespace Office365Service.Excel
{
    class RangeApi : WorksheetApi, IRestApi
    {
        #region Constructor
        public RangeApi(RESTService service, string title, string description, string method, string resourceFormat, Type resultType) 
            : base(service, title, description, method, string.Empty, resultType)
        {
            ResourceFormat = resourceFormat;
            Address = string.Empty;
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
                return base.Resource + $"/range(address='{Address}'){ResourceFormat}";
            }
        }
        // Address
        public string Address { get; set; }
        #endregion

        #region Methods
        public async Task<object> InvokeAsync(string id, string worksheetId, string address, string sessionId = "", string queryParameters = "")
        {
            Address = address;
            return await base.InvokeAsync(id, worksheetId, sessionId, queryParameters);
        }
        #endregion
    }
}
