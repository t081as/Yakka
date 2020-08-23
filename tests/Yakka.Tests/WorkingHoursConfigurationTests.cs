// Yakka - A system tray application calculating and displaying your working hours
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
using Yakka.Calculation;

namespace Yakka.Tests
{
    /// <summary>
    /// Contains unit tests of the <see cref="WorkingHoursConfiguration"/> class.
    /// </summary>
    [TestClass]
    public class WorkingHoursConfigurationTests
    {
        /// <summary>
        /// Tests the <see cref="WorkingHoursConfiguration.Save(WorkingHoursConfiguration)"/>
        /// and <see cref="WorkingHoursConfiguration.Load"/> methods.
        /// </summary>
        [TestMethod]
        public void SaveLoadTest()
        {
            var configuration = new WorkingHoursConfiguration
            {
                StartTime = DateTime.Now,
                WorkingHoursCalculator = new NoBreakWorkingHoursCalculator(),
                ManualBreakTime = TimeSpan.FromMinutes(20),
                BreakMode = BreakMode.Manual,
            };

            WorkingHoursConfiguration.Save(configuration);
            var result = WorkingHoursConfiguration.Load();

            Assert.AreEqual(configuration.ManualBreakTime, result.ManualBreakTime);
            Assert.AreEqual(configuration.BreakMode, result.BreakMode);
            Assert.AreEqual(configuration.StartTime, result.StartTime);
            Assert.AreEqual(configuration.WorkingHoursCalculator.Id, result.WorkingHoursCalculator.Id);
        }
    }
}
