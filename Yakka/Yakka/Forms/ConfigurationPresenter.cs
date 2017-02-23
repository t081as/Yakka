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
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Yakka.Calculator;
#endregion

namespace Yakka.Forms
{
    /// <summary>
    /// Represents the presenter managing the <see cref="IConfigurationView"/>.
    /// </summary>
    public class ConfigurationPresenter : IDisposable
    {
        #region Constants and Fields

        /// <summary>
        /// Represents the reference to the view managed by this presenter.
        /// </summary>
        private IConfigurationView view;

        /// <summary>
        /// Represents the reference to the user configuration that shall be displayed and updated..
        /// </summary>
        private UserConfiguration configuration;

        /// <summary>
        /// Indicates if the class has already been disposed.
        /// </summary>
        private bool disposed = false;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationPresenter"/> class.
        /// </summary>
        /// <param name="view">The view that shall be used.</param>
        /// <param name="configuration">The user configuration that shall be displayed and updated.</param>
        /// <exception cref="ArgumentNullException"><c>view</c> is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException"><c>configuration</c> is <c>null</c>.</exception>
        public ConfigurationPresenter(IConfigurationView view, UserConfiguration configuration)
        {
            if (view == null)
            {
                throw new ArgumentNullException("view");
            }

            if (configuration == null)
            {
                throw new ArgumentNullException("configuration");
            }

            this.view = view;
            this.view.ConfigurationChanged += this.View_ConfigurationChanged;
            this.configuration = configuration;

            this.view.Start = this.configuration.Start;
            this.view.AvailableCalculators = this.GetAvailableCalculators().ToArray();
            this.view.SelectedCalculator = this.configuration.Calculator;
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="ConfigurationPresenter"/> class.
        /// </summary>
        ~ConfigurationPresenter()
        {
            this.Dispose(false);
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
                        if (this.view != null)
                        {
                            this.view.ConfigurationChanged -= this.View_ConfigurationChanged;
                            this.view = null;
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
        /// Handles the <see cref="IConfigurationView.ConfigurationChanged"/> event.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the data.</param>
        protected virtual void View_ConfigurationChanged(object sender, EventArgs e)
        {
            if (this.view.Start != this.configuration.Start)
            {
                this.configuration.Start = this.view.Start;
            }

            if (this.view.SelectedCalculator != this.configuration.Calculator)
            {
                this.configuration.Calculator = this.view.SelectedCalculator;
            }
        }

        /// <summary>
        /// Returns all available implementations of <see cref="IWorkingHoursCalculator"/>.
        /// </summary>
        /// <returns>All available implementations of <see cref="IWorkingHoursCalculator"/>.</returns>
        private IEnumerable<IWorkingHoursCalculator> GetAvailableCalculators()
        {
            List<IWorkingHoursCalculator> calculators = new List<IWorkingHoursCalculator>();

            foreach (Type currentType in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (typeof(IWorkingHoursCalculator).IsAssignableFrom(currentType) &&
                    currentType.IsClass &&
                    !currentType.IsAbstract)
                {
                    calculators.Add((IWorkingHoursCalculator)Activator.CreateInstance(currentType));
                }
            }

            return calculators;
        }

        #endregion
    }
}
