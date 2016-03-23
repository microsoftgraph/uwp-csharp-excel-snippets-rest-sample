/*
 *  Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license.
 *  See LICENSE in the source repository root for complete license information.
 */

using System.Collections.Generic;
using System.Threading.Tasks;

using Windows.UI.Xaml.Navigation;

using Template10.Services.NavigationService;

using Office365Service.ViewModel;

namespace ExcelServiceExplorer.ViewModel
{
    public class ExcelApiPageViewModel : DetailPageViewModel
    {
        #region Constructor
        public ExcelApiPageViewModel()
        {
            Request = App.ExcelService.RequestViewModel;
            Response = App.ExcelService.ResponseViewModel;
        }
        #endregion  
    }
}

