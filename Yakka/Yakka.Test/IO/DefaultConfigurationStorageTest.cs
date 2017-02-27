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
using Yakka.IO;
#endregion

namespace Yakka.Test.IO
{
    /// <summary>
    /// Contains the unit tests of the <see cref="DefaultConfigurationStorage"/> class.
    /// </summary>
    [TestFixture]
    public class DefaultConfigurationStorageTest
    {
        #region Methods

        /// <summary>
        /// Tests the writer using an invalid argument.
        /// </summary>
        [Test]
        public void WriteArgumentNullTest()
        {
            DefaultConfigurationStorage storage = new DefaultConfigurationStorage();

            Assert.Throws<ArgumentNullException>(() => storage.Write(null));
        }

        /// <summary>
        /// Tests writing and reading the configuration.
        /// </summary>
        [Test]
        public void WriteAndReadTest()
        {
            DefaultConfigurationStorage storage = new DefaultConfigurationStorage();
            UserConfiguration configuration = new UserConfiguration();

            configuration.Start = DateTime.Now;
            configuration.Calculator = new WorkingHoursCalculatorStub();

            storage.Write(configuration);

            UserConfiguration loadedConfiguration = storage.Read();

            Assert.That(configuration.Start == loadedConfiguration.Start, "Invalid value");
            Assert.That(configuration.Calculator.GetType() == loadedConfiguration.Calculator.GetType(), "Invalid value");
        }

        #endregion
    }
}
