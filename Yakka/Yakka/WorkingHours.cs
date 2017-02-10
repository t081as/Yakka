#region GNU General Public License 3
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
using System.Collections.Generic;
#endregion

namespace Yakka
{
    /// <summary>
    /// Contains information about the working hours for the displays.
    /// </summary>
    public class WorkingHours
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkingHours"/> class.
        /// </summary>
        public WorkingHours()
        {
            this.Start = DateTime.Now;
            this.CalculatedWorkingHours = new TimeSpan(0);
            this.CalculatedBreak = new TimeSpan(0);
            this.EndOfWorkDay = new Dictionary<byte, DateTime>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the date and time representing the start of the working day.
        /// </summary>
        public DateTime Start
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the calculated working hours.
        /// </summary>
        public TimeSpan CalculatedWorkingHours
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the calculated break.
        /// </summary>
        public TimeSpan CalculatedBreak
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a dictionary containing calculated times for the end of the work day dependent on the desired
        /// working hours.
        /// </summary>
        public IReadOnlyDictionary<byte, DateTime> EndOfWorkDay
        {
            get;
            set;
        }

        #endregion
    }
}
