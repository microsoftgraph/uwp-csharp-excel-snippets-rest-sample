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
    public abstract class DetailPageViewModel : Template10.Mvvm.ViewModelBase
    {
        #region Constructor
        public DetailPageViewModel()
        {
        }
        #endregion  

        #region Properties
        public RequestViewModel Request { get; set; }
        public ResponseViewModel Response { get; set; }
        #endregion

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            await Task.CompletedTask;
        }

        public override async Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending)
        {
            await Task.CompletedTask;
        }

        public override async Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            args.Cancel = false;
            await Task.CompletedTask;
        }
    }
}

