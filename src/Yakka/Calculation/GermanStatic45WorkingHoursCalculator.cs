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
    /// Represents an implementation of <see cref="IWorkingHoursCalculator"/> enforcing a break of
    /// 45 minutes after 6 hours of work.
    /// </summary>
    public class GermanStatic45WorkingHoursCalculator : WorkingHoursCalculatorBase
    {
        /// <inheritdoc/>
        public override Guid Id => Guid.Parse("{A62206E7-FBBB-4859-8411-7E5571492E13}");

        /// <inheritdoc/>
        public override string Title => GermanStatic45WorkingHoursCalculatorResources.Title;

        /// <inheritdoc/>
        public override string Description => GermanStatic45WorkingHoursCalculatorResources.Description;

        /// <inheritdoc/>
        public override TimeSpan CalculateBreak(DateTime startTime, DateTime endTime)
        {
            TimeSpan workingHours = endTime - startTime;

            if (workingHours.TotalHours < 6)
            {
                return new TimeSpan(0);
            }
            else if (workingHours.Hours == 6 && workingHours.Minutes <= 45)
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
