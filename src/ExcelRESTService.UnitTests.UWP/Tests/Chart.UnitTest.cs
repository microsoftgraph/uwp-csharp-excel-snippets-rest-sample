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
    public class ChartTests
    {
        private Microsoft.OneDrive.Item item;

        [TestInitialize]
        public async Task Initialize()
        {
            item = await TestHelpers.UploadFile();
        }

        [TestMethod]
        public async Task ListCharts()
        {
            // Arrange
            // Act
            var charts = await App.ExcelService.ListChartsAsync(item.Id, "Sheet1");
            // Assert
            Assert.AreEqual(1, charts.Length, "Chart count is not 1");
        }

        [TestMethod]
        public async Task ListCharts_SheetWithNoCharts_ReturnEmptyList()
        {
            // Arrange
            // Act
            var charts = await App.ExcelService.ListChartsAsync(item.Id, "Sheet2");
            // Assert
            Assert.AreEqual(0, charts.Length, "Chart count is not 0");
        }

        [TestMethod]
        public async Task AddChart()
        {
            // Arrange
            // Act
            var chart = await App.ExcelService.AddChartAsync(item.Id, "Sheet3", "ColumnClustered", "Sheet3!A1:B4");
            // Assert
            Assert.AreEqual("Chart 2", chart.Name, "Chart name is not 'Chart 2'");
        }

        [TestMethod]
        public async Task GetChart()
        {
            // Arrange
            // Act
            var chart = await App.ExcelService.GetChartAsync(item.Id, "Sheet1", "Chart 1");
            // Assert
            Assert.AreEqual("Chart 1", chart.Name, "Chart name is not 'Chart 1'");
            Assert.AreEqual(470.25, chart.Width, "Chart width is not 470.25");
            Assert.AreEqual(258.0, chart.Height, "Chart height is not 258");
        }

        [TestMethod]
        public async Task GetChartTitle()
        {
            // Arrange
            var title = "Distance per day";
            // Act
            var chartTitle = await App.ExcelService.GetChartTitleAsync(item.Id, "Sheet1", "Chart 1");
            // Assert
            Assert.AreEqual(title, chartTitle.Text, $"Chart title is not '{title}'");
        }

        [TestMethod]
        public async Task GetChartImage()
        {
            // Arrange
            // Act
            var imageAsString = await App.ExcelService.GetChartImageAsync(item.Id, "Sheet1", "Chart 1");
            // Assert
            Assert.AreNotEqual(null, imageAsString, "Image string is null");
            Assert.AreNotEqual(0, imageAsString.Length, "Image string length is 0");
        }
    }
}
