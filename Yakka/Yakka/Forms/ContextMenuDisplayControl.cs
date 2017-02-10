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
using System.Globalization;
using System.Windows.Forms;
#endregion

namespace Yakka.Forms
{
    /// <summary>
    /// Represents the control displayed within the context menu of the application.
    /// </summary>
    public partial class ContextMenuDisplayControl : UserControl
    {
        #region Constants and Fields

        /// <summary>
        /// Represents the working hours.
        /// </summary>
        private TimeSpan calculatedWorkingHours;

        /// <summary>
        /// Represents the break.
        /// </summary>
        private TimeSpan calculatedBreak;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ContextMenuDisplayControl"/> class.
        /// </summary>
        public ContextMenuDisplayControl()
        {
            this.InitializeComponent();

            // Without setting a MinimumSize the control won't be visible in the ToolStripControlHost (see SystemTrayIconView)
            this.MinimumSize = this.Size;
        }

        #endregion

        #region Properties

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
                    this.Invoke(new Action(() => this.CalculatedWorkingHours = value));
                }
                else
                {
                    this.calculatedWorkingHours = value;
                    this.labelWorkingHours.Text = this.calculatedWorkingHours.ToString(SystemTrayIconView.TIMEFORMAT, CultureInfo.CurrentUICulture);
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
                    this.Invoke(new Action(() => this.CalculatedBreak = value));
                }
                else
                {
                    this.calculatedBreak = value;
                    this.labelBreak.Text = this.calculatedBreak.ToString(SystemTrayIconView.TIMEFORMAT, CultureInfo.CurrentUICulture);
                }
            }
        }

        #endregion
    }
}
