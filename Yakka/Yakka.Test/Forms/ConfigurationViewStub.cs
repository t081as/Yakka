#region GNU General Public License 3
// Yakka - A system tray application calculating and displaying your working hours
// Copyright (C) 2017 -2018 Tobias Koch
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
using Yakka.Calculator;
using Yakka.Forms;
#endregion

namespace Yakka.Test.Forms
{
    /// <summary>
    /// Represents a test stub implementing the <see cref="IConfigurationView"/> interface.
    /// </summary>
    public class ConfigurationViewStub : IConfigurationView
    {
        #region Events

        /// <summary>
        /// Occurs when the configuration is changed by the user.
        /// </summary>
        public event EventHandler ConfigurationChanged;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the start time of the working hours.
        /// </summary>
        public DateTime Start
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the calculator choosen by the user.
        /// </summary>
        public IWorkingHoursCalculator SelectedCalculator
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the calculators that are available and could be chosen by the user.
        /// </summary>
        public IWorkingHoursCalculator[] AvailableCalculators
        {
            get;
            set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Raises the <see cref="ConfigurationChanged"/> event.
        /// </summary>
        /// <param name="e">A <see cref="EventArgs"/> that contains the data.</param>
        public void OnConfigurationChanged(EventArgs e)
        {
            this.ConfigurationChanged?.Invoke(this, e);
        }

        #endregion
    }
}
