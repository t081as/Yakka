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
using Yakka.Forms;
#endregion

namespace Yakka.Test.Forms
{
    /// <summary>
    /// Represents a test stub implementing the <see cref="ISystemTrayIconView"/> interface.
    /// </summary>
    public class SystemTrayIconViewStub : ISystemTrayIconView
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemTrayIconViewStub"/> class.
        /// </summary>
        public SystemTrayIconViewStub()
        {
            this.Visible = false;
            this.WorkingHours = null;
            this.LastShownMessage = null;
            this.LastShownWarning = null;
        }

        #endregion

        #region Events

        /// <summary>
        /// Occurs when the user wants to edit the configuration.
        /// </summary>
        public event EventHandler Configure;

        /// <summary>
        /// Occurs when the user wants to display software information.
        /// </summary>
        public event EventHandler Info;

        /// <summary>
        /// Occurs when the user wants to quit the application.
        /// </summary>
        public event EventHandler Quit;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether the system tray icon is visible.
        /// </summary>
        public bool Visible
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the working hours information.
        /// </summary>
        public WorkingHours WorkingHours
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the last shown message.
        /// </summary>
        public string LastShownMessage
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the last shown warning.
        /// </summary>
        public string LastShownWarning
        {
            get;
            set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Shows a message to the user.
        /// </summary>
        /// <param name="message">The message that shall be shown to the user.</param>
        /// <exception cref="ArgumentNullException"><c>message</c> is <c>null</c>.</exception>
        /// <exception cref="ObjectDisposedException">The object has been disposed.</exception>
        public void ShowMessage(string message)
        {
            this.LastShownMessage = message;
        }

        /// <summary>
        /// Shows a warning message to the user.
        /// </summary>
        /// <param name="message">The warning message that shall be shown to the user.</param>
        /// <exception cref="ArgumentNullException"><c>message</c> is <c>null</c>.</exception>
        /// <exception cref="ObjectDisposedException">The object has been disposed.</exception>
        public void ShowWarning(string message)
        {
            this.LastShownWarning = message;
        }

        /// <summary>
        /// Raises the <see cref="Configure"/> event with empty <see cref="EventArgs"/>.
        /// </summary>
        public void OnConfigure()
        {
            if (this.Configure != null)
            {
                this.Configure.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Raises the <see cref="Info"/> event with empty <see cref="EventArgs"/>.
        /// </summary>
        public void OnInfo()
        {
            if (this.Info != null)
            {
                this.Info.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Raises the <see cref="Quit"/> event with empty <see cref="EventArgs"/>.
        /// </summary>
        public void OnQuit()
        {
            if (this.Quit != null)
            {
                this.Quit.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // Nothing to do in the test stub
        }

        #endregion
    }
}
