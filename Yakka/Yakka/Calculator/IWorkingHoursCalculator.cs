#region GNU General Public License 3
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
    /// Provides the ability to calculate working hours.
    /// </summary>
    public interface IWorkingHoursCalculator
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
        string GetDescription(CultureInfo culture);

        /// <summary>
        /// Calculates the working hours.
        /// </summary>
        /// <param name="start">The date and time representing the start of the working day.</param>
        /// <param name="end">The date and time representing the end of the working day.</param>
        /// <returns>The <see cref="TimeSpan"/> representing the working hours.</returns>
        /// <exception cref="InvalidOperationException">The working hours could not be calculated.</exception>
        TimeSpan CalculateWorkingHours(DateTime start, DateTime end);

        /// <summary>
        /// Calculates the break.
        /// </summary>
        /// <param name="start">The date and time representing the start of the working day.</param>
        /// <param name="end">The date and time representing the end of the working day.</param>
        /// <returns>The <see cref="TimeSpan"/> representing the break.</returns>
        /// <exception cref="InvalidOperationException">The break could not be calculated.</exception>
        TimeSpan CalculateBreak(DateTime start, DateTime end);

        #endregion
    }
}
