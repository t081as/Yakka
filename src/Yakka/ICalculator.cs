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

namespace Yakka
{
    /// <summary>
    /// Describes objects able to perform a working hours calculation.
    /// </summary>
    public interface ICalculator
    {
        /// <summary>
        /// Calculates the working hours using the specified <paramref name="configuration"/>.
        /// </summary>
        /// <param name="configuration">The working hours configuration.</param>
        /// <param name="currentTime">The current time.</param>
        /// <returns>An instance of <see cref="WorkingHoursCalculation"/> containing the information about the working hours.</returns>
        /// <exception cref="ArgumentNullException"><c>calculator</c> is <c>null</c>.</exception>
        /// <exception cref="InvalidOperationException">The working hours could not be calculated.</exception>
        WorkingHoursCalculation Calculate(WorkingHoursConfiguration configuration, DateTime currentTime);
    }
}
