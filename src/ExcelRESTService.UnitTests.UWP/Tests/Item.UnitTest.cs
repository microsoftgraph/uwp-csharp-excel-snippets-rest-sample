/*
 *  Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license.
 *  See LICENSE in the source repository root for complete license information.
 */

using System;
using System.Threading.Tasks;
using System.IO;

using Windows.Storage;

using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using AsyncAssert = Microsoft.VisualStudio.TestPlatform.UnitTestFramework.AppContainer.Assert;

using ExcelRESTService.UnitTests.UWP.Helpers;

namespace ExcelRESTService.UnitTests.UWP
{
    [TestClass]
    public class ItemTests
    {
        [TestMethod]
        public async Task UploadFile()
        {
            // Arrange
            var filename = TestHelpers.GetFilename();
            // Act
            Microsoft.OneDrive.Item item;
            var fileToUpload = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Content/template.xlsx"));
            using (var fileStream = await fileToUpload.OpenReadAsync())
            {
                item = await App.OneDriveService.UploadFileAsync("", filename, WindowsRuntimeStreamExtensions.AsStream(fileStream));
            }
            // Assert
            Assert.AreNotEqual(string.Empty, item.Id, "Item Id is blank");
            Assert.AreEqual(filename, item.Name, $"Filename is not {filename}");
            Assert.AreEqual(30779, item.Size, $"File size is not 30779");
        }

        [TestMethod]
        public async Task GetItemMetadataByName()
        {
            // Arrange
            var item = await TestHelpers.UploadFile();
            // Act
            var item2 = await App.OneDriveService.GetItemMetadataAsync("", item.Name);
            // Assert
            Assert.AreEqual(item.Id, item2.Id, "Item ids are not equal");
            Assert.AreEqual(item.Name, item2.Name, $"Filenames are not equal");
            Assert.AreEqual(item.Size, item2.Size, $"File sizes are not equal");
        }

        [TestMethod]
        public async Task GetItemMetadataById()
        {
            // Arrange
            var item = await TestHelpers.UploadFile();
            // Act
            var item2 = await App.OneDriveService.GetItemMetadataAsync(item.Id);
            // Assert
            Assert.AreEqual(item.Id, item2.Id, "Item ids are not equal");
            Assert.AreEqual(item.Name, item2.Name, $"Filenames are not equal");
            Assert.AreEqual(item.Size, item2.Size, $"File sizes are not equal");
        }

        [TestMethod]
        public async Task GetItemMetadataByName_NonExisting_ThrowsException()
        {
            // Arrange
            var filename = TestHelpers.GetFilename();
            // Act
            await AsyncAssert.ThrowsException<Exception>(
                 async () =>
                 {
                     var item2 = await App.OneDriveService.GetItemMetadataAsync("", filename);
                 }
            );
            // Assert
        }

        [TestMethod]
        public async Task GetItemMetadataById_NonExisting_ThrowsException()
        {
            // Arrange
            var id = "NotAnIdThatExist";
            // Act
            await AsyncAssert.ThrowsException<Exception>(
                 async () =>
                 {
                     var item2 = await App.OneDriveService.GetItemMetadataAsync("", id);
                 }
            );
            // Assert
        }
    }
}
