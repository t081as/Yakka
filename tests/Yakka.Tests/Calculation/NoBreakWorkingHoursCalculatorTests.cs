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

namespace Yakka.Tests.Calculation
{
    /// <summary>
    /// Contains unit tests of the <see cref="NoBreakWorkingHoursCalculator"/> class.
    /// </summary>
    [TestClass]
    public class NoBreakWorkingHoursCalculatorTests
    {
        /// <summary>
        /// Tests the required metadata.
        /// </summary>
        [TestMethod]
        public void MetadataTest()
        {
            var calulator = new NoBreakWorkingHoursCalculator();

            Assert.IsFalse(string.IsNullOrWhiteSpace(calulator.Title));
            Assert.IsFalse(string.IsNullOrWhiteSpace(calulator.Description));
        }

        /// <summary>
        /// Checks the calculation.
        /// </summary>
        [TestMethod]
        public void CalculateTest()
        {
            var calulator = new NoBreakWorkingHoursCalculator();
            var end = DateTime.Now;
            var start = end - TimeSpan.FromHours(7);

            var result = calulator.Calculate(start, end);

            Assert.AreEqual(0, result.breakTimeSpan.TotalMinutes);
            Assert.AreEqual(7, result.workTimeSpan.TotalHours);
        }

        /// <summary>
        /// Checks the calculation with invalid arguments.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CalculateWrongArgumentsTest()
        {
            var calulator = new NoBreakWorkingHoursCalculator();
            var start = DateTime.Now.AddHours(5);
            var end = DateTime.Now;

            var result = calulator.Calculate(start, end);
        }
    }
}
