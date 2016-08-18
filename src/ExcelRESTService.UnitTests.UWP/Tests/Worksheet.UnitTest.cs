/*
 *  Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license.
 *  See LICENSE in the source repository root for complete license information.
 */

using System;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

using ExcelRESTService.UnitTests.UWP.Helpers;

namespace ExcelRESTService.UnitTests.UWP
{
    [TestClass]
    public class WorksheetTests
    {
        [TestMethod]
        public async Task ListWorksheets()
        {
            // Arrange
            var item = await TestHelpers.UploadFile();
            
            // Act
            var worksheets = await App.ExcelService.ListWorksheetsAsync(item.Id);
            // Assert
            Assert.AreEqual(4, worksheets.Length, "Count of worksheets is not 4");
            Assert.AreEqual("Sheet1", worksheets[0].Name, "First worksheet is not named 'Sheet1'");
            Assert.AreEqual("Sheet2", worksheets[1].Name, "Second worksheet is not named 'Sheet2'");
            Assert.AreEqual("Sheet3", worksheets[2].Name, "Third worksheet is not named 'Sheet3'");
        }


        [TestMethod]
        public async Task AddWorksheet()
        {
            // Arrange
            var item = await TestHelpers.UploadFile();
            var sessionId = (await App.ExcelService.CreateSessionAsync(item.Id)).Id;

            var newSheetName = "NewSheet";

            // Act
            var worksheet = await App.ExcelService.AddWorksheetAsync(item.Id, newSheetName, sessionId);

            // Assert
            var worksheets = await App.ExcelService.ListWorksheetsAsync(item.Id, sessionId);

            await App.ExcelService.CloseSessionAsync(item.Id, sessionId);

            Assert.AreEqual(5, worksheets.Length, "Count of worksheets is not 5");
            Assert.AreEqual("Sheet1", worksheets[0].Name, "First worksheet is not named 'Sheet1'");
            Assert.AreEqual("Sheet2", worksheets[1].Name, "Second worksheet is not named 'Sheet2'");
            Assert.AreEqual("Sheet3", worksheets[2].Name, "Third worksheet is not named 'Sheet3'");
            Assert.AreEqual("Sheet4", worksheets[3].Name, "Third worksheet is not named 'Sheet4'");
            Assert.AreEqual(newSheetName, worksheets[4].Name, $"Fourth worksheet is not named '{newSheetName}'");
        }

        [TestMethod]
        public async Task GetUsedRange()
        {
            // Arrange
            var item = await TestHelpers.UploadFile();

            // Act
            var range = await App.ExcelService.WorksheetGetUsedRangeAsync(item.Id, "Sheet1");
            // Assert
            Assert.AreEqual(162, range.CellCount, "Cell count is not 162");
            Assert.AreEqual(6, range.ColumnCount, "Column count is not 6");
            Assert.AreEqual(27, range.RowCount, "Row count is not 27");
        }
    }
}
