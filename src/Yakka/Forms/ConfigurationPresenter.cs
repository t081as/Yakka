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

namespace Yakka.Forms
{
    /// <summary>
    /// Represents the presenter managing a <see cref="IConfigurationView"/>.
    /// </summary>
    public class ConfigurationPresenter
    {
        /// <summary>
        /// Represents the reference to the view managed by this presenter.
        /// </summary>
        private IConfigurationView view;

        /// <summary>
        /// Represents the working hours configuration.
        /// </summary>
        private WorkingHoursConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationPresenter"/> class.
        /// </summary>
        /// <param name="view">The reference to the view that shall be managed.</param>
        /// <param name="configuration">The working hours configuration.</param>
        public ConfigurationPresenter(IConfigurationView view, WorkingHoursConfiguration configuration)
        {
            this.view = view ?? throw new ArgumentNullException(nameof(view));
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(view));

            this.view.Changed += this.View_Changed;
        }

        /// <summary>
        /// Handles the <see cref="IConfigurationView.Changed"/> event.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The empty event args.</param>
        private void View_Changed(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
