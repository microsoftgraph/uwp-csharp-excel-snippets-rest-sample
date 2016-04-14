/*
 *  Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license.
 *  See LICENSE in the source repository root for complete license information.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Office365Service.Model
{
    public class Response
    {
        public string StatusCode;
        public string ReasonPhrase;
        public List<KeyValue> Headers;
        public string ErrorMessage = "";
        public string Body;
        public object Result;
    }
}
