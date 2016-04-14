/*
 *  Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license.
 *  See LICENSE in the source repository root for complete license information.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.IO;

using Microsoft.ApplicationInsights;

namespace Office365Service.ViewModel
{
    public class RestApiViewModel : ViewModelBase
    {
        #region Constructor
        public RestApiViewModel(IRestApi model)
        {
            Model = model;

            BodyProperties.CollectionChanged += BodyProperties_CollectionChanged;
            BodyProperties.PropertyChanged += BodyProperties_PropertyChanged;
        }


        #endregion

        #region Properties
        private IRestApi model;

        public IRestApi Model
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
        
        // Title
        public string Title
        {
            get
            {
                return model.Title;
            }
            set
            {
                if (value != model.Title)
                {
                    model.Title = value;
                    OnPropertyChanged("Title");
                }
            }
        }
        // Description
        public string Description
        {
            get
            {
                return model.Description;
            }
            set
            {
                if (value != model.Description)
                {
                    model.Description = value;
                    OnPropertyChanged("Description");
                }
            }
        }
        // Method
        public string Method
        {
            get
            {
                return model.Method;
            }
            set
            {
                if (value != model.Method)
                {
                    model.Method = value;
                    OnPropertyChanged("Method");
                }
            }
        }
        // QueryParameters
        public string QueryParameters
        {
            get
            {
                return model.QueryParameters;
            }
            set
            {
                if (value != model.QueryParameters)
                {
                    model.QueryParameters = value;
                    OnPropertyChanged("QueryParameters");
                    OnPropertyChanged("RequestUri");
                }
            }
        }
        // Headers
        public ObservableDictionary<string, string> Headers
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
        // BodyProperties
        public ObservableDictionary<string, object> BodyProperties
        {
            get
            {
                return model.BodyProperties;
            }
            set
            {
                if (value != model.BodyProperties)
                {
                    model.BodyProperties = value;
                    OnPropertyChanged("BodyProperties");
                    OnPropertyChanged("BodyAsText");
                }
            }
        }

        private void BodyProperties_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("BodyAsText");
        }

        private void BodyProperties_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnPropertyChanged("BodyAsText");
        }

        // BodyAsText
        public string BodyAsText
        {
            get
            {
                return model.BodyAsText;
            }
        }
        // FileStream
        public Stream FileStream
        {
            get
            {
                return model.FileStream;
            }
            set
            {
                if (value != model.FileStream)
                {
                    model.FileStream = value;
                    OnPropertyChanged("FileStream");
                }
            }
        }
        // RequestUri
        public Uri RequestUri
        {
            get
            {
                return model.RequestUri;
            }
            set
            {
                if (value != model.RequestUri)
                {
                    model.RequestUri = value;
                    OnPropertyChanged("RequestUri");
                }
            }
        }
        // IsLoading
        private bool isLoading = false;
        public bool IsLoading
        {
            get
            {
                return isLoading;
            }
            set
            {
                if (value != isLoading)
                {
                    isLoading = value;
                    OnPropertyChanged("IsLoading");
                    if (invokeCommand != null)
                    {
                        invokeCommand.OnCanExecuteChanged();
                    }
                }
            }
        }
        #endregion

        #region Methods
        //public void Clear()
        //{
        //    Method = string.Empty;
        //    RequestUri = null;
        //    Headers = null;
        //    Body = string.Empty;
        //}
        #endregion

        #region Commands
        #region - Invoke
        protected DelegateCommand invokeCommand;

        public ICommand InvokeCommand
        {
            get
            {
                if (invokeCommand == null)
                {
                    invokeCommand = new DelegateCommand(
                        async param => await Invoke(),
                        param => CanInvoke());
                }
                return invokeCommand;
            }
        }

        protected virtual async Task Invoke()
        {
            try
            {
                IsLoading = true;

                // Telemetry
                var tc = new TelemetryClient();
                tc.TrackEvent($"Commands/{Model.GetType().Name}{Model.ResourceFormat}/Invoke");

                // Invoke
                Model.Service.ResponseViewModel.Clear();
                await Model.InvokeAsync();
            }
            catch (Exception ex)
            {
                Model.Service.ResponseViewModel.ErrorMessage = ex.Message;
            }
            finally
            {
                IsLoading = false;
            }
        }

        protected virtual bool CanInvoke()
        {
            return (!IsLoading);
        }
        #endregion
        #endregion
    }
}
