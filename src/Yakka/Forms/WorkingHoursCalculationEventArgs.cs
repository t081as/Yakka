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
    /// This class encapsulates event data for events that requires a <see cref="WorkingHoursCalculation"/>.
    /// </summary>
    public class WorkingHoursCalculationEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WorkingHoursCalculationEventArgs"/> class.
        /// </summary>
        public WorkingHoursCalculationEventArgs()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkingHoursCalculationEventArgs"/> class.
        /// </summary>
        /// <param name="calculation">The calculation encapsulated by this instance.</param>
        public WorkingHoursCalculationEventArgs(WorkingHoursCalculation calculation)
        {
            this.Calculation = calculation;
        }

        /// <summary>
        /// Gets or sets the calculation encapsulated by this instance.
        /// </summary>
        /// <value>The calculation encapsulated by this instance.</value>
        public WorkingHoursCalculation Calculation { get; set; } = new WorkingHoursCalculation();
    }
}
