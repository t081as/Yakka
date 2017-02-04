#region GNU General Public License 3
//  Yakka - A system tray application calculating and displaying your working hours
//  Copyright (C) 2017  Tobias Koch
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program. If not, see <http://www.gnu.org/licenses/>.
#endregion

#region Namespaces
using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
#endregion

namespace Yakka.Forms
{
    /// <summary>
    /// Represents an implementation of the <see cref="ISystemTrayIconView"/> interface using the <see cref="NotifyIcon"/> class
    /// to display a system tray icon.
    /// </summary>
    /// <remarks>
    /// see https://en.wikipedia.org/wiki/Model-view-presenter for additional information about the pattern
    /// </remarks>
    public class SystemTrayIconView : ISystemTrayIconView
    {
        #region Constants and Fields

        /// <summary>
        /// Indicates if the class has already been disposed.
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Represents the component creating the icon in the system tray.
        /// </summary>
        private NotifyIcon systemTrayIcon = null;

        #endregion

        #region Events

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemTrayIconView"/> class.
        /// </summary>
        public SystemTrayIconView()
        {
            this.systemTrayIcon = new NotifyIcon();
            this.systemTrayIcon.Text = Application.ProductName;
            this.systemTrayIcon.Icon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="SystemTrayIconView"/> class.
        /// </summary>
        ~SystemTrayIconView()
        {
            this.Dispose(false);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether the system ray icon is visible.
        /// </summary>
        public bool Visible
        {
            get
            {
                if (this.disposed)
                {
                    throw new ObjectDisposedException(GetType().Name);
                }

                return this.systemTrayIcon.Visible;
            }

            set
            {
                if (this.disposed)
                {
                    throw new ObjectDisposedException(GetType().Name);
                }

                this.systemTrayIcon.Visible = value;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">Indicates if managed resources shall also be disposed.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    try
                    {
                        if (this.systemTrayIcon != null)
                        {
                            this.systemTrayIcon.Visible = false;
                            this.systemTrayIcon.Dispose();
                            this.systemTrayIcon = null;
                        }
                    }
                    catch
                    {
                        // The Dispose method must never throw an exception
                    }
                }

                this.disposed = true;
            }
        }

        #endregion
    }
}
