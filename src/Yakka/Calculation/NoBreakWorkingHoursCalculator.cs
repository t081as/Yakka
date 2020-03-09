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
    /// Represents an implementation of the <see cref="IWorkingHoursCalculator"/> interface
    /// calculating the working hours without any break.
    /// </summary>
    public class NoBreakWorkingHoursCalculator : IWorkingHoursCalculator
    {
        /// <inheritdoc/>
        public Guid Id => Guid.Parse("{28C2C346-282A-437A-A54A-70E4B347A7DB}");

        /// <inheritdoc/>
        public string Title => NoBreakWorkingHoursCalculatorResources.Title;

        /// <inheritdoc/>
        public string Description => NoBreakWorkingHoursCalculatorResources.Description;

        /// <inheritdoc/>
        public (TimeSpan workTimeSpan, TimeSpan breakTimeSpan) Calculate(DateTime startTime, DateTime endTime)
        {
            if (startTime > endTime)
            {
                throw new ArgumentException(WorkingHoursCalculatorResources.ArgumentException);
            }

            return (endTime - startTime, TimeSpan.Zero);
        }
    }
}
