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
    public class ApplicationTests
    {
        [TestMethod]
        public async Task Calculate()
        {
            // Arrange
            var item = await TestHelpers.UploadFile();
            
            // Act
            await App.ExcelService.CalculateAsync(item.Id);
            // Assert
        }
    }
}
