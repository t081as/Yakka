#region GNU General Public License 3
// Yakka - A system tray application calculating and displaying your working hours
// Copyright (C) 2017  Tobias Koch
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
using NUnit.Framework;
using Yakka.Forms;
#endregion

namespace Yakka.Test.Forms
{
    /// <summary>
    /// Contains the unit tests of the <see cref="SystemTrayIconPresenter"/> class.
    /// </summary>
    [TestFixture]
    public class SystemTrayIconPresenterTest
    {
        #region Constants and Fields

        /// <summary>
        /// Contains the test stub representing the view.
        /// </summary>
        private SystemTrayIconViewStub viewStub = null;

        /// <summary>
        /// Indicates whether the <see cref="SystemTrayIconViewStub.Info"/> event has been invoked.
        /// </summary>
        private volatile bool infoInvoked = false;

        /// <summary>
        /// Indicates whether the <see cref="SystemTrayIconViewStub.Configure"/> event has been invoked.
        /// </summary>
        private volatile bool configureInvoked = false;

        /// <summary>
        /// Indicates whether the <see cref="SystemTrayIconViewStub.Quit"/> event has been invoked.
        /// </summary>
        private volatile bool quitInvoked = false;

        #endregion

        #region Methods

        #region Setup / Teardown

        /// <summary>
        /// Gets called before a test starts.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            if (this.infoInvoked)
            {
                this.infoInvoked = false;
            }

            if (this.configureInvoked)
            {
                this.configureInvoked = false;
            }

            if (this.quitInvoked)
            {
                this.quitInvoked = false;
            }

            this.viewStub = new SystemTrayIconViewStub();
            this.viewStub.Configure += this.ViewStub_Configure;
            this.viewStub.Info += this.ViewStub_Info;
            this.viewStub.Quit += this.ViewStub_Quit;
        }

        /// <summary>
        /// Gets called after a test ends.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            this.viewStub.Configure -= this.ViewStub_Configure;
            this.viewStub.Info -= this.ViewStub_Info;
            this.viewStub.Quit -= this.ViewStub_Quit;
            this.viewStub.Dispose();
            this.viewStub = null;
        }

        #endregion

        #region Tests

        /// <summary>
        /// Tests the presenter initialization with an invalid argument.
        /// </summary>
        [Test]
        public void ConstructorViewNullTest()
        {
            Assert.Throws<ArgumentNullException>(() => new SystemTrayIconPresenter(null, new WorkingHoursCalculation()));
        }

        /// <summary>
        /// Tests the presenter initialization with an invalid argument.
        /// </summary>
        [Test]
        public void ConstructorCalculatorNullTest()
        {
            Assert.Throws<ArgumentNullException>(() => new SystemTrayIconPresenter(this.viewStub, null));
        }

        #endregion

        /// <summary>
        /// Handles the <see cref="SystemTrayIconViewStub.Quit"/> event.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> that contains the data.</param>
        private void ViewStub_Quit(object sender, EventArgs e)
        {
            this.quitInvoked = true;
        }

        /// <summary>
        /// Handles the <see cref="SystemTrayIconViewStub.Info"/> event.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> that contains the data.</param>
        private void ViewStub_Info(object sender, EventArgs e)
        {
            this.infoInvoked = true;
        }

        /// <summary>
        /// Handles the <see cref="SystemTrayIconViewStub.Configure"/> event.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> that contains the data.</param>
        private void ViewStub_Configure(object sender, EventArgs e)
        {
            this.configureInvoked = true;
        }

        #endregion
    }
}
