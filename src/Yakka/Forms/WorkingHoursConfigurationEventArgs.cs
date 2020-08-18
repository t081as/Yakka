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

namespace Yakka.Forms
{
    /// <summary>
    /// This class encapsulates event data for configuration events.
    /// </summary>
    public class WorkingHoursConfigurationEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WorkingHoursConfigurationEventArgs"/> class.
        /// </summary>
        public WorkingHoursConfigurationEventArgs()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkingHoursConfigurationEventArgs"/> class.
        /// </summary>
        /// <param name="configuration">The working hours configuration.</param>
        public WorkingHoursConfigurationEventArgs(WorkingHoursConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        /// Gets or sets the working hours configuration.
        /// </summary>
        /// <value>The working hours configuration.</value>
        public WorkingHoursConfiguration Configuration { get; set; } = new WorkingHoursConfiguration();
    }
}
