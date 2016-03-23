/*
*  Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license.
*  See LICENSE in the source repository root for complete license information.
*/

using System;

using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Controls;

namespace ExcelServiceExplorer.Views
{
    public sealed partial class AddTableRow : Page
    {
        public AddTableRow()
        {
            InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Disabled;

            App.ExcelService.RequestViewModel.Api.BodyProperties["index"] = null;
            App.ExcelService.RequestViewModel.Api.BodyProperties["values"] =
                new object[]
                {
                    new object[] { 1, DateTime.Now.ToString(), App.UserAccount.UserName, "Work", 36000, null }
                };
        }
    }
}
