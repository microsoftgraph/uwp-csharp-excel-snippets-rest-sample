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

namespace Office365Service.Model
{
    public class Request
    {
        public IRestApi Api;
        public List<KeyValue> Headers;
        public string Body;
        public string ErrorMessage = "";
    }
}
