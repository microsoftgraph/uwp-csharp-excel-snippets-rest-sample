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
    public class TableApiViewModel : WorkbookApiViewModel
    {
        #region Constructor
        public TableApiViewModel(IRestApi model) : base(model)
        {
        }
        #endregion

        #region Properties
        // TableId
        public string TableId
        {
            get
            {
                return ((TableApi)Model).TableId;
            }
            set
            {
                if (value != ((TableApi)Model).TableId)
                {
                    ((TableApi)Model).TableId = value;
                    OnPropertyChanged("TableId");
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
            return ((TableId != string.Empty) && base.CanInvoke());
        }
        #endregion
        #endregion
    }
}
