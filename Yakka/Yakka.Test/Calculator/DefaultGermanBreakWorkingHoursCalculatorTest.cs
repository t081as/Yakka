#region GNU General Public License 3
// Yakka - A system tray application calculating and displaying your working hours
// Copyright (C) -2018  Tobias Koch
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
#endregion

#region Namespaces
using System;
using System.Globalization;
using NUnit.Framework;
using Yakka.Calculator;
#endregion

namespace Yakka.Test.Calculator
{
    /// <summary>
    /// Contains the unit tests of the <see cref="DefaultGermanBreakWorkingHoursCalculator"/> class.
    /// </summary>
    [TestFixture]
    public class DefaultGermanBreakWorkingHoursCalculatorTest
    {
        #region Methods

        /// <summary>
        /// Tests getting the description with an invalid argument.
        /// </summary>
        [Test]
        public void GetDescriptionArgumentNullTest()
        {
            DefaultGermanBreakWorkingHoursCalculator calculator = new DefaultGermanBreakWorkingHoursCalculator();
            Assert.Throws<ArgumentNullException>(() => calculator.GetDescription(null));
        }

        /// <summary>
        /// Tests getting the description.
        /// </summary>
        [Test]
        public void GetDescriptionArgument()
        {
            DefaultGermanBreakWorkingHoursCalculator calculator = new DefaultGermanBreakWorkingHoursCalculator();

            Assert.That(!string.IsNullOrEmpty(calculator.GetDescription(CultureInfo.CurrentCulture)));
            Assert.That(calculator.GetDescription(CultureInfo.CurrentCulture) == calculator.ToString());
        }

        /// <summary>
        /// Tests the working hours calculation.
        /// </summary>
        [Test]
        public void CalculateNoBreakTest()
        {
            DefaultGermanBreakWorkingHoursCalculator calculator = new DefaultGermanBreakWorkingHoursCalculator();
            DateTime start = DateTime.Now;
            DateTime end = start.AddHours(5).AddMinutes(59);

            Assert.That(calculator.CalculateWorkingHours(start, end) == TimeSpan.FromHours(5) + TimeSpan.FromMinutes(59), "calculation (working hours) incorrect");
            Assert.That(calculator.CalculateBreak(start, end) == TimeSpan.FromMinutes(0), "calculation (break) incorrect");
        }

        /// <summary>
        /// Tests the working hours calculation.
        /// </summary>
        [Test]
        public void CalculateShortBreakTest()
        {
            DefaultGermanBreakWorkingHoursCalculator calculator = new DefaultGermanBreakWorkingHoursCalculator();
            DateTime start = DateTime.Now;
            DateTime end = start.AddHours(6).AddMinutes(31);

            Assert.That(calculator.CalculateWorkingHours(start, end) == TimeSpan.FromHours(6) + TimeSpan.FromMinutes(1), "calculation (working hours) incorrect");
            Assert.That(calculator.CalculateBreak(start, end) == TimeSpan.FromMinutes(30), "calculation (break) incorrect");
        }

        /// <summary>
        /// Tests the working hours calculation.
        /// </summary>
        [Test]
        public void CalculateLongBreakTest()
        {
            DefaultGermanBreakWorkingHoursCalculator calculator = new DefaultGermanBreakWorkingHoursCalculator();
            DateTime start = DateTime.Now;
            DateTime end = start.AddHours(9).AddMinutes(50);

            Assert.That(calculator.CalculateWorkingHours(start, end) == TimeSpan.FromHours(9) + TimeSpan.FromMinutes(5), "calculation (working hours) incorrect");
            Assert.That(calculator.CalculateBreak(start, end) == TimeSpan.FromMinutes(45), "calculation (break) incorrect");
        }

        /// <summary>
        /// Tests the working hours calculation.
        /// </summary>
        [Test]
        public void CalculateBreakTest()
        {
            DefaultGermanBreakWorkingHoursCalculator calculator = new DefaultGermanBreakWorkingHoursCalculator();
            DateTime start = DateTime.Now;
            DateTime end1 = start.AddHours(6).AddMinutes(28);
            DateTime end2 = start.AddHours(9).AddMinutes(42);

            Assert.That(calculator.CalculateBreak(start, end1) == TimeSpan.FromMinutes(28), "calculation (break) incorrect");
            Assert.That(calculator.CalculateBreak(start, end2) == TimeSpan.FromMinutes(42), "calculation (break) incorrect");
        }

        /// <summary>
        /// Tests the working hours calculation with invalid arguments.
        /// </summary>
        [Test]
        public void CalculateInvalidArgumentsTest()
        {
            DefaultGermanBreakWorkingHoursCalculator calculator = new DefaultGermanBreakWorkingHoursCalculator();
            DateTime start = DateTime.Now;
            DateTime end = start.AddHours(4);

            Assert.Throws<InvalidOperationException>(() => calculator.CalculateWorkingHours(end, start)); // start, end
            Assert.Throws<InvalidOperationException>(() => calculator.CalculateBreak(end, start)); // start, end
        }

        #endregion
    }
}
