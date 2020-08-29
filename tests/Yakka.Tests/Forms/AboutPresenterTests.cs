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
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mjolnir.IO;
using Moq;
using Yakka.Calculation;
using Yakka.Forms;

namespace Yakka.Tests.Forms
{
    /// <summary>
    /// Contains unit tests of the <see cref="AboutPresenter"/> class.
    /// </summary>
    [TestClass]
    public class AboutPresenterTests
    {
        /// <summary>
        /// Tests the <see cref="AboutPresenter.AboutPresenter(IAboutView)"/> constructor.
        /// </summary>
        [TestMethod]
        public void Test()
        {
            string name = string.Empty;
            string version = string.Empty;
            string description = string.Empty;
            string copyright = string.Empty;
            List<Author> authors = new List<Author>();
            string licenseText = string.Empty;

            var viewMock = new Mock<IAboutView>();
            viewMock.SetupSet(m => m.ApplicationName = It.IsAny<string>()).Callback<string>(s => { name = s; });
            viewMock.SetupSet(m => m.ApplicationVersion = It.IsAny<string>()).Callback<string>(s => { version = s; });
            viewMock.SetupSet(m => m.ApplicationDescription = It.IsAny<string>()).Callback<string>(s => { description = s; });
            viewMock.SetupSet(m => m.ApplicatioCopyright = It.IsAny<string>()).Callback<string>(s => { copyright = s; });
            viewMock.SetupSet(m => m.Authors = It.IsAny<IEnumerable<Author>>()).Callback<IEnumerable<Author>>(a => { authors = new List<Author>(a); });
            viewMock.SetupSet(m => m.LicenseText = It.IsAny<string>()).Callback<string>(s => { licenseText = s; });

            var presenter = new AboutPresenter(viewMock.Object);

            Assert.IsFalse(string.IsNullOrWhiteSpace(name));
            Assert.IsFalse(string.IsNullOrWhiteSpace(version));
            Assert.IsFalse(string.IsNullOrWhiteSpace(description));
            Assert.IsFalse(string.IsNullOrWhiteSpace(copyright));
            Assert.IsTrue(authors.Any());
            Assert.IsFalse(string.IsNullOrWhiteSpace(licenseText));
        }
    }
}
