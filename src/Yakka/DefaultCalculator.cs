﻿// Yakka - A system tray application calculating and displaying your working hours
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
using Yakka.Calculation;

namespace Yakka
{
    /// <summary>
    /// Contains methods to calculate the working hours and the break.
    /// </summary>
    public class DefaultCalculator : ICalculator
    {
        /// <inheritdoc />
        public WorkingHoursCalculation Calculate(WorkingHoursConfiguration configuration, DateTime currentTime)
        {
            var result = new WorkingHoursCalculation(configuration ?? throw new ArgumentNullException(nameof(configuration)));
            result.Configuration = configuration;

            if (configuration.BreakMode == BreakMode.Automatic)
            {
                (result.CalculatedWorkingHours, result.CalculatedBreak, result.Warning) = configuration.WorkingHoursCalculator.Calculate(configuration.StartTime, currentTime);
            }
            else if (configuration.BreakMode == BreakMode.Manual)
            {
                (_, _, string? warning) = configuration.WorkingHoursCalculator.Calculate(configuration.StartTime, currentTime);

                result.CalculatedBreak = configuration.ManualBreakTime;
                result.CalculatedWorkingHours = currentTime - configuration.StartTime - result.CalculatedBreak;
                result.Warning = warning;
            }
            else
            {
                throw new InvalidOperationException();
            }

            for (byte hoursWorked = 0; hoursWorked < 12; hoursWorked++)
            {
                DateTime specificEndOfWorkDay = configuration.StartTime;
                TimeSpan workedTimeSpan = new TimeSpan(0);

                while (workedTimeSpan.TotalHours < hoursWorked)
                {
                    specificEndOfWorkDay = specificEndOfWorkDay.AddMinutes(1);
                    (workedTimeSpan, _, _) = configuration.WorkingHoursCalculator.Calculate(configuration.StartTime, specificEndOfWorkDay);
                }

                result.FullHoursWorked.Add(hoursWorked, specificEndOfWorkDay);
            }

            return result;
        }
    }
}
