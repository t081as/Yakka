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
using System.ComponentModel;
using Yakka.Calculator;
#endregion

namespace Yakka
{
    /// <summary>
    /// Represents the configuration of the application including start of the working day and working <see cref="IWorkingHoursCalculator">hours calculator</see>.
    /// </summary>
    public class UserConfiguration : INotifyPropertyChanged
    {
        #region Constants and Fields

        /// <summary>
        /// Represents the start of the working day.
        /// </summary>
        /// <remarks>
        /// All calculations will use date and time components of the struct.
        /// </remarks>
        private DateTime start;

        /// <summary>
        /// Represents the calculator that shall be used.
        /// </summary>
        private IWorkingHoursCalculator calculator;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UserConfiguration"/> class.
        /// </summary>
        public UserConfiguration()
        {
            this.start = DateTime.Now;
            this.calculator = null;
        }

        #endregion

        #region Events

        /// <summary>
        /// Occurs when a property has been changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the date and time representing the start of the working day.
        /// </summary>
        public DateTime Start
        {
            get
            {
                return this.start;
            }

            set
            {
                this.start = value;
                this.OnPropertyChanged(new PropertyChangedEventArgs("Start"));
            }
        }

        /// <summary>
        /// Gets or sets the calculator that shall be used to calculate the working hours.
        /// </summary>
        public IWorkingHoursCalculator Calculator
        {
            get
            {
                return this.calculator;
            }

            set
            {
                this.calculator = value;
                this.OnPropertyChanged(new PropertyChangedEventArgs("Calculator"));
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="e">The event arguments containing information about the changed property.</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged.Invoke(this, e);
            }
        }

        #endregion
    }
}
