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

using Office365Service;

using Microsoft.User;

namespace Office365Service.User
{
    class UserApi : RestApi, IRestApi
    {
        #region Constructor
        public UserApi(RESTService service, string title, string description, string method, string resourceFormat, Type resultType) 
            : base(service, title, description, method, string.Empty, resultType)
        {
            ResourceFormat = resourceFormat;
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
                return base.Resource + $"{ResourceFormat}";
            }
        }
        #endregion

        #region Mapping
        protected override object MapResult(JObject jsonResult)
        {
            object result;
            switch (ResultType.Name)
            {
                case "User":
                    result = Microsoft.User.User.MapFromJson(jsonResult);
                    LogResult(result);
                    return result;
                
                default:
                    return base.MapResult(jsonResult);
            }
        }
        #endregion
    }
}
