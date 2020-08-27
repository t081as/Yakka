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
    /// Represents the base class for working hours calculators.
    /// </summary>
    public abstract class WorkingHoursCalculatorBase : IWorkingHoursCalculator
    {
        /// <inheritdoc/>
        public abstract Guid Id { get; }

        /// <inheritdoc/>
        public abstract string Title { get; }

        /// <inheritdoc/>
        public abstract string Description { get; }

        /// <inheritdoc/>
        public virtual (TimeSpan workTimeSpan, TimeSpan breakTimeSpan, string? warning) Calculate(DateTime startTime, DateTime endTime)
        {
            if (startTime > endTime)
            {
                throw new ArgumentException(WorkingHoursCalculatorResources.ArgumentException);
            }

            var calculatedBreak = this.CalculateBreak(startTime, endTime);

            return (endTime - startTime - calculatedBreak, calculatedBreak, null);
        }

        /// <summary>
        /// Calculates the break.
        /// </summary>
        /// <param name="startTime">The date and time representing the start of the working day.</param>
        /// <param name="endTime">The date and time representing the end of the working day.</param>
        /// <returns>A <see cref="TimeSpan"/> representing the break.</returns>
        public abstract TimeSpan CalculateBreak(DateTime startTime, DateTime endTime);

        /// <summary>
        /// Gets the <see cref="string"/> representation of this class.
        /// </summary>
        /// <returns>The <see cref="string"/> representation of this class.</returns>
        public override string ToString() => this.Title;
    }
}
