/*
 *  Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license.
 *  See LICENSE in the source repository root for complete license information.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Office365Service.Excel;

using Microsoft.ExcelServices;

namespace Office365Service.ViewModel
{
    public class WorkbookApiViewModel : ItemApiViewModel
    {
        #region Constructor
        public WorkbookApiViewModel(IRestApi model) : base(model)
        {
            SessionId = SessionInfo.LastId;
        }
        #endregion

        #region Properties
        // WorkbookId
        public string WorkbookId
        {
            get
            {
                return ((WorkbookApi)Model).WorkbookId;
            }
            set
            {
                if (value != ((WorkbookApi)Model).WorkbookId)
                {
                    ((WorkbookApi)Model).WorkbookId = value;
                    OnPropertyChanged("WorkbookId");
                    OnPropertyChanged("RequestUri");
                    invokeCommand.OnCanExecuteChanged();
                }
            }
        }
        // WorkbookPath
        public string WorkbookPath
        {
            get
            {
                return ((WorkbookApi)Model).WorkbookPath;
            }
            set
            {
                if (value != ((WorkbookApi)Model).WorkbookPath)
                {
                    ((WorkbookApi)Model).WorkbookPath = value;
                    OnPropertyChanged("WorkbookPath");
                    OnPropertyChanged("RequestUri");
                    invokeCommand.OnCanExecuteChanged();
                }
            }
        }
        // WorkbookName
        public string WorkbookName
        {
            get
            {
                return ((WorkbookApi)Model).WorkbookName;
            }
            set
            {
                if (value != ((WorkbookApi)Model).WorkbookName)
                {
                    ((WorkbookApi)Model).WorkbookName = value;
                    OnPropertyChanged("WorkbookName");
                    OnPropertyChanged("RequestUri");
                    invokeCommand.OnCanExecuteChanged();
                }
            }
        }
        // SessionId
        public string SessionId
        {
            get
            {
                return ((WorkbookApi)Model).SessionId;
            }
            set
            {
                if (value != ((WorkbookApi)Model).SessionId)
                {
                    ((WorkbookApi)Model).SessionId = value;
                    OnPropertyChanged("SessionId");
                    OnPropertyChanged("Headers");
                    //invokeCommand.OnCanExecuteChanged();
                }
            }
        }
        #endregion

        #region Commands
        #region - Invoke
        protected override async Task Invoke()
        {
            if (RequestUri.LocalPath.EndsWith("/createSession"))
            {
                SessionInfo.LastId = string.Empty;
            }

            await base.Invoke();

            if (RequestUri.LocalPath.EndsWith("/closeSession"))
            {
                SessionInfo.LastId = string.Empty;
            }
        }

        protected override bool CanInvoke()
        {
            if (Model.Resource.EndsWith("/closeSession"))
            {
                return (SessionId != string.Empty) && base.CanInvoke();
            }
            else
            {
                return base.CanInvoke();
            }
        }
        #endregion
        #endregion
    }
}
