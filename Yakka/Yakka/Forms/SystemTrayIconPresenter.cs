#region GNU General Public License 3
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
#endregion

#region Namespaces
using System;
using System.ComponentModel;
using System.Threading;
#endregion

namespace Yakka.Forms
{
    /// <summary>
    /// Represents the presenter managing the system tray icon.
    /// </summary>
    public class SystemTrayIconPresenter
    {
        #region Constants and Fields

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
        /// Represents the configuration containing the start of the working day and the calculator that shall be used.
        /// </summary>
        private UserConfiguration configuration;

        /// <summary>
        /// Represents the object used to lock the <see cref="configuration"/> property.
        /// </summary>
        private object configurationLock = new object();

        /// <summary>
        /// Represents the reference to the calculation module.
        /// </summary>
        private WorkingHoursCalculation calculation;

        /// <summary>
        /// Represents the reference to the background thread used for the calculations.
        /// </summary>
        private Thread calculationThread;

        /// <summary>
        /// Represents the object used to lock the <see cref="Monitor"/>.
        /// </summary>
        private object monitorLock = new object();

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemTrayIconPresenter"/> class.
        /// </summary>
        /// <param name="view">The view that shall be used.</param>
        /// <param name="calculation">The reference to the calculation module.</param>
        /// <exception cref="ArgumentNullException"><c>view</c> is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException"><c>calculation</c> is <c>null</c>.</exception>
        public SystemTrayIconPresenter(ISystemTrayIconView view, WorkingHoursCalculation calculation)
        {
            if (view == null)
            {
                throw new ArgumentNullException("view");
            }

            if (calculation == null)
            {
                throw new ArgumentNullException("calculation");
            }

            this.view = view;
            this.calculation = calculation;
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
        /// Gets or sets the configuration containing the start of the working day and the calculator that shall be used.
        /// </summary>
        public UserConfiguration Configuration
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
                    if (this.configuration != null)
                    {
                        this.configuration.PropertyChanged -= this.Configuration_PropertyChanged;
                    }

                    bool displayMessage = this.configuration == null && value != null;

                    this.configuration = value;

                    if (this.configuration != null)
                    {
                        this.configuration.PropertyChanged += this.Configuration_PropertyChanged;
                        this.Update();
                    }

                    if (displayMessage)
                    {
                        this.view.ShowMessage($"Start: {this.configuration.Start.ToShortTimeString()}");
                    }
                }
            }
        }

        #endregion

        #region Methods

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

            this.view.Configure += this.View_Configure;
            this.view.Info += this.View_Info;
            this.view.Quit += this.View_Quit;
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

            this.calculationThread.Abort();
            this.calculationThread = null;

            this.view.Visible = false;
            this.view.Configure -= this.View_Configure;
            this.view.Info -= this.View_Info;
            this.view.Quit -= this.View_Quit;

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
                                this.view.WorkingHours = this.calculation.Calculate(
                                    this.configuration.Calculator,
                                    this.configuration.Start,
                                    DateTime.Now);
                            }
                        }
                    }
                    catch (ThreadAbortException)
                    {
                        throw;
                    }
                    catch (Exception)
                    {
                        this.view.WorkingHours = new WorkingHours();
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
        /// Handles changes of the user configuration.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">A <see cref="PropertyChangedEventArgs"/> that contains the data.</param>
        protected virtual void Configuration_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.Update();

            if (e.PropertyName == "Start")
            {
                try
                {
                    this.view.ShowMessage($"Start: {this.configuration.Start.ToShortTimeString()}");
                }
                catch (ObjectDisposedException)
                {
                    // Object has already been disposed; ignore the exception
                }
            }
        }

        /// <summary>
        /// Handles the <see cref="ISystemTrayIconView.Quit">quit event of the view</see>.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        protected virtual void View_Quit(object sender, EventArgs e)
        {
            this.OnQuit(EventArgs.Empty);
        }

        /// <summary>
        /// Handles the <see cref="ISystemTrayIconView.Info">info event of the view</see>.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        protected virtual void View_Info(object sender, EventArgs e)
        {
            this.OnInfo(EventArgs.Empty);
        }

        /// <summary>
        /// Handles the <see cref="ISystemTrayIconView.Configure">configure event of the view</see>.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        protected virtual void View_Configure(object sender, EventArgs e)
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

        #endregion
    }
}
