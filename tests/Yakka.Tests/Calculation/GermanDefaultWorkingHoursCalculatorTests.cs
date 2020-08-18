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
    /// Contains unit tests of the <see cref="GermanDefaultWorkingHoursCalculator"/> class.
    /// </summary>
    [TestClass]
    public class GermanDefaultWorkingHoursCalculatorTests
    {
        /// <summary>
        /// Checks the calculation.
        /// </summary>
        [TestMethod]
        public void CalculateTest()
        {
            var calulator = new GermanDefaultWorkingHoursCalculator();

            DateTime start;
            DateTime end;
            (TimeSpan workTimeSpan, TimeSpan breakTimeSpan) result;

            // 6h, 0m
            end = DateTime.Now;
            start = end - TimeSpan.FromHours(6);
            result = calulator.Calculate(start, end);

            Assert.AreEqual(0, result.breakTimeSpan.TotalMinutes);
            Assert.AreEqual(6, result.workTimeSpan.TotalHours);

            // 6h, 30m
            end = DateTime.Now;
            start = end - TimeSpan.FromHours(6) - TimeSpan.FromMinutes(30);
            result = calulator.Calculate(start, end);

            Assert.AreEqual(30, result.breakTimeSpan.TotalMinutes);
            Assert.AreEqual(6, result.workTimeSpan.TotalHours);

            // 8h, 30m
            end = DateTime.Now;
            start = end - TimeSpan.FromHours(8) - TimeSpan.FromMinutes(30);
            result = calulator.Calculate(start, end);

            Assert.AreEqual(30, result.breakTimeSpan.TotalMinutes);
            Assert.AreEqual(8, result.workTimeSpan.TotalHours);

            // 9h, 45m
            end = DateTime.Now;
            start = end - TimeSpan.FromHours(9) - TimeSpan.FromMinutes(45);
            result = calulator.Calculate(start, end);

            Assert.AreEqual(45, result.breakTimeSpan.TotalMinutes);
            Assert.AreEqual(9, result.workTimeSpan.TotalHours);
        }

        /// <summary>
        /// Checks the calculation with invalid arguments.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CalculateWrongArgumentsTest()
        {
            var calulator = new GermanDefaultWorkingHoursCalculator();
            var start = DateTime.Now.AddHours(5);
            var end = DateTime.Now;

            var result = calulator.Calculate(start, end);
        }
    }
}
