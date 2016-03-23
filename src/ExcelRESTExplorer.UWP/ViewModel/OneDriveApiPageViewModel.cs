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
    public class OneDriveApiPageViewModel : DetailPageViewModel
    {
        #region Constructor
        public OneDriveApiPageViewModel()
        {
            Request = App.OneDriveService.RequestViewModel;
            Response = App.OneDriveService.ResponseViewModel;
        }
        #endregion  
    }
}

