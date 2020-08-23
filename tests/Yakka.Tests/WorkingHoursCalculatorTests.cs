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
    /// Contains unit tests of the <see cref="WorkingHoursCalculator"/> class.
    /// </summary>
    [TestClass]
    public class WorkingHoursCalculatorTests
    {
        /// <summary>
        /// Tests the <see cref="WorkingHoursCalculator.Calculate(WorkingHoursConfiguration, DateTime)"/> method.
        /// </summary>
        [TestMethod]
        public void CalculateTest()
        {
            var startTime = new DateTime(2020, 8, 22, 8, 0, 0);
            var nowTime = new DateTime(2020, 8, 22, 15, 0, 0);

            var configuration1 = new WorkingHoursConfiguration
            {
                StartTime = startTime,
                BreakMode = BreakMode.Automatic,
                WorkingHoursCalculator = new NoBreakWorkingHoursCalculator(),
                ManualBreakTime = TimeSpan.Zero,
            };

            var calculation1 = WorkingHoursCalculator.Calculate(configuration1, nowTime);
            Assert.AreEqual(7, calculation1.CalculatedWorkingHours.Hours);

            var configuration2 = new WorkingHoursConfiguration
            {
                StartTime = startTime,
                BreakMode = BreakMode.Manual,
                WorkingHoursCalculator = new NoBreakWorkingHoursCalculator(),
                ManualBreakTime = TimeSpan.FromMinutes(15),
            };

            var calculation2 = WorkingHoursCalculator.Calculate(configuration2, nowTime);
            Assert.AreEqual(6, calculation2.CalculatedWorkingHours.Hours);
            Assert.AreEqual(45, calculation2.CalculatedWorkingHours.Minutes);
            Assert.AreEqual(15, calculation2.CalculatedBreak.Minutes);
        }
    }
}
