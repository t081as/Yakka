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

namespace Yakka
{
    /// <summary>
    /// Enumerates the possible break modes.
    /// </summary>
    public enum BreakMode : int
    {
        /// <summary>
        /// The break time configured by the user will be used.
        /// </summary>
        Manual = 0,

        /// <summary>
        /// The break time calculated by the software will be used.
        /// </summary>
        Automatic = 1,
    }
}
