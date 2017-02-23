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
using System.Windows.Forms;
using Yakka.Calculator;
#endregion

namespace Yakka.Forms
{
    /// <summary>
    /// Represents the window that allows the user to change the configuration used to calculate the working hours.
    /// </summary>
    public partial class ConfigurationForm : Form, IConfigurationView
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationForm"/> class.
        /// </summary>
        public ConfigurationForm()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Events

        /// <summary>
        /// Occurs when the configuration is changed by the user.
        /// </summary>
        public event EventHandler ConfigurationChanged;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the start time of the working hours.
        /// </summary>
        public DateTime Start
        {
            get
            {
                return this.dateTimePickerStart.Value;
            }

            set
            {
                this.dateTimePickerStart.Value = value;
            }
        }

        /// <summary>
        /// Gets or sets the calculator choosen by the user.
        /// </summary>
        public IWorkingHoursCalculator SelectedCalculator
        {
            get
            {
                return (IWorkingHoursCalculator)this.comboBoxCalculator.SelectedItem;
            }

            set
            {
                foreach (object item in this.comboBoxCalculator.Items)
                {
                    if (item.GetType() == value.GetType())
                    {
                        this.comboBoxCalculator.SelectedItem = item;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the calculators that are available and could be chosen by the user.
        /// </summary>
        public IWorkingHoursCalculator[] AvailableCalculators
        {
            get
            {
                List<IWorkingHoursCalculator> calculators = new List<IWorkingHoursCalculator>();

                foreach (object item in this.comboBoxCalculator.Items)
                {
                    calculators.Add((IWorkingHoursCalculator)item);
                }

                return calculators.ToArray();
            }

            set
            {
                this.comboBoxCalculator.Items.Clear();
                this.comboBoxCalculator.Items.AddRange(value);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles the <see cref="Form.FormClosing"/> event.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">A <see cref="FormClosedEventArgs"/> that contains the data.</param>
        private void ConfigurationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK)
            {
                this.ConfigurationChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        #endregion
    }
}
