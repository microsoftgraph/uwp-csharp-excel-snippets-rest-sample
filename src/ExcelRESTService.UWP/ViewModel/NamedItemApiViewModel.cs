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
    public class NamedItemApiViewModel : WorkbookApiViewModel
    {
        #region Constructor
        public NamedItemApiViewModel(IRestApi model) : base(model)
        {
        }
        #endregion

        #region Properties
        // NamedItemId
        public string NamedItemId
        {
            get
            {
                return ((NamedItemApi)Model).NamedItemId;
            }
            set
            {
                if (value != ((NamedItemApi)Model).NamedItemId)
                {
                    ((NamedItemApi)Model).NamedItemId = value;
                    OnPropertyChanged("NamedItemId");
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
            return ((NamedItemId != string.Empty) && base.CanInvoke());
        }
        #endregion
        #endregion
    }
}
