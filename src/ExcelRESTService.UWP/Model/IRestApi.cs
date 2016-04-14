/*
 *  Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license.
 *  See LICENSE in the source repository root for complete license information.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Newtonsoft.Json.Linq;

namespace Office365Service
{
    public interface IRestApi
    {
        #region Constructor
        #endregion

        #region Properties
        RESTService Service { get; set; }
        string Title { get; set; }
        string Description { get; set; }
        string Method { get; set; }
        string ResourceFormat { get; set; }
        string Resource { get; }
        string QueryParameters { get; set; }
        ObservableDictionary<string, string> Headers { get; set; }
        ObservableDictionary<string, object> BodyProperties { get; set; }
        JObject BodyAsJson { get; }
        string BodyAsText { get; }
        Stream FileStream { get; set; }
        Type ResultType { get; set; }
        Uri RequestUri { get; set; }
        #endregion
        
        #region Methods
        Task<object> InvokeAsync();
        #endregion
    }
}
