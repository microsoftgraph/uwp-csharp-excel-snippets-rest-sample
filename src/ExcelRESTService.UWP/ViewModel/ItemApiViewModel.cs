/*
 *  Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license.
 *  See LICENSE in the source repository root for complete license information.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Office365Service.OneDrive;

namespace Office365Service.ViewModel
{
    public class ItemApiViewModel : RestApiViewModel
    {
        #region Constructor
        public ItemApiViewModel(IRestApi model) : base(model)
        {
            ItemId = ItemApi.LastItemId;
        }
        #endregion

        #region Properties
        // ItemId
        public string ItemId
        {
            get
            {
                return ((ItemApi)Model).ItemId;
            }
            set
            {
                if (value != ((ItemApi)Model).ItemId)
                { 
                    ((ItemApi)Model).ItemId = value;
                    OnPropertyChanged("ItemId");
                    OnPropertyChanged("RequestUri");
                    if (invokeCommand != null)
                    {
                        invokeCommand.OnCanExecuteChanged();
                    }
                }
            }
        }
        // ItemPath
        public string ItemPath
        {
            get
            {
                return ((ItemApi)Model).ItemPath;
            }
            set
            {
                if (value != ((ItemApi)Model).ItemPath)
                {
                    ((ItemApi)Model).ItemPath = value;
                    OnPropertyChanged("ItemPath");
                    OnPropertyChanged("RequestUri");
                    invokeCommand.OnCanExecuteChanged();
                }
            }
        }
        // ItemName
        public string ItemName
        {
            get
            {
                return ((ItemApi)Model).ItemName;
            }
            set
            {
                if (value != ((ItemApi)Model).ItemName)
                {
                    ((ItemApi)Model).ItemName = value;
                    OnPropertyChanged("ItemName");
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
            return (((ItemId != string.Empty) || (ItemName != string.Empty)) && base.CanInvoke());
        }
        #endregion
        #endregion
    }
}
