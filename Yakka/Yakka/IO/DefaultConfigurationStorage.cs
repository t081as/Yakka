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
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
#endregion

namespace Yakka.IO
{
    /// <summary>
    /// Represents an implementation of the <see cref="IConfigurationStorage"/> interface using the
    /// application data directory to read and write the <see cref="UserConfiguration"/>.
    /// </summary>
    public class DefaultConfigurationStorage : IConfigurationStorage
    {
        #region Constants and Fields

        /// <summary>
        /// Represents the filename used for the cofiguration file.
        /// </summary>
        public const string FILENAME = "Yakka-Configuration.bin";

        #endregion

        #region Methods

        /// <summary>
        /// Writes the given <see cref="UserConfiguration"/> to a persistent storage.
        /// </summary>
        /// <param name="configuration">A <see cref="UserConfiguration"/> that shall be written.</param>
        /// <exception cref="ArgumentNullException"><c>configuration</c> is <c>null</c>.</exception>
        /// <exception cref="IOException">The <see cref="UserConfiguration"/> could not be written.</exception>
        public void Write(UserConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException("configuration");
            }

            try
            {
                DirectoryInfo defaultDirectory = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Application.ProductName));
                if (!defaultDirectory.Exists)
                {
                    defaultDirectory.Create();
                }

                using (FileStream storageFileStream = File.Create(Path.Combine(defaultDirectory.FullName, FILENAME)))
                {
                        BinaryFormatter serializer = new BinaryFormatter();
                        serializer.Serialize(storageFileStream, configuration);
                }
            }
            catch (Exception ex)
            {
                throw new IOException("Error while writing to the configuration file", ex);
            }
        }

        /// <summary>
        /// Reads the <see cref="UserConfiguration"/> from a persistent storage.
        /// </summary>
        /// <returns>A <see cref="UserConfiguration"/> that have been read.</returns>
        /// <exception cref="IOException">The <see cref="UserConfiguration"/> could not be read.</exception>
        public UserConfiguration Read()
        {
            try
            {
                using (FileStream storageFileStream = File.OpenRead(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Application.ProductName, FILENAME)))
                {
                    BinaryFormatter serializer = new BinaryFormatter();
                    return (UserConfiguration)serializer.Deserialize(storageFileStream);
                }
            }
            catch (Exception ex)
            {
                throw new IOException("Error while reading from the isolated storage", ex);
            }
        }

        #endregion
    }
}
