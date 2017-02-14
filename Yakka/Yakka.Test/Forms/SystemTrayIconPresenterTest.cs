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
using System.Threading;
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
        /// Represents the subject under test.
        /// </summary>
        private SystemTrayIconPresenter presenter = null;

        /// <summary>
        /// Indicates whether the <see cref="SystemTrayIconPresenter.Info"/> event has been invoked.
        /// </summary>
        private volatile bool infoInvoked = false;

        /// <summary>
        /// Indicates whether the <see cref="SystemTrayIconPresenter.Configure"/> event has been invoked.
        /// </summary>
        private volatile bool configureInvoked = false;

        /// <summary>
        /// Indicates whether the <see cref="SystemTrayIconPresenter.Quit"/> event has been invoked.
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

            this.presenter = new SystemTrayIconPresenter(this.viewStub, new WorkingHoursCalculation());
            this.presenter.Configure += this.Configure;
            this.presenter.Info += this.Info;
            this.presenter.Quit += this.Quit;
        }

        /// <summary>
        /// Gets called after a test ends.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            this.presenter.Configure -= this.Configure;
            this.presenter.Info -= this.Info;
            this.presenter.Quit -= this.Quit;
            this.presenter = null;

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

        /// <summary>
        /// Tests the <see cref="SystemTrayIconPresenter.Show"/> method.
        /// </summary>
        [Test]
        public void ShowTest()
        {
            this.presenter.Show();
            Assert.That(this.viewStub.Visible == true, "View not visible");
        }

        /// <summary>
        /// Tests the <see cref="SystemTrayIconPresenter.Show"/> method.
        /// </summary>
        [Test]
        public void InvalidShowTest()
        {
            this.presenter.Show();
            Assert.Throws<InvalidOperationException>(() => this.presenter.Show());
        }

        /// <summary>
        /// Tests the <see cref="SystemTrayIconPresenter.Hide"/> method.
        /// </summary>
        [Test]
        public void HideTest()
        {
            this.ShowTest();
            this.presenter.Hide();
            Assert.That(this.viewStub.Visible == false, "View still visible");
        }

        /// <summary>
        /// Tests the <see cref="SystemTrayIconPresenter.Hide"/> method.
        /// </summary>
        [Test]
        public void InvalidHideTest()
        {
            Assert.Throws<InvalidOperationException>(() => this.presenter.Hide());
        }

        /// <summary>
        /// Tests the <see cref="SystemTrayIconPresenter.Configure"/> event.
        /// </summary>
        [Test]
        public void ConfigureEventTest()
        {
            this.presenter.Show();
            this.viewStub.OnConfigure();
            Thread.Sleep(100);
            this.presenter.Hide();

            Assert.That(this.configureInvoked == true, "Event not invoked");
        }

        /// <summary>
        /// Tests the <see cref="SystemTrayIconPresenter.Info"/> event.
        /// </summary>
        [Test]
        public void InfoEventTest()
        {
            this.presenter.Show();
            this.viewStub.OnInfo();
            Thread.Sleep(100);
            this.presenter.Hide();

            Assert.That(this.infoInvoked == true, "Event not invoked");
        }

        /// <summary>
        /// Tests the <see cref="SystemTrayIconPresenter.Quit"/> event.
        /// </summary>
        [Test]
        public void QuitEventTest()
        {
            this.presenter.Show();
            this.viewStub.OnQuit();
            Thread.Sleep(100);
            this.presenter.Hide();

            Assert.That(this.quitInvoked == true, "Event not invoked");
        }

        /// <summary>
        /// Tests the <see cref="SystemTrayIconPresenter.Configuration"/> property.
        /// </summary>
        [Test]
        public void UserConfigurationTest()
        {
            UserConfiguration configuration = new UserConfiguration();
            configuration.Start = DateTime.Now.Subtract(TimeSpan.FromSeconds(1));
            configuration.Calculator = new WorkingHoursCalculatorStub();

            this.presenter.Show();
            this.presenter.Configuration = configuration;
            Thread.Sleep(100);
            WorkingHours workingHours = this.viewStub.WorkingHours;
            this.presenter.Hide();

            Assert.That(workingHours.Start == configuration.Start, "Start invalid");
            Assert.That(workingHours.CalculatedWorkingHours >= TimeSpan.FromSeconds(1), "Calculation invalid");
        }

        /// <summary>
        /// Tests the <see cref="SystemTrayIconPresenter.Configuration"/> property.
        /// </summary>
        [Test]
        public void UserConfigurationChangeValueTest()
        {
            UserConfiguration configuration = new UserConfiguration();
            configuration.Start = DateTime.MinValue;
            configuration.Calculator = new WorkingHoursCalculatorStub();

            this.presenter.Show();
            this.presenter.Configuration = configuration;
            Thread.Sleep(500);

            configuration.Start = DateTime.Now;
            Thread.Sleep(500);

            WorkingHours workingHours = this.viewStub.WorkingHours;
            this.presenter.Hide();

            Assert.That(workingHours.Start != DateTime.MinValue, "Start invalid");
            Assert.That(workingHours.CalculatedWorkingHours <= TimeSpan.FromSeconds(10), "Calculation invalid");
        }

        /// <summary>
        /// Tests the <see cref="SystemTrayIconPresenter.Configuration"/> property.
        /// </summary>
        [Test]
        public void UserConfigurationChangeObjectTest()
        {
            UserConfiguration configuration = new UserConfiguration();
            configuration.Start = DateTime.MinValue;
            configuration.Calculator = new WorkingHoursCalculatorStub();

            this.presenter.Show();
            this.presenter.Configuration = configuration;
            Thread.Sleep(500);

            UserConfiguration configurationNew = new UserConfiguration();
            configurationNew.Start = DateTime.Now;
            configurationNew.Calculator = new WorkingHoursCalculatorStub();

            this.presenter.Configuration = configurationNew;
            Thread.Sleep(500);

            WorkingHours workingHours = this.viewStub.WorkingHours;
            this.presenter.Hide();

            Assert.That(workingHours.Start != DateTime.MinValue, "Start invalid");
            Assert.That(workingHours.CalculatedWorkingHours <= TimeSpan.FromSeconds(10), "Calculation invalid");
        }

        /// <summary>
        /// Tests the <see cref="SystemTrayIconPresenter.Configuration"/> property.
        /// </summary>
        [Test]
        public void UserConfigurationNullTest()
        {
            this.viewStub.WorkingHours = new WorkingHours();

            this.presenter.Show();
            this.presenter.Configuration = null;
            Thread.Sleep(500);

            WorkingHours workingHours = this.viewStub.WorkingHours;
            this.presenter.Hide();

            Assert.That(workingHours != null, "Value invalid");
        }

        /// <summary>
        /// Tests the <see cref="SystemTrayIconPresenter.Configuration"/> property.
        /// </summary>
        [Test]
        public void UserInvalidConfigurationTest()
        {
            UserConfiguration configuration = new UserConfiguration();
            configuration.Start = DateTime.MinValue;
            configuration.Calculator = null;

            this.presenter.Show();
            this.presenter.Configuration = configuration;
            Thread.Sleep(500);

            WorkingHours workingHours = this.viewStub.WorkingHours;
            this.presenter.Hide();

            Assert.That(workingHours != null, "Value invalid");
        }

        #endregion

        /// <summary>
        /// Handles the <see cref="SystemTrayIconPresenter.Quit"/> event.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> that contains the data.</param>
        private void Quit(object sender, EventArgs e)
        {
            this.quitInvoked = true;
        }

        /// <summary>
        /// Handles the <see cref="SystemTrayIconPresenter.Info"/> event.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> that contains the data.</param>
        private void Info(object sender, EventArgs e)
        {
            this.infoInvoked = true;
        }

        /// <summary>
        /// Handles the <see cref="SystemTrayIconPresenter.Configure"/> event.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> that contains the data.</param>
        private void Configure(object sender, EventArgs e)
        {
            this.configureInvoked = true;
        }

        #endregion
    }
}
