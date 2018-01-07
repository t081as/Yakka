#region GNU General Public License 3
// Yakka - A system tray application calculating and displaying your working hours
// Copyright (C) 2017-2018  Tobias Koch
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
using Yakka.Calculator;
#endregion

namespace Yakka
{
    /// <summary>
    /// Provides the calculation for the working hours.
    /// </summary>
    public class WorkingHoursCalculation
    {
        #region Constants and Fields

        /// <summary>
        /// Represents the number of estimated working times that are calculated
        /// </summary>
        private const byte WORKINGTIMECALCULATIONS = 20;

        #endregion

        #region Methods

        /// <summary>
        /// Calculates the working hours using the specified <see cref="IWorkingHoursCalculator"/> and the given
        /// start of the work day.
        /// </summary>
        /// <param name="calculator">The calculator that shall be used.</param>
        /// <param name="startOfWorkDay">The start of the work day.</param>
        /// <param name="currentTime">The current time.</param>
        /// <returns>An instance of <see cref="WorkingHours"/> containing the information about the working hours.</returns>
        /// <exception cref="ArgumentNullException"><c>calculator</c> is <c>null</c>.</exception>
        /// <exception cref="InvalidOperationException">The working hours could not be calculated.</exception>
        public WorkingHours Calculate(IWorkingHoursCalculator calculator, DateTime startOfWorkDay, DateTime currentTime)
        {
            if (calculator == null)
            {
                throw new ArgumentNullException("calculator");
            }

            TimeSpan currentWorkingHours = calculator.CalculateWorkingHours(startOfWorkDay, currentTime);
            TimeSpan currentBreak = calculator.CalculateBreak(startOfWorkDay, currentTime);

            Dictionary<byte, DateTime> endOfWorkDayEstimations = new Dictionary<byte, DateTime>();

            for (byte hoursWorked = 0; hoursWorked < WORKINGTIMECALCULATIONS; hoursWorked++)
            {
                DateTime specificEndOfWorkDay = startOfWorkDay;
                TimeSpan workedTimeSpan = new TimeSpan(0);

                while (workedTimeSpan.TotalHours < hoursWorked)
                {
                    specificEndOfWorkDay = specificEndOfWorkDay.AddMinutes(1);
                    workedTimeSpan = calculator.CalculateWorkingHours(startOfWorkDay, specificEndOfWorkDay);
                }

                endOfWorkDayEstimations.Add(hoursWorked, specificEndOfWorkDay);
            }

            WorkingHours workingHours = new WorkingHours();
            workingHours.Start = startOfWorkDay;
            workingHours.CalculatedWorkingHours = currentWorkingHours;
            workingHours.CalculatedBreak = currentBreak;
            workingHours.EndOfWorkDay = endOfWorkDayEstimations;

            return workingHours;
        }

        #endregion
    }
}
