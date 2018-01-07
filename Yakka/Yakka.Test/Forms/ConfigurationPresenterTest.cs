#region GNU General Public License 3
// Yakka - A system tray application calculating and displaying your working hours
// Copyright (C) 2017-2018  Tobias Koch
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
using Yakka.Calculator;
using Yakka.Forms;
#endregion

namespace Yakka.Test.Forms
{
    /// <summary>
    /// Contains the unit tests of the <see cref="ConfigurationPresenter"/> class.
    /// </summary>
    [TestFixture]
    public class ConfigurationPresenterTest
    {
        #region Constants and Fields

        /// <summary>
        /// Represents the default start time used for tests.
        /// </summary>
        private DateTime defaultStart;

        /// <summary>
        /// Represents the default calculator used for tests.
        /// </summary>
        private IWorkingHoursCalculator defaultCalculator;

        /// <summary>
        /// Contains the test stub representing the view.
        /// </summary>
        private ConfigurationViewStub viewStub = null;

        /// <summary>
        /// Represents the subject under test.
        /// </summary>
        private ConfigurationPresenter presenter = null;

        /// <summary>
        /// Represents the <see cref="UserConfiguration"/> used by the presenter.
        /// </summary>
        private UserConfiguration configuration = null;

        /// <summary>
        /// Used to check if the configuration has been changed internally.
        /// </summary>
        private volatile bool configurationChangedInvoked = false;

        #endregion

        #region Methods

        #region Setup / Teardown

        /// <summary>
        /// Gets called before a test starts.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.defaultStart = new DateTime(1984, 11, 27, 8, 0, 0);
            this.defaultCalculator = new WorkingHoursCalculatorStub();
            this.configurationChangedInvoked = false;

            this.viewStub = new ConfigurationViewStub();
            this.configuration = new UserConfiguration();
            this.configuration.Start = this.defaultStart;
            this.configuration.Calculator = this.defaultCalculator;
            this.configuration.PropertyChanged += this.Configuration_PropertyChanged;

            this.presenter = new ConfigurationPresenter(this.viewStub, this.configuration);
        }

        /// <summary>
        /// Gets called after a test ends.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            this.presenter.Dispose();
            this.presenter = null;

            this.viewStub = null;

            this.configuration.PropertyChanged -= this.Configuration_PropertyChanged;
            this.configuration = null;
        }

        #endregion

        /// <summary>
        /// Tests the presenter initialization with an invalid argument.
        /// </summary>
        [Test]
        public void ConstructorViewNullTest()
        {
            Assert.Throws<ArgumentNullException>(() => new ConfigurationPresenter(null, this.configuration));
        }

        /// <summary>
        /// Tests the presenter initialization with an invalid argument.
        /// </summary>
        [Test]
        public void ConstructorConfigurationNullTest()
        {
            Assert.Throws<ArgumentNullException>(() => new ConfigurationPresenter(this.viewStub, null));
        }

        /// <summary>
        /// Tests that no changes are performed without the notification from the view.
        /// </summary>
        [Test]
        public void NoChangeTest()
        {
            DateTime now = DateTime.Now;

            this.viewStub.Start = now;
            this.viewStub.SelectedCalculator = null;

            Thread.Sleep(500);

            Assert.That(this.configuration.Start == this.defaultStart, "Configuration invalid");
            Assert.That(this.configuration.Calculator == this.defaultCalculator, "Configuration invalid");
        }

        /// <summary>
        /// Tests that the changes are performed correctly when notified by the view.
        /// </summary>
        [Test]
        public void ChangeTest()
        {
            DateTime now = DateTime.Now;

            this.viewStub.Start = now;
            this.viewStub.SelectedCalculator = null;

            this.viewStub.OnConfigurationChanged(EventArgs.Empty);

            Thread.Sleep(500);

            Assert.That(this.configuration.Start == now, "Configuration invalid");
            Assert.That(this.configuration.Calculator == null, "Configuration invalid");
        }

        /// <summary>
        /// Tests that there are no changes if the event has been invoked by the view without actual data changes.
        /// </summary>
        [Test]
        public void EventWithoutChangesTest()
        {
            this.viewStub.OnConfigurationChanged(EventArgs.Empty);

            Thread.Sleep(500);

            Assert.That(this.configurationChangedInvoked == false, "Event invocation detected without actual changes");
        }

        /// <summary>
        /// Handles the <see cref="UserConfiguration.PropertyChanged"/> event.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the data.</param>
        private void Configuration_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            this.configurationChangedInvoked = true;
        }

        #endregion
    }
}
