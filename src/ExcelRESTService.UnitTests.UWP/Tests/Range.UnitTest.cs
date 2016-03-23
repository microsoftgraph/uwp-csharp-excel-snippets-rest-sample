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
    public class RangeTests
    {
        private Microsoft.OneDrive.Item item;

        [TestInitialize]
        public async Task Initialize()
        {
            item = await TestHelpers.UploadFile();
        }

        [TestMethod]
        public async Task GetRange()
        {
            // Arrange
            var values =
                new object[]
                {
                        new object[] { "A", (Double)1 },
                        new object[] { "B", (Double)2 }
                };
            // Act
            var range = await App.ExcelService.GetRangeAsync(item.Id, "Sheet3", "A2:B3");
            // Assert
            Assert.AreEqual(2, range.RowCount, "Row count is not 2");
            Assert.AreEqual(2, range.ColumnCount, "Column count is not 2");

            Assert.AreEqual(((object[])(values[0]))[0], range.Values[0][0], $"A2 value is not {((object[])(values[0]))[0]}");
            Assert.AreEqual(((object[])(values[1]))[0], range.Values[1][0], $"A3 value is not {((object[])(values[1]))[0]}");

            Assert.AreEqual(((object[])(values[0]))[1], range.Values[0][1], $"B2 value is not {((object[])(values[0]))[1]}");
            Assert.AreEqual(((object[])(values[1]))[1], range.Values[1][1], $"B3 value is not {((object[])(values[1]))[1]}");
        }

        [TestMethod]
        public async Task GetRangeWithSelect()
        {
            // Arrange
            var values =
                new object[]
                {
                        new object[] { "A", (Double)1 },
                        new object[] { "B", (Double)2 }
                };
            // Act
            var range = await App.ExcelService.GetRangeAsync(item.Id, "Sheet3", "A2:B3", "", "$select=rowCount, columnCount");
            // Assert
            Assert.AreEqual(2, range.RowCount, "Row count is not 2");
            Assert.AreEqual(2, range.ColumnCount, "Column count is not 2");
            Assert.AreEqual(null, range.Address, "Address is not null");
            Assert.AreEqual(null, range.RowIndex, "Row index is not null");
            Assert.AreEqual(null, range.ColumnIndex, "Column index is not null");
        }

        [TestMethod]
        public async Task UpdateRange()
        {
            // Arrange
            var values =
                new object[]
                {
                        new object[] { "D", (Double)4 },
                        new object[] { "E", (Double)5 },
                        new object[] { "F", (Double)6 }
                };
            // Act
            var range = await App.ExcelService.UpdateRangeAsync(item.Id, "Sheet3", "A2:B4", values);
            // Assert
            Assert.AreEqual(3, range.RowCount, "Row count is not 3");
            Assert.AreEqual(2, range.ColumnCount, "Column count is not 2");

            Assert.AreEqual(((object[])(values[0]))[0], range.Values[0][0], $"A2 value is not {((object[])(values[0]))[0]}");
            Assert.AreEqual(((object[])(values[1]))[0], range.Values[1][0], $"A3 value is not {((object[])(values[1]))[0]}");

            Assert.AreEqual(((object[])(values[0]))[1], range.Values[0][1], $"B2 value is not {((object[])(values[0]))[1]}");
            Assert.AreEqual(((object[])(values[1]))[1], range.Values[1][1], $"B3 value is not {((object[])(values[1]))[1]}");
        }
    }
}
