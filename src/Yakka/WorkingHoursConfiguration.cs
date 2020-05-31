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
using Yakka.Calculation;

namespace Yakka
{
    /// <summary>
    /// Encapsulates the current working hours configuration.
    /// </summary>
    public class WorkingHoursConfiguration
    {
        /// <summary>
        /// Gets or sets the start date and time of the current working day.
        /// </summary>
        /// <value>The start date and time of the current working day.</value>
        public DateTime StartTime { get; set; } = DateTime.Now;

        /// <summary>
        /// Gets or sets the implementation of the <see cref="IWorkingHoursCalculator"/> interface
        /// that shall be used to calculate.
        /// </summary>
        /// <value>
        /// The implementation of the <see cref="IWorkingHoursCalculator"/> interface
        /// that shall be used to calculate.
        /// </value>
        public IWorkingHoursCalculator? WorkingHoursCalculator { get; set; } = null;

        /// <summary>
        /// Gets or sets the break mode configured by the user.
        /// </summary>
        /// <value>The break mode configured by the user.</value>
        public BreakMode BreakMode { get; set; } = BreakMode.Automatic;

        /// <summary>
        /// Gets or sets the manual break configured by the user.
        /// </summary>
        /// <value>The manual break configured by the user.</value>
        /// <remarks>
        /// This value will only be used if <see cref="BreakMode"/> is set to <see cref="BreakMode.Manual"/>.
        /// </remarks>
        public TimeSpan ManualBreakTime { get; set; } = TimeSpan.FromSeconds(0);
    }
}