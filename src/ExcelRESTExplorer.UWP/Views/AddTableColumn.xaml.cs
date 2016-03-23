/*
*  Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license.
*  See LICENSE in the source repository root for complete license information.
*/

using System;

using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Controls;

namespace ExcelServiceExplorer.Views
{
    public sealed partial class AddTableColumn : Page
    {
        public AddTableColumn()
        {
            InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Disabled;

            App.ExcelService.RequestViewModel.Api.BodyProperties["index"] = 0;
            App.ExcelService.RequestViewModel.Api.BodyProperties["values"] =
                new object[] {
                    new object[] { "Column" },
                    new object[] { DateTime.Now.ToString() },
                    new object[] { "natalie@northwindtraders.com" },
                    new object[] { "Work" },
                    new object[] { 36000 },
                    new object[] { null }
                };
        }
    }
}
