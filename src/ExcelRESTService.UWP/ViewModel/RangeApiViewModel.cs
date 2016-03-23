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
    public class RangeApiViewModel : WorksheetApiViewModel
    {
        #region Constructor
        public RangeApiViewModel(IRestApi model) : base(model)
        {
        }
        #endregion

        #region Properties
        // Address
        public string Address
        {
            get
            {
                return ((RangeApi)Model).Address;
            }
            set
            {
                if (value != ((RangeApi)Model).Address)
                {
                    ((RangeApi)Model).Address = value;
                    OnPropertyChanged("Address");
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
            return ((Address != string.Empty) && base.CanInvoke());
        }
        #endregion
        #endregion
    }
}
