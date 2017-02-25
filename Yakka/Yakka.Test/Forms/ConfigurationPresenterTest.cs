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
    /// Contains the unit tests of the <see cref="ConfigurationPresenter"/> class.
    /// </summary>
    [TestFixture]
    public class ConfigurationPresenterTest
    {
        #region Constants and Fields

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

        #endregion

        #region Methods

        #region Setup / Teardown

        /// <summary>
        /// Gets called before a test starts.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.viewStub = new ConfigurationViewStub();
            this.configuration = new UserConfiguration();

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

        #endregion
    }
}
