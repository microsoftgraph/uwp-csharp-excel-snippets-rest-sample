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
    public class ChartApiViewModel : WorksheetApiViewModel
    {
        #region Constructor
        public ChartApiViewModel(IRestApi model) : base(model)
        {
        }
        #endregion

        #region Properties
        // ChartId
        public string ChartId
        {
            get
            {
                return ((ChartApi)Model).ChartId;
            }
            set
            {
                if (value != ((ChartApi)Model).ChartId)
                {
                    ((ChartApi)Model).ChartId = value;
                    OnPropertyChanged("ChartId");
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
            return ((ChartId != string.Empty) && base.CanInvoke());
        }
        #endregion
        #endregion
    }
}
