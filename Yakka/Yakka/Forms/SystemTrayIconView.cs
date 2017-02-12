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
using System.Drawing;
using System.Reflection;
using System.Text;
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
        /// Represents the string used to format the displayed times.
        /// </summary>
        public const string TIMEFORMAT = "hh\\:mm";

        /// <summary>
        /// Represents the message visibility time in milliseconds.
        /// </summary>
        private const int MESSAGETIME = 5000;

        /// <summary>
        /// Indicates if the class has already been disposed.
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Represents the working hours information.
        /// </summary>
        private WorkingHours workingHours;

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
        /// Represents the control shown in the context menu.
        /// </summary>
        private ContextMenuDisplayControl contextMenuControl = null;

        /// <summary>
        /// Represents the message shown on left-clicking the <see cref="NotifyIcon"/>.
        /// </summary>
        private string quickMessage;

        /// <summary>
        /// Represents the object used to lock the <see cref="quickMessage"/> property.
        /// </summary>
        private object quickMessageLock = new object();

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemTrayIconView"/> class.
        /// </summary>
        public SystemTrayIconView()
        {
            this.workingHours = null;
            this.contextMenuControl = new ContextMenuDisplayControl();
            this.contextMenuControl.Info += this.ContextMenuControl_Info;

            this.systemTrayIcon = new NotifyIcon();
            this.systemTrayIcon.Text = Application.ProductName;
            this.systemTrayIcon.Icon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location);
            this.systemTrayIcon.MouseUp += this.SystemTrayIcon_MouseUp;

            this.configurationMenuItem = new ToolStripMenuItem("Configuration");
            this.configurationMenuItem.Click += this.ConfigurationMenuItem_Click;

            this.aboutMenuItem = new ToolStripMenuItem("About");
            this.aboutMenuItem.Click += this.AboutMenuItem_Click;

            this.quitMenuItem = new ToolStripMenuItem("Quit");
            this.quitMenuItem.Click += this.QuitMenuItem_Click;

            ToolStripControlHost controlHost = new ToolStripControlHost(this.contextMenuControl);
            controlHost.Width = this.contextMenuControl.Width;
            controlHost.Height = this.contextMenuControl.Height;

            this.systemTrayIcon.ContextMenuStrip = new ContextMenuStrip();
            this.systemTrayIcon.ContextMenuStrip.Items.Add(controlHost);
            this.systemTrayIcon.ContextMenuStrip.Items.Add(this.configurationMenuItem);
            this.systemTrayIcon.ContextMenuStrip.Items.Add(this.aboutMenuItem);
            this.systemTrayIcon.ContextMenuStrip.Items.Add(new ToolStripSeparator());
            this.systemTrayIcon.ContextMenuStrip.Items.Add(this.quitMenuItem);
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
        /// Gets or sets a value indicating whether the system tray icon is visible.
        /// </summary>
        public bool Visible
        {
            get
            {
                if (this.disposed)
                {
                    throw new ObjectDisposedException(this.GetType().Name);
                }

                return this.systemTrayIcon.Visible;
            }

            set
            {
                if (this.disposed)
                {
                    throw new ObjectDisposedException(this.GetType().Name);
                }

                this.systemTrayIcon.Visible = value;
            }
        }

        /// <summary>
        /// Gets or sets the working hours information.
        /// </summary>
        public WorkingHours WorkingHours
        {
            get
            {
                if (this.disposed)
                {
                    throw new ObjectDisposedException(this.GetType().Name);
                }

                return this.workingHours;
            }

            set
            {
                if (this.disposed)
                {
                    throw new ObjectDisposedException(this.GetType().Name);
                }

                this.workingHours = value;

                if (this.workingHours != null)
                {
                    this.Update();
                }
            }
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
            if (this.disposed)
            {
                throw new ObjectDisposedException(this.GetType().Name);
            }

            if (message == null)
            {
                throw new ArgumentNullException("message");
            }

            if (this.systemTrayIcon.Visible)
            {
                this.systemTrayIcon.BalloonTipIcon = ToolTipIcon.Info;
                this.systemTrayIcon.BalloonTipTitle = Application.ProductName;
                this.systemTrayIcon.BalloonTipText = message;

                this.systemTrayIcon.ShowBalloonTip(MESSAGETIME);
            }
        }

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

                        if (this.contextMenuControl != null)
                        {
                            this.contextMenuControl.Dispose();
                            this.contextMenuControl = null;
                        }

                        if (this.systemTrayIcon != null)
                        {
                            if (this.systemTrayIcon.ContextMenuStrip != null)
                            {
                                this.systemTrayIcon.ContextMenuStrip.Dispose();
                                this.systemTrayIcon.ContextMenuStrip = null;
                            }

                            this.systemTrayIcon.Visible = false;
                            this.systemTrayIcon.MouseUp -= this.SystemTrayIcon_MouseUp;
                            this.systemTrayIcon.Dispose();
                            this.systemTrayIcon = null;
                        }
                    }
                    catch
                    {
                        // The Dispose method must never throw exceptions
                    }
                }

                this.disposed = true;
            }
        }

        /// <summary>
        /// Updates all view-related elements.
        /// </summary>
        protected virtual void Update()
        {
            this.contextMenuControl.CalculatedWorkingHours = this.workingHours.CalculatedWorkingHours;
            this.contextMenuControl.CalculatedBreak = this.workingHours.CalculatedBreak;
            this.systemTrayIcon.Text = $"{Application.ProductName}\n\n{this.workingHours.Start.ToShortTimeString()} (Start)\n{this.workingHours.CalculatedWorkingHours.ToString(TIMEFORMAT)} (Working hours)\n{this.workingHours.CalculatedBreak.ToString(TIMEFORMAT)} (Break)";

            lock (this.quickMessageLock)
            {
                this.quickMessage = this.systemTrayIcon.Text;
            }
        }

        /// <summary>
        /// Handles the mouse up event of the <see cref="NotifyIcon"/>.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments containing information about the click.</param>
        protected virtual void SystemTrayIcon_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                lock (this.quickMessageLock)
                {
                    this.ShowMessage(this.quickMessage);
                }
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
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        protected virtual void ConfigurationMenuItem_Click(object sender, EventArgs e)
        {
            this.OnConfigure(EventArgs.Empty);
        }

        /// <summary>
        /// Handles the <see cref="ContextMenuDisplayControl.Info"/> event.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        protected virtual void ContextMenuControl_Info(object sender, EventArgs e)
        {
            this.OnInfo(EventArgs.Empty);
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
