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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace Yakka.Forms
{
    /// <summary>
    /// Represents the presenter managing the <see cref="IConfigurationView"/>.
    /// </summary>
    public class ConfigurationPresenter
    {
        #region Constants and Fields

        /// <summary>
        /// Represents the reference to the view managed by this presenter.
        /// </summary>
        private IConfigurationView view;

        /// <summary>
        /// Represents the reference to the user configuration that shall be displayed and updated..
        /// </summary>
        private UserConfiguration configuration;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationPresenter"/> class.
        /// </summary>
        /// <param name="view">The view that shall be used.</param>
        /// <param name="configuration">The user configuration that shall be displayed and updated.</param>
        /// <exception cref="ArgumentNullException"><c>view</c> is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException"><c>configuration</c> is <c>null</c>.</exception>
        public ConfigurationPresenter(IConfigurationView view, UserConfiguration configuration)
        {
            if (view == null)
            {
                throw new ArgumentNullException("view");
            }

            if (configuration == null)
            {
                throw new ArgumentNullException("configuration");
            }

            this.view = view;
            this.configuration = configuration;

            if (this.view != null && this.configuration != null)
            {
                // I'm only fixing the ci build
            }
        }

        #endregion
    }
}
