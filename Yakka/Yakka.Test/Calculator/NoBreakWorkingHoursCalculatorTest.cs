#region GNU General Public License 3
//  Yakka - A system tray application calculating and displaying your working hours
//  Copyright (C) 2017  Tobias Koch
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program. If not, see <http://www.gnu.org/licenses/>.
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
    /// Contains the unit tests of the <see cref="NoBreakWorkingHoursCalculator"/> class.
    /// </summary>
    [TestFixture]
    public class NoBreakWorkingHoursCalculatorTest
    {
        #region Methods

        /// <summary>
        /// Tests getting the description with an invalid argument.
        /// </summary>
        [Test]
        public void GetDescriptionArgumentNullTest()
        {
            NoBreakWorkingHoursCalculator calculator = new NoBreakWorkingHoursCalculator();
            Assert.Throws<ArgumentNullException>(() => calculator.GetDescription(null));
        }

        /// <summary>
        /// Tests getting the description.
        /// </summary>
        [Test]
        public void GetDescriptionArgument()
        {
            NoBreakWorkingHoursCalculator calculator = new NoBreakWorkingHoursCalculator();
            Assert.That(!string.IsNullOrEmpty(calculator.GetDescription(CultureInfo.CurrentCulture)));
        }

        /// <summary>
        /// Tests the working hours calculation.
        /// </summary>
        [Test]
        public void CalculateWorkingHoursTest()
        {
            NoBreakWorkingHoursCalculator calculator = new NoBreakWorkingHoursCalculator();
            DateTime start = DateTime.Now;
            DateTime end = start.AddHours(4);

            TimeSpan workingHours = calculator.CalculateWorkingHours(start, end);

            Assert.That(workingHours == TimeSpan.FromHours(4), "calculation incorrect");
        }

        /// <summary>
        /// Tests the working hours calculation with invalid arguments.
        /// </summary>
        [Test]
        public void CalculateWorkingHoursInvalidArgumentsTest()
        {
            NoBreakWorkingHoursCalculator calculator = new NoBreakWorkingHoursCalculator();
            DateTime start = DateTime.Now;
            DateTime end = start.AddHours(4);

            Assert.Throws<InvalidOperationException>(() => calculator.CalculateWorkingHours(end, start)); // start, end
        }

        /// <summary>
        /// Tests the break calculation.
        /// </summary>
        [Test]
        public void CalculateBreakTest()
        {
            NoBreakWorkingHoursCalculator calculator = new NoBreakWorkingHoursCalculator();
            DateTime start = DateTime.Now;
            DateTime end = start.AddHours(8);

            TimeSpan br = calculator.CalculateBreak(start, end);

            Assert.That(br == new TimeSpan(0), "calculation incorrect");
        }

        #endregion
    }
}
