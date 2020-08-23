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
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yakka.Calculation;

namespace Yakka.Tests.Calculation
{
    /// <summary>
    /// Contains unit tests of the <see cref="WorkingHoursCalculators"/> class.
    /// </summary>
    [TestClass]
    public class WorkingHoursCalculatorsTests
    {
        /// <summary>
        /// Tests the detection of calculators.
        /// </summary>
        [TestMethod]
        public void FindCalculatorsTest()
        {
            Assert.AreEqual(true, WorkingHoursCalculators.All.Any());
        }

        /// <summary>
        /// Tests the availability of a default calculator.
        /// </summary>
        [TestMethod]
        public void DefaultCalculatorTest()
        {
            Assert.IsNotNull(WorkingHoursCalculators.Default);
        }
    }
}
