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
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.DataCollection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Yakka.Calculation;
using Yakka.Forms;

namespace Yakka.Tests.Forms
{
    /// <summary>
    /// Contains unit tests of the <see cref="ConfigurationPresenter"/> class.
    /// </summary>
    [TestClass]
    public class ConfigurationPresenterTest
    {
        /// <summary>
        /// Tests if a changed configuration is detected correctly.
        /// </summary>
        [TestMethod]
        public void ConfigurationChangedTest()
        {
            WorkingHoursConfiguration? receivedConfiguration = null;

            var viewMock = new Mock<IConfigurationView>();
            viewMock.SetupGet(m => m.StartTime).Returns(DateTime.Now);
            viewMock.SetupGet(m => m.BreakMode).Returns(BreakMode.Automatic);
            viewMock.SetupGet(m => m.WorkingHoursCalculator).Returns(new NoBreakWorkingHoursCalculator());
            viewMock.SetupGet(m => m.ManualBreakTime).Returns(TimeSpan.FromMinutes(20));

            var testConfiguration = new WorkingHoursConfiguration
            {
                StartTime = DateTime.Now,
                BreakMode = BreakMode.Manual,
                WorkingHoursCalculator = new NoBreakWorkingHoursCalculator(),
                ManualBreakTime = TimeSpan.FromMinutes(15),
            };

            EventHandler<WorkingHoursConfigurationEventArgs> handler = (sender, args) =>
            {
                receivedConfiguration = args.Configuration;
            };

            var presenter = new ConfigurationPresenter(viewMock.Object, testConfiguration);
            presenter.ConfigurationChanged += handler;

            viewMock.Raise(m => m.Changed += null, EventArgs.Empty);

            Assert.IsNotNull(receivedConfiguration);
            Assert.AreEqual(TimeSpan.FromMinutes(20), receivedConfiguration?.ManualBreakTime);
            Assert.AreEqual(BreakMode.Automatic, receivedConfiguration?.BreakMode);
        }
    }
}
