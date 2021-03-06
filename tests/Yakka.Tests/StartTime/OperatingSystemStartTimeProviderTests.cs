﻿// Yakka - A system tray application calculating and displaying your working hours
// Copyright (C) 2017-2020  Tobias Koch
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program. If not, see <http://www.gnu.org/licenses/>.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yakka.StartTime;

namespace Yakka.Tests.StartTime
{
    /// <summary>
    /// Contains unit tests of the <see cref="OperatingSystemStartTimeProvider"/> class.
    /// </summary>
    [TestClass]
    public class OperatingSystemStartTimeProviderTests
    {
        /// <summary>
        /// Checks the <see cref="OperatingSystemStartTimeProvider.StartTime"/> property.
        /// </summary>
        [TestMethod]
        public void StartTimeTest()
        {
            var provider = new OperatingSystemStartTimeProvider();
            Assert.IsTrue((DateTime.Now - provider.StartTime)?.TotalSeconds > 10);
        }
    }
}
