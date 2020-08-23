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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Yakka.Forms
{
    /// <summary>
    /// Represents the window that displays details of the working hours calculation.
    /// </summary>
    public partial class DetailForm : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DetailForm"/> class.
        /// </summary>
        public DetailForm()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Handles the click event.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The empty event arguments.</param>
        private void DetailForm_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        /// <summary>
        /// Handles the shown event.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The empty event arguments.</param>
        private void DetailForm_Shown(object sender, EventArgs e)
        {
            this.Left = Screen.PrimaryScreen.Bounds.X;
            this.Top = Screen.PrimaryScreen.Bounds.Y;
            this.Width = Screen.PrimaryScreen.Bounds.Width;
            this.Height = Screen.PrimaryScreen.Bounds.Height;
        }

        /// <summary>
        /// Handles the resize event.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The empty event arguments.</param>
        private void DetailForm_Resize(object sender, EventArgs e)
        {
            this.panelDetails.Left = (this.Width - this.panelDetails.Width) / 2;
            this.panelDetails.Top = (this.Height - this.panelDetails.Height) / 2;
        }
    }
}
