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

namespace Yakka.Calculation
{
    /// <summary>
    /// Describes objects providing the ability to calculate working hours.
    /// </summary>
    public interface IWorkingHoursCalculator
    {
        /// <summary>
        /// Gets the unique id of the calculator.
        /// </summary>
        /// <value>The unique id of the calculator.</value>
        Guid Id { get; }

        /// <summary>
        /// Gets the title of this calculator.
        /// </summary>
        /// <value>The title of this calculator.</value>
        string Title { get; }

        /// <summary>
        /// Gets the description of this calculator.
        /// </summary>
        /// <value>The description of this calculator.</value>
        string Description { get; }

        /// <summary>
        /// Calculates and returns the working hours and the break.
        /// </summary>
        /// <param name="startTime">The date and time representing the start of the working day.</param>
        /// <param name="endTime">The date and time representing the end of the working day.</param>
        /// <returns>
        /// A <see cref="ValueTuple{T1, T2}"/> containing the following values:
        /// <list type="bullet">
        /// <item>workTimeSpan: a <see cref="TimeSpan"/> representing the calculated working hours.</item>
        /// <item>breakTimeSpan: a <see cref="TimeSpan"/> representing the calculated break.</item>
        /// </list>
        /// </returns>
        (TimeSpan workTimeSpan, TimeSpan breakTimeSpan) Calculate(DateTime startTime, DateTime endTime);
    }
}