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

namespace Yakka
{
    /// <summary>
    /// Represents the result of the calculation of the working hours.
    /// </summary>
    public class WorkingHoursCalculation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WorkingHoursCalculation"/> class.
        /// </summary>
        public WorkingHoursCalculation()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkingHoursCalculation"/> class with the specified configuration.
        /// </summary>
        /// <param name="configuration">The <see cref="WorkingHoursConfiguration"/> that shall be used.</param>
        public WorkingHoursCalculation(WorkingHoursConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        /// Gets or sets the working hours configurationused for the calculation.
        /// </summary>
        /// <value>The working hours configurationused for the calculation.</value>
        public WorkingHoursConfiguration Configuration { get; set; } = new WorkingHoursConfiguration();

        /// <summary>
        /// Gets or sets the calculated working hours.
        /// </summary>
        /// <value>The calculated working hours.</value>
        public TimeSpan CalculatedWorkingHours { get; set; } = TimeSpan.Zero;

        /// <summary>
        /// Gets or sets the calculated break.
        /// </summary>
        /// <value>The calculated break.</value>
        public TimeSpan CalculatedBreak { get; set; } = TimeSpan.Zero;

        /// <summary>
        /// Gets a dictionary containing the <see cref="DateTime"/> of full working hours.
        /// </summary>
        /// <value>A dictionary containing the <see cref="DateTime"/> of full working hours.</value>
        public IDictionary<int, DateTime> FullHoursWorked { get; } = new Dictionary<int, DateTime>();
    }
}
