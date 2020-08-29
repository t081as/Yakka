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
using System.IO;
using System.Reflection;
using System.Text;
using Mjolnir.IO;

namespace Yakka.Forms
{
    /// <summary>
    /// Represents the presenter managing an <see cref="IAboutView"/>.
    /// </summary>
    public class AboutPresenter
    {
        /// <summary>
        /// The name of the license text file.
        /// </summary>
        public const string LicenseTextFile = "LICENSE.txt";

        /// <summary>
        /// The name of the text file containing the licenses of third party components.
        /// </summary>
        public const string ThirdPartyLicenseTextFile = "NOTICE.txt";

        /// <summary>
        /// The name of the author text file.
        /// </summary>
        public const string AuthorTextFile = "AUTHORS.txt";

        /// <summary>
        /// The reference to the view.
        /// </summary>
        private IAboutView view;

        /// <summary>
        /// Initializes a new instance of the <see cref="AboutPresenter"/> class.
        /// </summary>
        /// <param name="view">The reference to the view that shall be managed.</param>
        public AboutPresenter(IAboutView view)
        {
            this.view = view ?? throw new ArgumentNullException(nameof(view));

            this.LoadApplicationInformation();
            this.LoadAuthorList();
            this.LoadLicenseInformation();
        }

        /// <summary>
        /// Loads application information and updates the view.
        /// </summary>
        private void LoadApplicationInformation()
        {
            var assembly = Assembly.GetExecutingAssembly();

            var assemblyVersion = assembly?.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ?? string.Empty;
            var assemblyFileVersion = assembly?.GetCustomAttribute<AssemblyFileVersionAttribute>()?.Version ?? string.Empty;

            this.view.ApplicationName = assembly?.GetCustomAttribute<AssemblyTitleAttribute>()?.Title ?? string.Empty;
            this.view.ApplicationVersion = $"{assemblyVersion} ({assemblyFileVersion})";
            this.view.ApplicationDescription = Global.Description;
            this.view.ApplicatioCopyright = assembly?.GetCustomAttribute<AssemblyCopyrightAttribute>()?.Copyright ?? string.Empty;
        }

        /// <summary>
        /// Loads the author list and updates the view.
        /// </summary>
        private void LoadAuthorList()
        {
            var applicationPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;
            var authorsFile = Path.Combine(applicationPath, AuthorTextFile);

            if (File.Exists(authorsFile))
            {
                using var authorsStream = File.OpenRead(authorsFile);
                this.view.Authors = Author.From(authorsStream);
            }
        }

        /// <summary>
        /// Loads license information and updates the view.
        /// </summary>
        private void LoadLicenseInformation()
        {
            var applicationPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;
            var licenseFile = Path.Combine(applicationPath, LicenseTextFile);
            var thirdPartyFile = Path.Combine(applicationPath, ThirdPartyLicenseTextFile);

            var licenseInformationBuilder = new StringBuilder();

            if (File.Exists(licenseFile))
            {
                licenseInformationBuilder.AppendLine(File.ReadAllText(licenseFile));
                licenseInformationBuilder.AppendLine();
            }

            if (File.Exists(thirdPartyFile))
            {
                licenseInformationBuilder.AppendLine(File.ReadAllText(thirdPartyFile));
            }

            this.view.LicenseText = licenseInformationBuilder.ToString();
        }
    }
}
