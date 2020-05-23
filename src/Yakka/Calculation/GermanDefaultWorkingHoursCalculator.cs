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
    /// Represents an implementation of <see cref="IWorkingHoursCalculator"/> calculating the break according to german law;
    /// i.e. 30 minutes when working more than six hours and 45 minutes when working more than 9 hours.
    /// </summary>
    public class GermanDefaultWorkingHoursCalculator : IWorkingHoursCalculator
    {
        /// <inheritdoc/>
        public Guid Id => Guid.Parse("{4536F994-A4EB-4D11-9FB2-674A2ACA7177}");

        /// <inheritdoc/>
        public string Title => GermanDefaultWorkingHoursCalculatorResources.Title;

        /// <inheritdoc/>
        public string Description => GermanDefaultWorkingHoursCalculatorResources.Description;

        /// <inheritdoc/>
        public (TimeSpan workTimeSpan, TimeSpan breakTimeSpan) Calculate(DateTime startTime, DateTime endTime)
        {
            if (startTime > endTime)
            {
                throw new ArgumentException(WorkingHoursCalculatorResources.ArgumentException);
            }

            var calculatedBreak = CalculateBreak(startTime, endTime);

            return (endTime - startTime - calculatedBreak, calculatedBreak);
        }

        /// <summary>
        /// Calculates the break.
        /// </summary>
        /// <param name="startTime">The date and time representing the start of the working day.</param>
        /// <param name="endTime">The date and time representing the end of the working day.</param>
        /// <returns>A <see cref="TimeSpan"/> representing the break.</returns>
        private static TimeSpan CalculateBreak(DateTime startTime, DateTime endTime)
        {
            TimeSpan workingHours = endTime - startTime;

            if (workingHours.TotalHours < 6)
            {
                return new TimeSpan(0);
            }
            else if (workingHours.Hours == 6 && workingHours.Minutes <= 30)
            {
                return TimeSpan.FromMinutes(workingHours.Minutes);
            }
            else if (workingHours.TotalHours < 9 || (workingHours.Hours == 9 && workingHours.Minutes <= 30))
            {
                return TimeSpan.FromMinutes(30);
            }
            else if (workingHours.Hours == 9 && workingHours.Minutes > 30 && workingHours.Minutes <= 45)
            {
                return TimeSpan.FromMinutes(workingHours.Minutes);
            }
            else
            {
                return TimeSpan.FromMinutes(45);
            }
        }
    }
}
