﻿#region GNU General Public License 3
// Yakka - A system tray application calculating and displaying your working hours
// Copyright (C) 2017  Tobias Koch
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
#endregion

namespace Yakka.IO
{
    /// <summary>
    /// Represents an implementation of the <see cref="IStartTimeProvider"/> interface returning the current time.
    /// </summary>
    public class CurrentTimeProvider : IStartTimeProvider
    {
        /// <summary>
        /// Gets the start time of the working day.
        /// </summary>
        public DateTime? StartTime
        {
            get
            {
                return DateTime.Now;
            }
        }
    }
}
