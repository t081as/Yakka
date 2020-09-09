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

namespace Yakka.Forms
{
    /// <summary>
    /// Represents the presenter managing an <see cref="IConfigurationView"/>.
    /// </summary>
    public class ConfigurationPresenter
    {
        /// <summary>
        /// Represents the reference to the view managed by this presenter.
        /// </summary>
        private IConfigurationView view;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationPresenter"/> class.
        /// </summary>
        /// <param name="view">The reference to the view that shall be managed.</param>
        /// <exception cref="ArgumentNullException"><c>view</c> is <c>null</c>.</exception>
        public ConfigurationPresenter(IConfigurationView view)
        {
            this.view = view ?? throw new ArgumentNullException(nameof(view));

            this.view.Changed += this.View_Changed;
        }

        /// <summary>
        /// Occurs when the configuration has been changed.
        /// </summary>
        public event EventHandler<WorkingHoursConfigurationEventArgs>? ConfigurationChanged;

        /// <summary>
        /// Updates the attached view using the specified <paramref name="configuration"/>.
        /// </summary>
        /// <param name="configuration">The configuration that shall be used to update the attached view.</param>
        /// <exception cref="ArgumentNullException"><c>configuration</c> is <c>null</c>.</exception>
        public void UpdateView(WorkingHoursConfiguration configuration)
        {
            this.view.StartTime = configuration?.StartTime ?? throw new ArgumentNullException(nameof(configuration));
            this.view.WorkingHoursCalculator = configuration.WorkingHoursCalculator;
            this.view.BreakMode = configuration.BreakMode;
            this.view.ManualBreakTime = configuration.ManualBreakTime;
        }

        /// <summary>
        /// Triggers the <see cref="ConfigurationChanged"/> event.
        /// </summary>
        /// <param name="e">A <see cref="WorkingHoursConfigurationEventArgs"/> containing the updated configuration.</param>
        protected virtual void OnConfigurationChanged(WorkingHoursConfigurationEventArgs e)
        {
            this.ConfigurationChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Handles the <see cref="IConfigurationView.Changed"/> event.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The empty event args.</param>
        private void View_Changed(object? sender, EventArgs e)
        {
            var newConfiguration = new WorkingHoursConfiguration
            {
                StartTime = this.view.StartTime,
                WorkingHoursCalculator = this.view.WorkingHoursCalculator,
                BreakMode = this.view.BreakMode,
                ManualBreakTime = this.view.ManualBreakTime,
            };

            this.OnConfigurationChanged(new WorkingHoursConfigurationEventArgs(newConfiguration));
        }
    }
}
