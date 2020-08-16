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
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Yakka.Calculation;

namespace Yakka.Forms
{
    /// <summary>
    /// Represents the window that allows the user to change the configuration used to calculate the working hours.
    /// </summary>
    public partial class ConfigurationForm : Form, IConfigurationView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationForm"/> class.
        /// </summary>
        /// <param name="availableCalculators">The available working hours calculatrors the user may choose from.</param>
        public ConfigurationForm(IEnumerable<IWorkingHoursCalculator> availableCalculators)
        {
            if (availableCalculators == null)
            {
                throw new ArgumentNullException(nameof(availableCalculators));
            }

            this.InitializeComponent();
            this.Icon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location);
            this.WorkingHoursCalculator = availableCalculators.First();
        }

        /// <inheritdoc />
        public event EventHandler? Confirm;

        /// <inheritdoc />
        public event EventHandler? Apply;

        /// <inheritdoc />
        public event EventHandler? Cancel;

        /// <inheritdoc />
        public DateTime StartTime { get; set; }

        /// <inheritdoc />
        public IWorkingHoursCalculator WorkingHoursCalculator { get; set; }

        /// <inheritdoc />
        public BreakMode BreakMode { get; set; }

        /// <inheritdoc />
        public TimeSpan ManualBreakTime { get; set; }

        /// <summary>
        /// Triggers the <see cref="Confirm"/> event.
        /// </summary>
        protected virtual void OnConfirm()
        {
            this.Confirm?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Triggers the <see cref="Apply"/> event.
        /// </summary>
        protected virtual void OnApply()
        {
            this.Apply?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Triggers the <see cref="Cancel"/> event.
        /// </summary>
        protected virtual void OnCancel()
        {
            this.Cancel?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Handles the <c>CheckedChanged</c> event.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The empty event args.</param>
        private void CheckBoxManual_CheckedChanged(object sender, EventArgs e)
        {
            this.maskedTextBoxManualBreak.ReadOnly = !this.checkBoxManual.Checked;
        }

        /// <summary>
        /// Handles the <c>Clicked</c> event.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The empty event args.</param>
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.OnCancel();
        }

        /// <summary>
        /// Handles the <c>Clicked</c> event.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The empty event args.</param>
        private void ButtonApply_Click(object sender, EventArgs e)
        {
            this.OnApply();
        }

        /// <summary>
        /// Handles the <c>Clicked</c> event.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The empty event args.</param>
        private void ButtonConfirm_Click(object sender, EventArgs e)
        {
            this.OnConfirm();
        }
    }
}
