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
using System.Collections.Generic;
using System.ComponentModel;
#endregion

namespace Yakka.Forms
{
    /// <summary>
    /// Represents the presenter managing the system tray icon.
    /// </summary>
    /// <remarks>
    /// see https://en.wikipedia.org/wiki/Model-view-presenter for additional information about the pattern
    /// </remarks>
    public class SystemTrayIconPresenter
    {
        #region Constants and Fields

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

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemTrayIconPresenter"/> class.
        /// </summary>
        /// <param name="view">The view that shall be used.</param>
        /// <exception cref="ArgumentNullException"><c>view</c> is <c>null</c>.</exception>
        public SystemTrayIconPresenter(ISystemTrayIconView view)
        {
            if (view == null)
            {
                throw new ArgumentNullException("view");
            }

            this.view = view;
        }

        #endregion

        #region Events

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
                    this.Update();
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

            this.view.Visible = false;
            this.view.Configure -= this.View_Configure;
            this.view.Info -= this.View_Info;
            this.view.Quit -= this.View_Quit;

            this.isVisible = false;
        }

        /// <summary>
        /// Updates all view-related elements by recalculating all shown values.
        /// </summary>
        protected virtual void Update()
        {
            if (this.configuration != null)
            {
                DateTime currentDateTime = DateTime.Now;

                TimeSpan currentWorkingHours = this.configuration.Calculator.CalculateWorkingHours(this.configuration.Start, currentDateTime);
                TimeSpan currentBreak = this.configuration.Calculator.CalculateBreak(this.configuration.Start, currentDateTime);

                Dictionary<byte, DateTime> endOfWorkDayEstimations = new Dictionary<byte, DateTime>();

                for (byte hoursWorked = 0; hoursWorked < 12; hoursWorked++)
                {
                    throw new NotImplementedException();
                }
            }
        }

        /// <summary>
        /// Handles changes of the user configuration.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments containing information about the changed property.</param>
        protected virtual void Configuration_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.Update();
        }

        /// <summary>
        /// Handles the <see cref="ISystemTrayIconView.Quit">quit event of the view</see>.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The empty event arguments.</param>
        protected virtual void View_Quit(object sender, EventArgs e)
        {
            this.OnQuit(EventArgs.Empty);
        }

        /// <summary>
        /// Handles the <see cref="ISystemTrayIconView.Info">info event of the view</see>.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The empty event arguments.</param>
        protected virtual void View_Info(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Handles the <see cref="ISystemTrayIconView.Configure">configure event of the view</see>.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The empty event arguments.</param>
        protected virtual void View_Configure(object sender, EventArgs e)
        {
            throw new NotImplementedException();
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
