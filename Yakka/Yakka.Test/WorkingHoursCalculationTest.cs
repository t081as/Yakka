﻿#region GNU General Public License 3
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
using NUnit.Framework;
#endregion

namespace Yakka.Test
{
    /// <summary>
    /// Contains the unit tests of the <see cref="WorkingHoursCalculation"/> class.
    /// </summary>
    [TestFixture]
    public class WorkingHoursCalculationTest
    {
        #region Constants and Fields

        /// <summary>
        /// Represents the date / time used as start of working hours (27.11.1984 08:25:00.00).
        /// </summary>
        private DateTime testStartTime = new DateTime(1984, 11, 27, 8, 25, 0, 0);

        #endregion

        #region Methods

        /// <summary>
        /// Tests the calculation with no break (see <see cref="WorkingHoursCalculatorStub"/>).
        /// </summary>
        [Test]
        public void CalculateWorkingHoursNoBreak()
        {
            WorkingHoursCalculation calculation = new WorkingHoursCalculation();
            DateTime end = new DateTime(1984, 11, 27, 12, 24, 0, 0);

            WorkingHours result = calculation.Calculate(
                new WorkingHoursCalculatorStub(),
                this.testStartTime,
                end);

            Assert.That(result.Start == this.testStartTime, "start time incorrect");
            Assert.That(result.CalculatedBreak == new TimeSpan(0), "calculation incorrect");
            Assert.That(result.CalculatedWorkingHours == new TimeSpan(3, 59, 0), "calculation incorrect");
        }

        #endregion
    }
}