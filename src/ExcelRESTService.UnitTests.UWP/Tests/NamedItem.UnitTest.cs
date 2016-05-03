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
    public class NamedItems
    {
        private Microsoft.OneDrive.Item item;

        [TestInitialize]
        public async Task Initialize()
        {
            item = await TestHelpers.UploadFile();
        }

        [TestMethod]
        public async Task ListNamedItems()
        {
            // Arrange
            // Act
            var namedItems = await App.ExcelService.ListNamedItemsAsync(item.Id);
            // Assert
            Assert.AreEqual(9, namedItems.Length, "Named item count is not 9");
        }

        [TestMethod]
        public async Task GetNamedItem()
        {
            // Arrange
            var namedItemId = "ChartData";
            // Act
            var namedItem = await App.ExcelService.GetNamedItemAsync(item.Id, namedItemId);
            // Assert
            Assert.AreEqual(namedItemId.ToLower(), namedItem.Name.ToLower(), "Named item id does not match expected");
            Assert.AreEqual("Range", namedItem.Type, "Type of named item is not Range");
            Assert.AreEqual("Sheet3!$A$1:$B$4", namedItem.Value, "Value does not match expected");
            Assert.AreEqual(true, namedItem.Visible, "Visible is not True");
        }

        [TestMethod]
        public async Task NamedItemRange()
        {
            // Arrange
            var item = await TestHelpers.UploadFile();

            // Act
            var range = await App.ExcelService.NamedItemRangeAsync(item.Id, "ChartData");
            // Assert
            Assert.AreEqual(8, range.CellCount, "Cell count is not 8");
            Assert.AreEqual(2, range.ColumnCount, "Column count is not 2");
            Assert.AreEqual(4, range.RowCount, "Row count is not 4");
        }
    }
}
