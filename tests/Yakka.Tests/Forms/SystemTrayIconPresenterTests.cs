﻿// Yakka - A system tray application calculating and displaying your working hours
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
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Yakka.Calculation;
using Yakka.Forms;

namespace Yakka.Tests.Forms
{
    /// <summary>
    /// Contains unit tests of the <see cref="SystemTrayIconPresenter"/> class.
    /// </summary>
    [TestClass]
    public class SystemTrayIconPresenterTests
    {
        /// <summary>
        /// Tests the <see cref="SystemTrayIconPresenter.Details"/> event.
        /// </summary>
        [TestMethod]
        public void DetailsTest()
        {
            var viewMock = new Mock<ISystemTrayIconView>();
            var detailsInvoked = false;

            EventHandler<WorkingHoursCalculationEventArgs> handler = (sender, e) =>
            {
                detailsInvoked = true;
            };

            using (var presenter = new SystemTrayIconPresenter(viewMock.Object, new WorkingHoursConfiguration(), new DefaultCalculator()))
            {
                presenter.Details += handler;

                presenter.Show();
                viewMock.Raise(m => m.Details += null, EventArgs.Empty);
                presenter.Hide();
            }

            Assert.IsTrue(detailsInvoked);
        }

        /// <summary>
        /// Tests the <see cref="SystemTrayIconPresenter.Configure"/> event.
        /// </summary>
        [TestMethod]
        public void ConfigureTest()
        {
            var viewMock = new Mock<ISystemTrayIconView>();
            var configureInvoked = false;

            EventHandler handler = (sender, e) =>
            {
                configureInvoked = true;
            };

            using (var presenter = new SystemTrayIconPresenter(viewMock.Object, new WorkingHoursConfiguration(), new DefaultCalculator()))
            {
                presenter.Configure += handler;

                presenter.Show();
                viewMock.Raise(m => m.Configure += null, EventArgs.Empty);
                presenter.Hide();
            }

            Assert.IsTrue(configureInvoked);
        }

        /// <summary>
        /// Tests the <see cref="SystemTrayIconPresenter.Info"/> event.
        /// </summary>
        [TestMethod]
        public void InfoTest()
        {
            var viewMock = new Mock<ISystemTrayIconView>();
            var infoInvoked = false;

            EventHandler handler = (sender, e) =>
            {
                infoInvoked = true;
            };

            using (var presenter = new SystemTrayIconPresenter(viewMock.Object, new WorkingHoursConfiguration(), new DefaultCalculator()))
            {
                presenter.Info += handler;

                presenter.Show();
                viewMock.Raise(m => m.Info += null, EventArgs.Empty);
                presenter.Hide();
            }

            Assert.IsTrue(infoInvoked);
        }

        /// <summary>
        /// Tests the <see cref="SystemTrayIconPresenter.Quit"/> event.
        /// </summary>
        [TestMethod]
        public void QuitTest()
        {
            var viewMock = new Mock<ISystemTrayIconView>();
            var quitInvoked = false;

            EventHandler handler = (sender, e) =>
            {
                quitInvoked = true;
            };

            using (var presenter = new SystemTrayIconPresenter(viewMock.Object, new WorkingHoursConfiguration(), new DefaultCalculator()))
            {
                presenter.Quit += handler;

                presenter.Show();
                viewMock.Raise(m => m.Quit += null, EventArgs.Empty);
                presenter.Hide();
            }

            Assert.IsTrue(quitInvoked);
        }

        /// <summary>
        /// Tests the <see cref="SystemTrayIconPresenter.Configuration"/> property.
        /// </summary>
        [TestMethod]
        public void ConfigurationTest()
        {
            WorkingHoursCalculation? result = null;
            var viewMock = new Mock<ISystemTrayIconView>();
            viewMock.SetupSet(m => m.WorkingHoursCalculation = It.IsAny<WorkingHoursCalculation>()).Callback<WorkingHoursCalculation>(c => { result = c; });

            var configuration = new WorkingHoursConfiguration
            {
                StartTime = DateTime.Now.Subtract(TimeSpan.FromHours(1)),
                WorkingHoursCalculator = new NoBreakWorkingHoursCalculator(),
                BreakMode = BreakMode.Manual,
                ManualBreakTime = TimeSpan.Zero,
            };

            using (var presenter = new SystemTrayIconPresenter(viewMock.Object, configuration, new DefaultCalculator()))
            {
                presenter.Show();
                presenter.Configuration = configuration;
                Assert.AreEqual(configuration.ManualBreakTime, presenter.Configuration.ManualBreakTime);

                Thread.Sleep(2000);

                presenter.Hide();
            }

            Assert.IsNotNull(result);
            Assert.IsTrue(result?.CalculatedWorkingHours.Hours >= 1);
        }

        /// <summary>
        /// Tests if the presenter forwards warnings to the user interface.
        /// If <c>the same</c> warning is triggered twice in a short time interval it shall not be displayed again.
        /// </summary>
        [TestMethod]
        public void WarningSameTest()
        {
            uint messageDisplayCount = 0;
            var viewMock = new Mock<ISystemTrayIconView>();
            viewMock.Setup(m => m.ShowWarning(It.IsAny<string>())).Callback<string>(s =>
            {
                messageDisplayCount++;
            });

            var calculatorMock = new Mock<ICalculator>();
            calculatorMock.Setup(m => m.Calculate(It.IsAny<WorkingHoursConfiguration>(), It.IsAny<DateTime>())).Returns(new WorkingHoursCalculation
            {
                CalculatedWorkingHours = TimeSpan.FromHours(1),
                CalculatedBreak = TimeSpan.FromHours(1),
                Configuration = new WorkingHoursConfiguration(),
                Warning = "Test warning",
            });

            var configuration = new WorkingHoursConfiguration();

            using (var presenter = new SystemTrayIconPresenter(viewMock.Object, configuration, calculatorMock.Object))
            {
                Assert.AreEqual(0u, messageDisplayCount);
                presenter.Show();
                presenter.Configuration = configuration;
                Thread.Sleep(1000);
                Assert.AreEqual(1u, messageDisplayCount);
                presenter.Configuration = configuration; // Force re-calculation and same warning
                Thread.Sleep(1000);
                Assert.AreEqual(1u, messageDisplayCount);
                presenter.Hide();
            }
        }
    }
}
