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
using System.Linq;
using System.Reflection;

namespace Yakka.Calculation
{
    /// <summary>
    /// Allows to access implementations of the <see cref="IWorkingHoursCalculator"/> interface.
    /// </summary>
    public static class WorkingHoursCalculators
    {
        /// <summary>
        /// Initializes static members of the <see cref="WorkingHoursCalculators"/> class.
        /// </summary>
        static WorkingHoursCalculators()
        {
            var instances = new List<IWorkingHoursCalculator>();
            var types = Assembly.GetExecutingAssembly().GetTypes().Where(t => typeof(IWorkingHoursCalculator).IsAssignableFrom(t) && t.IsClass);

            foreach (var type in types)
            {
                if (Activator.CreateInstance(type) is IWorkingHoursCalculator currentCalculator)
                {
                    instances.Add(currentCalculator);
                }
            }

            All = instances;
        }

        /// <summary>
        /// Gets all available implementations of the <see cref="IWorkingHoursCalculator"/> interface.
        /// </summary>
        /// <value>All available implementations of the <see cref="IWorkingHoursCalculator"/> interface.</value>
        public static IEnumerable<IWorkingHoursCalculator> All { get; }

        /// <summary>
        /// Gets the default implementation of the <see cref="IWorkingHoursCalculator"/> interface.
        /// </summary>
        /// <value>The default implementation of the <see cref="IWorkingHoursCalculator"/> interface.</value>
        public static IWorkingHoursCalculator Default => new NoBreakWorkingHoursCalculator();
    }
}
