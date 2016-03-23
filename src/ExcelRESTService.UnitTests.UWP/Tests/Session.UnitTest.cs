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
    public class SessionTests
    {
        [TestMethod]
        public async Task CreateSession()
        {
            // Arrange
            var item = await TestHelpers.UploadFile();
            
            // Act
            var sessionInfo = await App.ExcelService.CreateSessionAsync(item.Id);
            // Assert
            Assert.AreNotEqual(string.Empty, sessionInfo.Id, "Session id is blank");
        }

        [TestMethod]
        public async Task CloseSession()
        {
            // Arrange
            var item = await TestHelpers.UploadFile();
            var sessionInfo = await App.ExcelService.CreateSessionAsync(item.Id);
            // Act
            await App.ExcelService.CloseSessionAsync(item.Id, sessionInfo.Id);
            // Assert
        }
    }
}
