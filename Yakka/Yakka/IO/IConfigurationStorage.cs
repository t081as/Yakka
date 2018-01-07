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
using System.IO;
#endregion

namespace Yakka.IO
{
    /// <summary>
    /// Provides the interface to an object that is able to write and read a <see cref="UserConfiguration"/>.
    /// </summary>
    public interface IConfigurationStorage
    {
        #region Methods

        /// <summary>
        /// Writes the given <see cref="UserConfiguration"/> to a persistent storage.
        /// </summary>
        /// <param name="configuration">A <see cref="UserConfiguration"/> that shall be written.</param>
        /// <exception cref="ArgumentNullException"><c>configuration</c> is <c>null</c>.</exception>
        /// <exception cref="IOException">The <see cref="UserConfiguration"/> could not be written.</exception>
        void Write(UserConfiguration configuration);

        /// <summary>
        /// Reads the <see cref="UserConfiguration"/> from a persistent storage.
        /// </summary>
        /// <returns>A <see cref="UserConfiguration"/> that have been read.</returns>
        /// <exception cref="IOException">The <see cref="UserConfiguration"/> could not be read.</exception>
        UserConfiguration Read();

        #endregion
    }
}
