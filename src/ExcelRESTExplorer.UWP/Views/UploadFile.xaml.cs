/*
 *  Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license.
 *  See LICENSE in the source repository root for complete license information.
 */

using System;

using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Controls;
using Windows.Storage;

namespace ExcelServiceExplorer.Views
{
    public sealed partial class UploadFile : Page
    {
        public UploadFile()
        {
            InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Disabled;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            var fileToUpload = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Content/template.xlsx"));
            var fileStream = await fileToUpload.OpenReadAsync();

            App.OneDriveService.RequestViewModel.Api.FileStream = fileStream;
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            App.OneDriveService.RequestViewModel.Api.FileStream.Dispose();
            App.OneDriveService.RequestViewModel.Api.FileStream = null;
        }
    }
}

