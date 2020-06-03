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
using System.ComponentModel;
using System.Threading;

namespace Yakka.Forms
{
    /// <summary>
    /// Represents the presenter managing the system tray icon.
    /// </summary>
    public class SystemTrayIconPresenter
    {
        /// <summary>
        /// Represents the time in seconds the background thread sleeps before calculating or recalculating
        /// the working hours based on the current time.
        /// </summary>
        private const int UPDATETIME = 30;

        /// <summary>
        /// Indicates if the system tray icon has been shown by the presenter.
        /// </summary>
        private bool isVisible = false;

        /// <summary>
        /// Represents the view that shall be used by this presenter.
        /// </summary>
        private ISystemTrayIconView view;

        /// <summary>
        /// Represents the working hours configuration.
        /// </summary>
        private WorkingHoursConfiguration configuration = new WorkingHoursConfiguration();

        /// <summary>
        /// Represents the object used to lock the <see cref="configuration"/> property.
        /// </summary>
        private object configurationLock = new object();

        /// <summary>
        /// Represents the reference to the background thread used for the calculations.
        /// </summary>
        private Thread? calculationThread;

        /// <summary>
        /// Represents the object used to lock the <see cref="Monitor"/>.
        /// </summary>
        private object monitorLock = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemTrayIconPresenter"/> class.
        /// </summary>
        /// <param name="view">The view that shall be used.</param>
        /// <param name="configuration">The reference to the configuration.</param>
        /// <exception cref="ArgumentNullException"><c>view</c> is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException"><c>configuration</c> is <c>null</c>.</exception>
        public SystemTrayIconPresenter(ISystemTrayIconView view, WorkingHoursConfiguration configuration)
        {
            if (view == null)
            {
                throw new ArgumentNullException(nameof(view));
            }

            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            this.view = view;
            this.configuration = configuration;
        }

        /// <summary>
        /// Occurs when the user wants to edit the configuration.
        /// </summary>
        public event EventHandler? Configure;

        /// <summary>
        /// Occurs when the user wants to display software information.
        /// </summary>
        public event EventHandler? Info;

        /// <summary>
        /// Occurs when the user wants to quit the application.
        /// </summary>
        public event EventHandler? Quit;

        /// <summary>
        /// Gets or sets the working hours configuration.
        /// </summary>
        /// <value>The working hours configuration.</value>
        public WorkingHoursConfiguration Configuration
        {
            get
            {
                lock (this.configurationLock)
                {
                    return this.configuration;
                }
            }

            set
            {
                lock (this.configurationLock)
                {
                    this.configuration = value;
                }

                this.Update();
            }
        }

        /// <summary>
        /// Displays the system tray icon.
        /// </summary>
        /// <exception cref="InvalidOperationException">The system tray icon is already visible.</exception>
        public void Show()
        {
            if (this.isVisible)
            {
                throw new InvalidOperationException();
            }

            this.view.Configure += this.ViewConfigure;
            this.view.Info += this.ViewInfo;
            this.view.Quit += this.ViewQuit;
            this.view.Visible = true;

            this.calculationThread = new Thread(new ThreadStart(this.CalculationUpdateThread));
            this.calculationThread.Name = "Calculation";
            this.calculationThread.IsBackground = true;
            this.calculationThread.Start();

            this.isVisible = true;
        }

        /// <summary>
        /// Hides the system tray icon.
        /// </summary>
        /// <exception cref="InvalidOperationException">The system tray icon is not visible.</exception>
        public void Hide()
        {
            if (!this.isVisible)
            {
                throw new InvalidOperationException();
            }

            this.calculationThread?.Abort();
            this.calculationThread = null;

            this.view.Visible = false;
            this.view.Configure -= this.ViewConfigure;
            this.view.Info -= this.ViewInfo;
            this.view.Quit -= this.ViewQuit;

            this.isVisible = false;
        }

        /// <summary>
        /// Triggers the <see cref="CalculationUpdateThread"/> to calculate new values immediately.
        /// </summary>
        protected virtual void Update()
        {
            lock (this.monitorLock)
            {
                Monitor.Pulse(this.monitorLock);
            }
        }

        /// <summary>
        /// Represents the method executed in a background thread to calculate the working hours.
        /// </summary>
        protected virtual void CalculationUpdateThread()
        {
            try
            {
                while (true)
                {
                    try
                    {
                        lock (this.configurationLock)
                        {
                            if (this.configuration != null)
                            {
                                this.view.WorkingHoursCalculation = WorkingHoursCalculator.Calculate(
                                    this.configuration,
                                    DateTime.Now);
                            }
                        }
                    }
                    catch (ThreadAbortException)
                    {
                        throw;
                    }
                    catch
                    {
                        this.view.WorkingHoursCalculation = new WorkingHoursCalculation();
                    }

                    lock (this.monitorLock)
                    {
                        Monitor.Wait(this.monitorLock, TimeSpan.FromSeconds(UPDATETIME));
                    }
                }
            }
            catch (ThreadAbortException)
            {
                Thread.ResetAbort();
            }
        }

        /// <summary>
        /// Handles the <see cref="ISystemTrayIconView.Quit">quit event of the view</see>.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        protected virtual void ViewQuit(object? sender, EventArgs e)
        {
            this.OnQuit(EventArgs.Empty);
        }

        /// <summary>
        /// Handles the <see cref="ISystemTrayIconView.Info">info event of the view</see>.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        protected virtual void ViewInfo(object? sender, EventArgs e)
        {
            this.OnInfo(EventArgs.Empty);
        }

        /// <summary>
        /// Handles the <see cref="ISystemTrayIconView.Configure">configure event of the view</see>.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        protected virtual void ViewConfigure(object? sender, EventArgs e)
        {
            this.OnConfigure(EventArgs.Empty);
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
