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
using Moq;
using Yakka.Forms;

namespace Yakka.Tests.Forms
{
    /// <summary>
    /// Contains unit tests of the <see cref="DetailPresenter"/> class.
    /// </summary>
    [TestClass]
    public class DetailPresenterTests
    {
        /// <summary>
        /// Tests the <see cref="DetailPresenter.WorkingHoursCalculation"/> property.
        /// </summary>
        [TestMethod]
        public void ConfigurationTest()
        {
            WorkingHoursCalculation? result = null;

            var viewMock = new Mock<IDetailView>();
            viewMock.SetupSet(m => m.WorkingHoursCalculation = It.IsAny<WorkingHoursCalculation>()).Callback<WorkingHoursCalculation>(c => { result = c; });

            var calculation = new WorkingHoursCalculation
            {
                CalculatedWorkingHours = TimeSpan.FromHours(6),
                CalculatedBreak = TimeSpan.FromHours(1),
            };

            var presenter = new DetailPresenter(viewMock.Object);
            presenter.WorkingHoursCalculation = calculation;

            Assert.IsNotNull(result);
            Assert.AreEqual(calculation.CalculatedWorkingHours, result?.CalculatedWorkingHours);
            Assert.AreEqual(calculation.CalculatedBreak, result?.CalculatedBreak);
        }
    }
}
