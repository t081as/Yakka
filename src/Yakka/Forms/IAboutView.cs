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
using Mjolnir.IO;

namespace Yakka.Forms
{
    /// <summary>
    /// Describes objects that serve as a view for application information.
    /// </summary>
    public interface IAboutView
    {
        /// <summary>
        /// Gets or sets the application name.
        /// </summary>
        /// <value>The application name.</value>
        string ApplicationName { get; set; }

        /// <summary>
        /// Gets or sets the application version.
        /// </summary>
        /// <value>The application version.</value>
        string ApplicationVersion { get; set; }

        /// <summary>
        /// Gets or sets the application description.
        /// </summary>
        /// <value>The application description.</value>
        string ApplicationDescription { get; set; }

        /// <summary>
        /// Gets or sets the application copyright.
        /// </summary>
        /// <value>The application copyright.</value>
        string ApplicatioCopyright { get; set; }

        /// <summary>
        /// Gets or sets the list of authors.
        /// </summary>
        /// <value>The list of authors.</value>
        IEnumerable<Author> Authors { get; set; }

        /// <summary>
        /// Gets or sets the license text.
        /// </summary>
        /// <value>The license text.</value>
        string LicenseText { get; set; }
    }
}
