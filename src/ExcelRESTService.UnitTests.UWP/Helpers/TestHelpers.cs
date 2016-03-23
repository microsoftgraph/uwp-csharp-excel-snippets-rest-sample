/*
 *  Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license.
 *  See LICENSE in the source repository root for complete license information.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace ExcelRESTService.UnitTests.UWP.Helpers
{
    public static class TestHelpers
    {
        public static string GetFilename()
        {
            return $"Test {DateTime.Now:yyyy-MM-dd HH-mm-ss}.xlsx";
        }

        public async static Task<Microsoft.OneDrive.Item> UploadFile()
        {
            var filename = TestHelpers.GetFilename();
            Microsoft.OneDrive.Item item;
            var fileToUpload = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Content/template.xlsx"));
            using (var fileStream = await fileToUpload.OpenReadAsync())
            {
                item = await App.OneDriveService.UploadFileAsync("", filename, fileStream);
            }
            return item;
        }
    }
}
