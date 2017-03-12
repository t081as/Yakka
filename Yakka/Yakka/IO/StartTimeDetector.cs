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

namespace Yakka.IO
{
    /// <summary>
    /// Configures the start time of the working day using implementations of the <see cref="IStartTimeProvider"/> interface.
    /// </summary>
    public class StartTimeDetector
    {
        #region Constants and Fields

        /// <summary>
        /// Represents the reference to the configuration object that contains the start time of the working day.
        /// </summary>
        private UserConfiguration configuration;

        /// <summary>
        /// Represents the implementations used to retrieve the start time of the working day.
        /// </summary>
        private List<IStartTimeProvider> startTimeProviders;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StartTimeDetector"/> class.
        /// </summary>
        /// <param name="configuration">A <see cref="UserConfiguration"/> that contains the start time of the working day.</param>
        public StartTimeDetector(UserConfiguration configuration)
            : this()
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StartTimeDetector"/> class.
        /// </summary>
        private StartTimeDetector()
        {
            this.startTimeProviders = new List<IStartTimeProvider>();

            this.startTimeProviders.Add(new CurrentTimeProvider());
        }

        #endregion#

        #region Methods

        /// <summary>
        /// Sets the start time of the given user configuration using implementations of the <see cref="IStartTimeProvider"/>
        /// interface.
        /// </summary>
        public void SetStartTime()
        {
            /*
             * Upon launch the application will load the last saved start time from the storage.
             *
             * Before trying to guess the start time of the working day the application shall check
             * if the start time is already accurate (because this could also be a simple application or
             * operating system restart).
             */
            if (!this.IsToday(this.configuration.Start))
            {
                foreach (IStartTimeProvider provider in this.startTimeProviders)
                {
                    DateTime? startTime = provider.StartTime;

                    if (startTime != null && this.IsToday(startTime.Value))
                    {
                        this.configuration.Start = startTime.Value;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Checks if the date component of the given <see cref="DateTime"/> equals the current date.
        /// </summary>
        /// <param name="date">The date that shall be checked.</param>
        /// <returns><c>True</c> if the date equals the current date, otherwise <c>false</c>.</returns>
        private bool IsToday(DateTime date)
        {
            return date.Year == DateTime.Today.Year &&
                date.Month == DateTime.Today.Month &&
                date.Day == DateTime.Today.Day;
        }

        #endregion
    }
}
