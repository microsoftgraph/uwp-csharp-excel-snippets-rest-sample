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
    public class ResponseViewModel : ViewModelBase
    {
        #region Constructor
        public ResponseViewModel()
        {
            model = new Response();
        }
        #endregion

        #region Properties
        private Response model;

        public Response Model
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
        // StatusCode
        public string StatusCode
        {
            get
            {
                return model.StatusCode;
            }
            set
            {
                if (value != model.StatusCode)
                {
                    model.StatusCode = value;
                    OnPropertyChanged("StatusCode");
                }
            }
        }
        // ReasonPhrase
        public string ReasonPhrase
        {
            get
            {
                return model.ReasonPhrase;
            }
            set
            {
                if (value != model.ReasonPhrase)
                {
                    model.ReasonPhrase = value;
                    OnPropertyChanged("ReasonPhrase");
                }
            }
        }
        // ErrorMessage
        public string ErrorMessage
        {
            get
            {
                return model.ErrorMessage;
            }
            set
            {
                if (value != model.ErrorMessage)
                {
                    model.ErrorMessage = value;
                    OnPropertyChanged("ErrorMessage");
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
        // Result
        public object Result
        {
            get
            {
                return model.Result;
            }
            set
            {
                if (value != model.Result)
                {
                    model.Result = value;
                    OnPropertyChanged("Result");
                }
            }
        }
        #endregion

        #region Methods
        public void Clear()
        {
            StatusCode = string.Empty;
            ReasonPhrase = string.Empty;
            ErrorMessage = string.Empty;
            Headers = null;
            Body = string.Empty;
            Result = null;
        }
        #endregion
    }
}
