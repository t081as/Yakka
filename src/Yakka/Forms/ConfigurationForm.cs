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
using System.Globalization;
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
            this.comboBoxCalculator.Items.AddRange(availableCalculators.ToArray());
            this.WorkingHoursCalculator = availableCalculators.First();

            this.Text = ConfigurationFormResources.Title;
            this.labelStartTime.Text = ConfigurationFormResources.LabelTime;
            this.labelCalculator.Text = ConfigurationFormResources.LabelCalculator;
            this.checkBoxManual.Text = ConfigurationFormResources.LabelManualBreak;
            this.buttonConfirm.Text = ConfigurationFormResources.ButtonOk;
            this.buttonApply.Text = ConfigurationFormResources.ButtonApply;
            this.buttonCancel.Text = ConfigurationFormResources.ButtonCancel;
        }

        /// <inheritdoc />
        public event EventHandler? Changed;

        /// <inheritdoc />
        public DateTime StartTime
        {
            get
            {
                return this.dateTimeStartTime.Value.WithoutSeconds();
            }

            set
            {
                this.dateTimeStartTime.Value = value;
            }
        }

        /// <inheritdoc />
        public IWorkingHoursCalculator WorkingHoursCalculator
        {
            get
            {
                return (IWorkingHoursCalculator)this.comboBoxCalculator.SelectedItem;
            }

            set
            {
                this.comboBoxCalculator.SelectedItem = value;
            }
        }

        /// <inheritdoc />
        public BreakMode BreakMode
        {
            get
            {
                return this.checkBoxManual.Checked switch
                {
                    true => BreakMode.Manual,
                    false => BreakMode.Automatic,
                };
            }

            set
            {
                this.checkBoxManual.Checked = value switch
                {
                    BreakMode.Automatic => false,
                    BreakMode.Manual => true,
                    _ => false,
                };
            }
        }

        /// <inheritdoc />
        public TimeSpan ManualBreakTime
        {
            get
            {
                string[] parts = this.maskedTextBoxManualBreak.Text.Split(":");
                return new TimeSpan(int.Parse(parts[0], CultureInfo.CurrentCulture), int.Parse(parts[1], CultureInfo.CurrentCulture), 0);
            }

            set
            {
                this.maskedTextBoxManualBreak.Text = $"{value.Hours:00}:{value.Minutes:00}";
            }
        }

        /// <summary>
        /// Triggers the <see cref="Changed"/> event.
        /// </summary>
        protected virtual void OnChanged()
        {
            this.Changed?.Invoke(this, EventArgs.Empty);
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
            this.Hide();
        }

        /// <summary>
        /// Handles the <c>Clicked</c> event.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The empty event args.</param>
        private void ButtonApply_Click(object sender, EventArgs e)
        {
            this.OnChanged();
        }

        /// <summary>
        /// Handles the <c>Clicked</c> event.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The empty event args.</param>
        private void ButtonConfirm_Click(object sender, EventArgs e)
        {
            this.OnChanged();
            this.Hide();
        }

        /// <summary>
        /// Handles the <see cref="ComboBox.SelectedIndexChanged"/> event.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The empty event args.</param>
        private void ComboBoxCalculator_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBoxCalculator.SelectedItem != null)
            {
                this.toolTip.SetToolTip(this.comboBoxCalculator, (this.comboBoxCalculator.SelectedItem as IWorkingHoursCalculator)?.Description);
            }
            else
            {
                this.toolTip.SetToolTip(this.comboBoxCalculator, string.Empty);
            }
        }
    }
}
