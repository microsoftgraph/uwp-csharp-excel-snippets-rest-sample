/*
 *  Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license.
 *  See LICENSE in the source repository root for complete license information.
 */

using System;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace ExcelRESTService.UnitTests.UWP
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public async Task GetUser()
        {
            // Arrange
            // Act
            var User = await App.UserService.GetUserAsync();
            // Assert
            Assert.AreNotEqual(string.Empty, User.Id, "User Id is blank");
            Assert.AreNotEqual(string.Empty, User.DisplayName, "DisplayName is blank");
        }
    }
}
