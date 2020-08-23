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

namespace Yakka.Forms
{
    /// <summary>
    /// Represents the presenter managing the detail view.
    /// </summary>
    public class DetailPresenter
    {
        /// <summary>
        /// The view managed by this instance.
        /// </summary>
        private IDetailView view;

        /// <summary>
        /// Initializes a new instance of the <see cref="DetailPresenter"/> class.
        /// </summary>
        /// <param name="view">The view managed by this instance.</param>
        public DetailPresenter(IDetailView view)
        {
            this.view = view;
        }

        /// <summary>
        /// Gets or sets the working hours calculation result.
        /// </summary>
        /// <value>The working hours calculation result.</value>
        public WorkingHoursCalculation WorkingHoursCalculation
        {
            get => this.view.WorkingHoursCalculation;
            set => this.view.WorkingHoursCalculation = value;
        }
    }
}
