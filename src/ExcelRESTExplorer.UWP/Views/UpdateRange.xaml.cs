/*
 *  Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license.
 *  See LICENSE in the source repository root for complete license information.
 */

using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Controls;

namespace ExcelServiceExplorer.Views
{
    public sealed partial class UpdateRange : Page
    {
        public UpdateRange()
        {
            InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Disabled;

            App.ExcelService.RequestViewModel.Api.BodyProperties["values"] = new object[] { new object[] { "ID", "Created", "User Name", "Work", 45000, null } };
        }
    }
}
