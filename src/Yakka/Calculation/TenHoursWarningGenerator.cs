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
    /// An implementation of the <see cref="IWarningGenerator"/> interface generating warnings if the
    /// user is close to ten working hours.
    /// </summary>
    public class TenHoursWarningGenerator : IWarningGenerator
    {
        /// <inheritdoc />
        public string? GetWarning(TimeSpan workTimeSpan, TimeSpan breakTimeSpan)
        {
            if (workTimeSpan.Hours >= 10)
            {
                return TenHoursWarningGeneratorResources.Error;
            }
            else if (workTimeSpan.Hours == 9 && workTimeSpan.Minutes > 20)
            {
                return TenHoursWarningGeneratorResources.Warning;
            }
            else
            {
                return null;
            }
        }
    }
}
