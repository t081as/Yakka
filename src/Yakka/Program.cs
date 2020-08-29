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
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Yakka.Calculation;
using Yakka.Forms;
using Yakka.StartTime;

namespace Yakka
{
    /// <summary>
    /// Contains the main entry point of the application.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The presenter of the main view.
        /// </summary>
        private static SystemTrayIconPresenter? mainPresenter;

        /// <summary>
        /// The configuration view.
        /// </summary>
        private static ConfigurationForm? configurationView;

        /// <summary>
        /// The presenter of the configuration view.
        /// </summary>
        private static ConfigurationPresenter? configurationPresenter;

        /// <summary>
        /// The detail view.
        /// </summary>
        private static DetailForm? detailView;

        /// <summary>
        /// The presenter of the detail view.
        /// </summary>
        private static DetailPresenter? detailPresenter;

        /// <summary>
        /// The main entry point of the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var configuration = WorkingHoursConfiguration.Load();
            configuration.StartTime = new StartTimeDetector().StartTime;

            using var mainView = new SystemTrayIconView();
            mainPresenter = new SystemTrayIconPresenter(mainView, configuration, new DefaultCalculator());
            mainPresenter.Quit += MainPresenterQuit;
            mainPresenter.Configure += MainPresenterConfigure;
            mainPresenter.Info += MainPresenterInfo;
            mainPresenter.Details += MainPresenterDetails;
            mainPresenter.Show();

            configurationView = new ConfigurationForm(WorkingHoursCalculators.All);
            configurationPresenter = new ConfigurationPresenter(configurationView, configuration);
            configurationPresenter.ConfigurationChanged += ConfigurationPresenterConfigurationChanged;

            detailView = new DetailForm();
            detailPresenter = new DetailPresenter(detailView);

            Application.Run();
        }

        /// <summary>
        /// Handles the <see cref="SystemTrayIconPresenter.Details"/> event.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">A <see cref="WorkingHoursCalculationEventArgs"/> containing the calculation.</param>
        private static void MainPresenterDetails(object? sender, WorkingHoursCalculationEventArgs e)
        {
            detailView?.Show();

            if (detailPresenter != null)
            {
                detailPresenter.WorkingHoursCalculation = e.Calculation;
            }
        }

        /// <summary>
        /// Handles the <see cref="ConfigurationPresenter.ConfigurationChanged"/> event.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">A <see cref="WorkingHoursConfigurationEventArgs"/> containing the updated configuration.</param>
        private static void ConfigurationPresenterConfigurationChanged(object? sender, WorkingHoursConfigurationEventArgs e)
        {
            if (mainPresenter != null)
            {
                WorkingHoursConfiguration.Save(e.Configuration);
                mainPresenter.Configuration = e.Configuration;
            }
        }

        /// <summary>
        /// Handles the <see cref="SystemTrayIconPresenter.Info"/> event.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">An empty <see cref="EventArgs"/>.</param>
        private static void MainPresenterInfo(object? sender, EventArgs e)
        {
            using var aboutView = new AboutForm();
            var aboutPresenter = new AboutPresenter(aboutView);

            aboutView.ShowDialog();
        }

        /// <summary>
        /// Handles the <see cref="SystemTrayIconPresenter.Configure"/> event.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">An empty <see cref="EventArgs"/>.</param>
        private static void MainPresenterConfigure(object? sender, EventArgs e)
        {
            configurationView?.Show();
        }

        /// <summary>
        /// Handles the <see cref="SystemTrayIconPresenter.Quit"/> event.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">An empty <see cref="EventArgs"/>.</param>
        private static void MainPresenterQuit(object? sender, EventArgs e)
        {
            mainPresenter?.Hide();
            Application.Exit();
        }
    }
}