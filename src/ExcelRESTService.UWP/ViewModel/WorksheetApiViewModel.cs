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

namespace Office365Service.ViewModel
{
    public class WorksheetApiViewModel : WorkbookApiViewModel
    {
        #region Constructor
        public WorksheetApiViewModel(IRestApi model) : base(model)
        {
        }
        #endregion

        #region Properties
        // WorksheetId
        public string WorksheetId
        {
            get
            {
                return ((WorksheetApi)Model).WorksheetId;
            }
            set
            {
                if (value != ((WorksheetApi)Model).WorksheetId)
                {
                    ((WorksheetApi)Model).WorksheetId = value;
                    OnPropertyChanged("WorksheetId");
                    OnPropertyChanged("RequestUri");
                    invokeCommand.OnCanExecuteChanged();
                }
            }
        }
        #endregion

        #region Commands
        #region - Invoke
        protected override bool CanInvoke()
        {
            return ((WorksheetId != string.Empty) && base.CanInvoke());
        }
        #endregion
        #endregion
    }
}
