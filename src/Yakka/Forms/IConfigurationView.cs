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

namespace Yakka.Forms
{
    /// <summary>
    /// Describes objects that serve as a view for changing the working hours configuration.
    /// </summary>
    public interface IConfigurationView
    {
        /// <summary>
        /// Gets or sets the start date and time of the current working day.
        /// </summary>
        /// <value>The start date and time of the current working day.</value>
        DateTime StartTime { get; set; }

        /// <summary>
        /// Gets or sets the implementation of the <see cref="IWorkingHoursCalculator"/> interface
        /// that shall be used to calculate.
        /// </summary>
        /// <value>
        /// The implementation of the <see cref="IWorkingHoursCalculator"/> interface
        /// that shall be used to calculate.
        /// </value>
        IWorkingHoursCalculator WorkingHoursCalculator { get; set; }

        /// <summary>
        /// Gets or sets the break mode configured by the user.
        /// </summary>
        /// <value>The break mode configured by the user.</value>
        BreakMode BreakMode { get; set; }

        /// <summary>
        /// Gets or sets the manual break configured by the user.
        /// </summary>
        /// <value>The manual break configured by the user.</value>
        /// <remarks>
        /// This value will only be used if <see cref="BreakMode"/> is set to <see cref="BreakMode.Manual"/>.
        /// </remarks>
        TimeSpan ManualBreakTime { get; set; }
    }
}
