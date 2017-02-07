﻿#region GNU General Public License 3
//  Yakka - A system tray application calculating and displaying your working hours
//  Copyright (C) 2017  Tobias Koch
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program. If not, see <http://www.gnu.org/licenses/>.
#endregion

#region Namespaces
using System;
using System.Globalization;
using Yakka.Calculator;
#endregion

namespace Yakka.Test
{
    /// <summary>
    /// Represents a stub for the <see cref="IWorkingHoursCalculator"/> interface adding 30 minutes break after
    /// four hours of work and additional 45 minutes after seven hours.
    /// </summary>
    public class WorkingHoursCalculatorStub : IWorkingHoursCalculator
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
        public string GetDescription(CultureInfo culture)
        {
            if (culture == null)
            {
                throw new ArgumentNullException("culture");
            }

            switch (culture.TwoLetterISOLanguageName.ToLowerInvariant())
            {
                case "de":
                    return "Testpause (30 min > 4h Arbeit, 75 min > 7h Arbeit)";

                default:
                    return "Test break (30 min > 4h work, 75 min > 7h work)";
            }
        }

        /// <summary>
        /// Calculates the working hours.
        /// </summary>
        /// <param name="start">The date and time representing the start of the working day.</param>
        /// <param name="end">The date and time representing the end of the working day.</param>
        /// <returns>The <see cref="TimeSpan"/> representing the working hours.</returns>
        /// <exception cref="InvalidOperationException">The working hours could not be calculated.</exception>
        public TimeSpan CalculateWorkingHours(DateTime start, DateTime end)
        {
            if (start > end)
            {
                throw new InvalidOperationException("start > end");
            }

            return (end - start) - this.CalculateBreak(start, end);
        }

        /// <summary>
        /// Calculates the break.
        /// </summary>
        /// <param name="start">The date and time representing the start of the working day.</param>
        /// <param name="end">The date and time representing the end of the working day.</param>
        /// <returns>The <see cref="TimeSpan"/> representing the break.</returns>
        /// <exception cref="InvalidOperationException">The break could not be calculated.</exception>
        public TimeSpan CalculateBreak(DateTime start, DateTime end)
        {
            if (start > end)
            {
                throw new InvalidOperationException("start > end");
            }

            TimeSpan workingHours = end - start;

            if (workingHours.TotalHours < 4)
            {
                return new TimeSpan(0);
            }
            else if (workingHours.TotalHours < 7)
            {
                return TimeSpan.FromMinutes(30);
            }
            else
            {
                return TimeSpan.FromMinutes(75);
            }
        }

        #endregion
    }
}
