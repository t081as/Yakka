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
using System.Collections.Generic;

namespace Yakka.StartTime
{
    /// <summary>
    /// Configures the start time of the working day using implementations of the <see cref="IStartTimeProvider"/> interface.
    /// </summary>
    public class StartTimeDetector
    {
        /// <summary>
        /// Represents the implementations used to retrieve the start time of the working day.
        /// </summary>
        private List<IStartTimeProvider> startTimeProviders;

        /// <summary>
        /// Initializes a new instance of the <see cref="StartTimeDetector"/> class.
        /// </summary>
        private StartTimeDetector()
        {
            this.startTimeProviders = new List<IStartTimeProvider>();

            this.startTimeProviders.Add(new StoredStartTimeProvider());
            this.startTimeProviders.Add(new OperatingSystemStartTimeProvider());
            this.startTimeProviders.Add(new CurrentStartTimeProvider());
        }

        /// <summary>
        /// Gets the start date and time of the working day.
        /// </summary>
        /// <value>The start date and time of the working day.</value>
        public DateTime StartTime
        {
            get
            {
                DateTime? startTime = null;

                foreach (var provider in this.startTimeProviders)
                {
                    startTime = provider.StartTime;

                    if (startTime != null && DateTime.Now - startTime < TimeSpan.FromHours(12))
                    {
                        break;
                    }
                }

                return startTime ?? DateTime.Now;
            }
        }
    }
}