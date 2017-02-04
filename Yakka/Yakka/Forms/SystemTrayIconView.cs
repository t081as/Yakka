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
using System.ComponentModel;
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

        /// <summary>
        /// Represents the context menu item "Configuration".
        /// </summary>
        private ToolStripMenuItem configurationMenuItem;

        /// <summary>
        /// Represents the context menu item "About".
        /// </summary>
        private ToolStripMenuItem aboutMenuItem;

        /// <summary>
        /// Represents the context menu item "Quit".
        /// </summary>
        private ToolStripMenuItem quitMenuItem;

        /// <summary>
        /// Represents the configuration containing the start of the working day and the calculator that shall be used.
        /// </summary>
        private UserConfiguration configuration;

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

            this.configurationMenuItem = new ToolStripMenuItem("Configuration");
            this.configurationMenuItem.Click += this.ConfigurationMenuItem_Click;

            this.aboutMenuItem = new ToolStripMenuItem("About");
            this.aboutMenuItem.Click += this.AboutMenuItem_Click;

            this.quitMenuItem = new ToolStripMenuItem("Quit");
            this.quitMenuItem.Click += this.QuitMenuItem_Click;

            this.systemTrayIcon.ContextMenuStrip = new ContextMenuStrip();
            this.systemTrayIcon.ContextMenuStrip.Items.Add(this.configurationMenuItem);
            this.systemTrayIcon.ContextMenuStrip.Items.Add(this.aboutMenuItem);
            this.systemTrayIcon.ContextMenuStrip.Items.Add(new ToolStripSeparator());
            this.systemTrayIcon.ContextMenuStrip.Items.Add(this.quitMenuItem);

            this.configuration = null;
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="SystemTrayIconView"/> class.
        /// </summary>
        ~SystemTrayIconView()
        {
            this.Dispose(false);
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

        /// <summary>
        /// Gets or sets the configuration containing the start of the working day and the calculator that shall be used.
        /// </summary>
        public UserConfiguration Configuration
        {
            get
            {
                return this.configuration;
            }

            set
            {
                if (this.configuration != null)
                {
                    this.configuration.PropertyChanged -= this.Configuration_PropertyChanged;
                }

                this.configuration = value;

                if (this.configuration != null)
                {
                    this.configuration.PropertyChanged += this.Configuration_PropertyChanged;
                }
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
        /// <param name="disposing">Indicates whether managed resources shall also be disposed.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    try
                    {
                        if (this.configurationMenuItem != null)
                        {
                            this.configurationMenuItem.Click -= this.ConfigurationMenuItem_Click;
                            this.configurationMenuItem.Dispose();
                            this.configurationMenuItem = null;
                        }

                        if (this.aboutMenuItem != null)
                        {
                            this.aboutMenuItem.Click -= this.AboutMenuItem_Click;
                            this.aboutMenuItem.Dispose();
                            this.aboutMenuItem = null;
                        }

                        if (this.quitMenuItem != null)
                        {
                            this.quitMenuItem.Click -= this.QuitMenuItem_Click;
                            this.quitMenuItem.Dispose();
                            this.quitMenuItem = null;
                        }

                        if (this.systemTrayIcon != null)
                        {
                            if (this.systemTrayIcon.ContextMenuStrip != null)
                            {
                                this.systemTrayIcon.ContextMenuStrip.Dispose();
                                this.systemTrayIcon.ContextMenuStrip = null;
                            }

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

        /// <summary>
        /// Handles the click event of the specific menu item.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The empty event arguments.</param>
        protected virtual void QuitMenuItem_Click(object sender, EventArgs e)
        {
            this.OnQuit(EventArgs.Empty);
        }

        /// <summary>
        /// Handles the click event of the specific menu item.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The empty event arguments.</param>
        protected virtual void AboutMenuItem_Click(object sender, EventArgs e)
        {
            this.OnInfo(EventArgs.Empty);
        }

        /// <summary>
        /// Handles the click event of the specific menu item.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The empty event arguments.</param>
        protected virtual void ConfigurationMenuItem_Click(object sender, EventArgs e)
        {
            this.OnConfigure(EventArgs.Empty);
        }

        /// <summary>
        /// Handles changes of the user configuration.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments containing information about the changed property.</param>
        protected virtual void Configuration_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Raises the <see cref="Configure"/> event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnConfigure(EventArgs e)
        {
            if (this.Configure != null)
            {
                this.Configure.Invoke(this, e);
            }
        }

        /// <summary>
        /// Raises the <see cref="Info"/> event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnInfo(EventArgs e)
        {
            if (this.Info != null)
            {
                this.Info.Invoke(this, e);
            }
        }

        /// <summary>
        /// Raises the <see cref="Quit"/> event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnQuit(EventArgs e)
        {
            if (this.Quit != null)
            {
                this.Quit.Invoke(this, e);
            }
        }

        #endregion
    }
}
