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
using System.Linq;
using Yakka.Calculation;

namespace Yakka
{
    /// <summary>
    /// Encapsulates the current working hours configuration.
    /// </summary>
    public class WorkingHoursConfiguration
    {
        /// <summary>
        /// Gets or sets the start date and time of the current working day.
        /// </summary>
        /// <value>The start date and time of the current working day.</value>
        public DateTime StartTime { get; set; } = DateTime.Now;

        /// <summary>
        /// Gets or sets the implementation of the <see cref="IWorkingHoursCalculator"/> interface
        /// that shall be used to calculate.
        /// </summary>
        /// <value>
        /// The implementation of the <see cref="IWorkingHoursCalculator"/> interface
        /// that shall be used to calculate.
        /// </value>
        public IWorkingHoursCalculator WorkingHoursCalculator { get; set; } = new NoBreakWorkingHoursCalculator();

        /// <summary>
        /// Gets or sets the break mode configured by the user.
        /// </summary>
        /// <value>The break mode configured by the user.</value>
        public BreakMode BreakMode { get; set; } = BreakMode.Automatic;

        /// <summary>
        /// Gets or sets the manual break configured by the user.
        /// </summary>
        /// <value>The manual break configured by the user.</value>
        /// <remarks>
        /// This value will only be used if <see cref="BreakMode"/> is set to <see cref="BreakMode.Manual"/>.
        /// </remarks>
        public TimeSpan ManualBreakTime { get; set; } = TimeSpan.FromSeconds(0);

        /// <summary>
        /// Saves the specified <paramref name="configuration"/>.
        /// </summary>
        /// <param name="configuration">The configuration that shall be saved.</param>
        /// <exception cref="ArgumentNullException"><c>configuration</c> is <c>null</c>.</exception>
        public static void Save(WorkingHoursConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            Properties.Settings.Default.StartTime = configuration.StartTime;
            Properties.Settings.Default.WorkingHoursCalculator = configuration.WorkingHoursCalculator?.Id ?? Guid.Empty;
            Properties.Settings.Default.BreakMode = (byte)configuration.BreakMode;
            Properties.Settings.Default.ManualBreakTime = configuration.ManualBreakTime;

            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Loads the saved <see cref="WorkingHoursConfiguration"/>.
        /// </summary>
        /// <returns>A <see cref="WorkingHoursConfiguration"/> that has been loaded.</returns>
        public static WorkingHoursConfiguration Load()
        {
            var result = new WorkingHoursConfiguration();
            result.StartTime = Properties.Settings.Default.StartTime;
            result.WorkingHoursCalculator = WorkingHoursCalculators.All.FirstOrDefault(c => c.Id == Properties.Settings.Default.WorkingHoursCalculator) ?? WorkingHoursCalculators.Default;
            result.BreakMode = Enum.IsDefined(typeof(BreakMode), Properties.Settings.Default.BreakMode)
                ? (BreakMode)Properties.Settings.Default.BreakMode
                : BreakMode.Automatic;
            result.ManualBreakTime = Properties.Settings.Default.ManualBreakTime;

            return result;
        }
    }
}