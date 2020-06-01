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

using System;
using System.Globalization;
using System.Windows.Forms;

namespace Yakka.Forms
{
    /// <summary>
    /// Represents the control displayed within the context menu of the application.
    /// </summary>
    public partial class ContextMenuDisplayControl : UserControl
    {
        /// <summary>
        /// Represents the working hours.
        /// </summary>
        private TimeSpan calculatedWorkingHours;

        /// <summary>
        /// Represents the break.
        /// </summary>
        private TimeSpan calculatedBreak;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContextMenuDisplayControl"/> class.
        /// </summary>
        public ContextMenuDisplayControl()
        {
            this.InitializeComponent();
            //// this.toolTip.SetToolTip(this.labelWorkingHours, "Working hours");
            //// this.toolTip.SetToolTip(this.labelBreak, "Break");

            // Without setting a MinimumSize the control won't be visible in the ToolStripControlHost (see SystemTrayIconView)
            this.MinimumSize = this.Size;
        }

        /// <summary>
        /// Occurs when the user wants to display software information.
        /// </summary>
        public event EventHandler? Info;

        /// <summary>
        /// Gets or sets the calculated working hours that shall be displayed.
        /// </summary>
        public TimeSpan CalculatedWorkingHours
        {
            get
            {
                return this.calculatedWorkingHours;
            }

            set
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() => this.calculatedWorkingHours = value));
                }
                else
                {
                    this.calculatedWorkingHours = value;
                    this.labelWorkingHours.Text = this.calculatedWorkingHours.ToString(SystemTrayIconView.TimeFormat, CultureInfo.CurrentCulture);
                }
            }
        }

        /// <summary>
        /// Gets or sets the calculated break that shall be displayed.
        /// </summary>
        public TimeSpan CalculatedBreak
        {
            get
            {
                return this.calculatedBreak;
            }

            set
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() => this.calculatedBreak = value));
                }
                else
                {
                    this.calculatedBreak = value;
                    this.labelBreak.Text = this.calculatedBreak.ToString(SystemTrayIconView.TimeFormat, CultureInfo.CurrentCulture);
                }
            }
        }

        /// <summary>
        /// Raises the <see cref="Info"/> event.
        /// </summary>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        protected virtual void OnInfo(EventArgs e)
        {
            if (this.Info != null)
            {
                this.Info.Invoke(this, e);
            }
        }

        /// <summary>
        /// Handles the <see cref="Control.Click"/> event.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        protected virtual void PictureBoxIconClick(object sender, EventArgs e)
        {
            this.OnInfo(EventArgs.Empty);
        }
    }
}
