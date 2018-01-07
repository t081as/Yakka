﻿#region GNU General Public License 3
// Yakka - A system tray application calculating and displaying your working hours
// Copyright (C) 2017-2018  Tobias Koch
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
#endregion

#region Namespaces
using System;
using System.Globalization;
#endregion

namespace Yakka.Calculator
{
    /// <summary>
    /// Represents an implementation of <see cref="IWorkingHoursCalculator"/> calculating the break according to german law;
    /// i.e. 30 minutes when working more than six hours and 45 minutes when working more than 9 hours.
    /// </summary>
    [Serializable]
    public class DefaultGermanBreakWorkingHoursCalculator : WorkingHoursCalculator
    {
        #region Methods

        /// <summary>
        /// Gets the description of this <see cref="IWorkingHoursCalculator"/> for the specified <see cref="CultureInfo">language</see>.
        /// </summary>
        /// <param name="culture">The desired <see cref="CultureInfo">language</see> of the description.</param>
        /// <returns>The description of this <see cref="IWorkingHoursCalculator"/>.</returns>
        /// <remarks>
        /// If the description is not available in the desired language the default language shall be used.
        /// </remarks>
        /// <exception cref="ArgumentNullException"><c>culture</c> is <c>null</c>.</exception>
        public override string GetDescription(CultureInfo culture)
        {
            if (culture == null)
            {
                throw new ArgumentNullException("culture");
            }

            switch (culture.TwoLetterISOLanguageName.ToLowerInvariant())
            {
                case "de":
                    return "Deutschland: Standardpause (30 min > 6h Arbeit, 45 min > 9h Arbeit)";

                default:
                    return "Germany: default break (30 min > 6h work, 45 min > 9h work)";
            }
        }

        /// <summary>
        /// Calculates the break.
        /// </summary>
        /// <param name="start">The date and time representing the start of the working day.</param>
        /// <param name="end">The date and time representing the end of the working day.</param>
        /// <returns>The <see cref="TimeSpan"/> representing the break.</returns>
        /// <exception cref="InvalidOperationException">The break could not be calculated.</exception>
        public override TimeSpan CalculateBreak(DateTime start, DateTime end)
        {
            if (start > end)
            {
                throw new InvalidOperationException("start > end");
            }

            TimeSpan workingHours = end - start;

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

        #endregion
    }
}
