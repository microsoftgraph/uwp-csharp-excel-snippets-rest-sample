/*
 *  Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license.
 *  See LICENSE in the source repository root for complete license information.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Office365Service.Model;

namespace Office365Service.ViewModel
{
    public class RequestViewModel : ViewModelBase
    {
        #region Constructor
        public RequestViewModel()
        {
            model = new Request();
        }
        #endregion

        #region Properties
        private Request model;

        public Request Model
        {
            get
            {
                return model;
            }
            set
            {
                if (value != model)
                {
                    model = value;
                }
            }
        }
        // Api
        private RestApiViewModel apiVM;
        public RestApiViewModel Api
        {
            get
            {
                if (apiVM == null)
                {
                    apiVM = new RestApiViewModel(model.Api);
                }
                return apiVM;
            }
            set
            {
                if (apiVM != value)
                {
                    apiVM = value;
                    OnPropertyChanged("Api");
                }
            }
        }

        // Headers
        public List<KeyValue> Headers
        {
            get
            {
                return model.Headers;
            }
            set
            {
                if (value != model.Headers)
                {
                    model.Headers = value;
                    OnPropertyChanged("Headers");
                }
            }
        }
        // Body
        public string Body
        {
            get
            {
                return model.Body;
            }
            set
            {
                if (value != model.Body)
                {
                    model.Body = value;
                    OnPropertyChanged("Body");
                }
            }
        }
        #endregion

        #region Methods
        public void Clear()
        {
            Headers = null;
            Body = string.Empty;
        }
        #endregion
    }
}
