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
using System.Threading;

namespace Yakka.Forms
{
    /// <summary>
    /// Represents the presenter managing the system tray icon.
    /// </summary>
    public class SystemTrayIconPresenter : IDisposable
    {
        /// <summary>
        /// Represents the time in seconds the background thread sleeps before calculating or recalculating
        /// the working hours based on the current time.
        /// </summary>
        private const int UpdateTime = 30;

        /// <summary>
        /// The interval that shall be waited before displaying the same message to the user.
        /// </summary>
        private static readonly TimeSpan MessageTime = TimeSpan.FromMinutes(5);

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
        /// The calculator that shall be used.
        /// </summary>
        private ICalculator calculator;

        /// <summary>
        /// Represents the object used to lock the <see cref="Monitor"/>.
        /// </summary>
        private object monitorLock = new object();

        /// <summary>
        /// The source for the token used to cancel the calculation thread.
        /// </summary>
        private CancellationTokenSource calculationThreadCancellationTokenSource = new CancellationTokenSource();

        /// <summary>
        /// The most recent warning displayed to the user.
        /// </summary>
        private string? recentWarning;

        /// <summary>
        /// The time themost recent warning was displayed to the user.
        /// </summary>
        private DateTime recentWarningTime = DateTime.MinValue;

        /// <summary>
        /// Indicates if the class has already been disposed.
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemTrayIconPresenter"/> class.
        /// </summary>
        /// <param name="view">The view that shall be used.</param>
        /// <param name="configuration">The reference to the configuration.</param>
        /// <param name="calculator">The calculator that shall be used.</param>
        /// <exception cref="ArgumentNullException"><c>view</c> is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException"><c>configuration</c> is <c>null</c>.</exception>
        public SystemTrayIconPresenter(ISystemTrayIconView view, WorkingHoursConfiguration configuration, ICalculator calculator)
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
            this.calculator = calculator;
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="SystemTrayIconPresenter"/> class.
        /// </summary>
        ~SystemTrayIconPresenter()
        {
            this.Dispose(false);
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
        /// Occurs when the user wants to display details of the calculation.
        /// </summary>
        public event EventHandler<WorkingHoursCalculationEventArgs>? Details;

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
            this.view.Details += this.ViewDetails;
            this.view.Visible = true;

            this.calculationThread = new Thread(new ParameterizedThreadStart(this.CalculationUpdateThread));
            this.calculationThread.Name = "Calculation";
            this.calculationThread.IsBackground = true;
            this.calculationThread.Start(this.calculationThreadCancellationTokenSource.Token);

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

            this.calculationThreadCancellationTokenSource.Cancel();
            this.Update();
            this.calculationThread = null;

            this.view.Visible = false;
            this.view.Configure -= this.ViewConfigure;
            this.view.Info -= this.ViewInfo;
            this.view.Quit -= this.ViewQuit;
            this.view.Details -= this.ViewDetails;

            this.isVisible = false;
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
        /// <param name="obj">An <see cref="object"/> given to the thread.</param>
        /// <exception cref="ArgumentNullException"><c>obj</c> is <c>null</c>.</exception>
        protected virtual void CalculationUpdateThread(object? obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            CancellationToken token = (CancellationToken)obj;

            while (!token.IsCancellationRequested)
            {
                try
                {
                    lock (this.configurationLock)
                    {
                        if (this.configuration != null)
                        {
                            var calculation = this.calculator.Calculate(
                                this.configuration,
                                DateTime.Now);

                            this.view.WorkingHoursCalculation = calculation;

                            if (calculation.Warning != null)
                            {
                                if (calculation.Warning != this.recentWarning ||
                                    (calculation.Warning != null && DateTime.Now - this.recentWarningTime > MessageTime))
                                {
                                    this.recentWarning = calculation.Warning;
                                    this.recentWarningTime = DateTime.Now;
                                    this.view.ShowWarning(calculation.Warning);
                                }
                            }
                        }
                    }
                }
                catch (Exception)
                {
                }

                lock (this.monitorLock)
                {
                    Monitor.Wait(this.monitorLock, TimeSpan.FromSeconds(UpdateTime));
                }
            }
        }

        /// <summary>
        /// Handles the <see cref="ISystemTrayIconView.Details"/> event of the view.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        protected virtual void ViewDetails(object? sender, EventArgs e)
        {
            this.OnDetails(new WorkingHoursCalculationEventArgs(this.view.WorkingHoursCalculation));
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

        /// <summary>
        /// Raises the <see cref="Details"/> event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnDetails(WorkingHoursCalculationEventArgs e)
        {
            this.Details?.Invoke(this, e);
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
                        this.calculationThreadCancellationTokenSource.Dispose();
                    }
                    catch
                    {
                        // The Dispose method must never throw exceptions
                    }
                }

                this.disposed = true;
            }
        }
    }
}
