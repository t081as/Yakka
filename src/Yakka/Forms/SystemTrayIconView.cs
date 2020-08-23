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
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;

namespace Yakka.Forms
{
    /// <summary>
    /// Represents an implementation of the <see cref="ISystemTrayIconView"/> interface using the <see cref="NotifyIcon"/> class
    /// to display a system tray icon.
    /// </summary>
    public class SystemTrayIconView : ISystemTrayIconView
    {
        /// <summary>
        /// Represents the string used to format the displayed times.
        /// </summary>
        public const string TimeFormat = "hh\\:mm";

        /// <summary>
        /// Represents the message visibility time in milliseconds.
        /// </summary>
        private const int MessageTime = 5000;

        /// <summary>
        /// Indicates if the class has already been disposed.
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Represents the working hours information.
        /// </summary>
        private WorkingHoursCalculation workingHoursCalculation = new WorkingHoursCalculation();

        /// <summary>
        /// Represents the component creating the icon in the system tray.
        /// </summary>
        private NotifyIcon systemTrayIcon;

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
        private ContextMenuDisplayControl contextMenuControl;

        /// <summary>
        /// Represents the message shown on left-clicking the <see cref="NotifyIcon"/>.
        /// </summary>
        private string quickMessage = string.Empty;

        /// <summary>
        /// Represents the object used to lock the <see cref="quickMessage"/> property.
        /// </summary>
        private object quickMessageLock = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemTrayIconView"/> class.
        /// </summary>
        public SystemTrayIconView()
        {
            this.contextMenuControl = new ContextMenuDisplayControl();
            this.contextMenuControl.Info += this.ContextMenuControlInfo;

            this.systemTrayIcon = new NotifyIcon();
            this.systemTrayIcon.Text = Application.ProductName;
            this.systemTrayIcon.Icon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location);
            this.systemTrayIcon.MouseUp += this.SystemTrayIconMouseUp;

            this.configurationMenuItem = new ToolStripMenuItem(SystemTrayIconViewResources.ConfigurationText);
            this.configurationMenuItem.Click += this.ConfigurationMenuItemClick;

            this.aboutMenuItem = new ToolStripMenuItem(SystemTrayIconViewResources.AboutText);
            this.aboutMenuItem.Click += this.AboutMenuItemClick;

            this.quitMenuItem = new ToolStripMenuItem(SystemTrayIconViewResources.QuitText);
            this.quitMenuItem.Click += this.QuitMenuItemClick;

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

        /// <inheritdoc />
        public event EventHandler? Configure;

        /// <inheritdoc />
        public event EventHandler? Info;

        /// <inheritdoc />
        public event EventHandler? Quit;

        /// <inheritdoc />
        public WorkingHoursCalculation WorkingHoursCalculation
        {
            get
            {
                if (this.disposed)
                {
                    throw new ObjectDisposedException(this.GetType().Name);
                }

                return this.workingHoursCalculation;
            }

            set
            {
                if (this.disposed)
                {
                    throw new ObjectDisposedException(this.GetType().Name);
                }

                this.workingHoursCalculation = value;
                this.Update();
            }
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
        public void ShowMessage(string message)
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(this.GetType().Name);
            }

            this.ShowPopupMessage(ToolTipIcon.Info, message);
        }

        /// <inheritdoc />
        public void ShowWarning(string message)
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(this.GetType().Name);
            }

            this.ShowPopupMessage(ToolTipIcon.Warning, message);
        }

        /// <inheritdoc />
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
                        this.configurationMenuItem.Click -= this.ConfigurationMenuItemClick;
                        this.configurationMenuItem.Dispose();

                        this.aboutMenuItem.Click -= this.AboutMenuItemClick;
                        this.aboutMenuItem.Dispose();

                        this.quitMenuItem.Click -= this.QuitMenuItemClick;
                        this.quitMenuItem.Dispose();

                        this.contextMenuControl.Info -= this.ContextMenuControlInfo;
                        this.contextMenuControl.Dispose();

                        if (this.systemTrayIcon != null)
                        {
                            if (this.systemTrayIcon.ContextMenuStrip != null)
                            {
                                this.systemTrayIcon.ContextMenuStrip.Dispose();
                                this.systemTrayIcon.ContextMenuStrip = null;
                            }

                            this.systemTrayIcon.Visible = false;
                            this.systemTrayIcon.MouseUp -= this.SystemTrayIconMouseUp;
                            this.systemTrayIcon.Dispose();
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
            this.contextMenuControl.CalculatedWorkingHours = this.workingHoursCalculation.CalculatedWorkingHours;
            this.contextMenuControl.CalculatedBreak = this.workingHoursCalculation.CalculatedBreak;
            this.systemTrayIcon.Text = $"{Application.ProductName}\n\n{this.workingHoursCalculation.Configuration.StartTime.ToShortTimeString()} (Start)\n{this.workingHoursCalculation.CalculatedWorkingHours.ToString(TimeFormat, CultureInfo.CurrentCulture)} (Working hours)\n{this.workingHoursCalculation.CalculatedBreak.ToString(TimeFormat, CultureInfo.CurrentCulture)} (Break)";

            lock (this.quickMessageLock)
            {
                this.quickMessage = this.systemTrayIcon.Text;
            }
        }

        /// <summary>
        /// Shows a popup message to the user.
        /// </summary>
        /// <param name="icon">The icon that shall be shown to the user.</param>
        /// <param name="message">The warning message that shall be shown to the user.</param>
        /// <exception cref="ArgumentNullException"><c>message</c> is <c>null</c>.</exception>
        /// <exception cref="ObjectDisposedException">The object has been disposed.</exception>
        protected virtual void ShowPopupMessage(ToolTipIcon icon, string message)
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(this.GetType().Name);
            }

            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            if (this.systemTrayIcon.Visible)
            {
                this.systemTrayIcon.BalloonTipIcon = icon;
                this.systemTrayIcon.BalloonTipTitle = Application.ProductName;
                this.systemTrayIcon.BalloonTipText = message;

                this.systemTrayIcon.ShowBalloonTip(MessageTime);
            }
        }

        /// <summary>
        /// Handles the mouse up event of the <see cref="NotifyIcon"/>.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments containing information about the click.</param>
        protected virtual void SystemTrayIconMouseUp(object? sender, MouseEventArgs e)
        {
            if (e?.Button == MouseButtons.Left)
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
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        protected virtual void QuitMenuItemClick(object? sender, EventArgs e)
        {
            this.OnQuit(EventArgs.Empty);
        }

        /// <summary>
        /// Handles the click event of the specific menu item.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        protected virtual void AboutMenuItemClick(object? sender, EventArgs e)
        {
            this.OnInfo(EventArgs.Empty);
        }

        /// <summary>
        /// Handles the click event of the specific menu item.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        protected virtual void ConfigurationMenuItemClick(object? sender, EventArgs e)
        {
            this.OnConfigure(EventArgs.Empty);
        }

        /// <summary>
        /// Handles the <see cref="ContextMenuDisplayControl.Info"/> event.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        protected virtual void ContextMenuControlInfo(object? sender, EventArgs e)
        {
            this.OnInfo(EventArgs.Empty);
        }

        /// <summary>
        /// Raises the <see cref="Configure"/> event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnConfigure(EventArgs e)
        {
            this.Configure?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the <see cref="Info"/> event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnInfo(EventArgs e)
        {
            this.Info?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the <see cref="Quit"/> event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnQuit(EventArgs e)
        {
            this.Quit?.Invoke(this, e);
        }
    }
}
