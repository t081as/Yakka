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

namespace Yakka.StartTime
{
    /// <summary>
    /// Describes objects able to provide a possible start time of the working day.
    /// </summary>
    public interface IStartTimeProvider
    {
        /// <summary>
        /// Gets the start time of the working day.
        /// </summary>
        /// <value>The start time of the working day or <c>null</c> if the start time could not be determined.</value>
        DateTime? StartTime { get; }
    }
}
