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

            using (var presenter = new SystemTrayIconPresenter(viewMock.Object, new WorkingHoursConfiguration()))
            {
                presenter.Configure += handler;

                presenter.Show();
                viewMock.Raise(m => m.Configure += null, EventArgs.Empty);
                presenter.Hide();
            }

            Assert.IsTrue(configureInvoked);
        }
    }
}
