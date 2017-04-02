﻿#region GNU General Public License 3
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
using System.Threading;
using System.Windows.Forms;
using Yakka.Calculator;
using Yakka.Forms;
using Yakka.IO;
#endregion

namespace Yakka
{
    /// <summary>
    /// Contains the main entry point for the application.
    /// </summary>
    public static class Program
    {
        #region Constants and Fields

        /// <summary>
        /// Represents the configuration loaded from the persistent storage or initialized with the default configuration.
        /// </summary>
        private static UserConfiguration configuration;

        /// <summary>
        /// Represents the reference to a class able to read and write the configuration.
        /// </summary>
        private static IConfigurationStorage configurationStorage;

        #endregion

        #region Methods

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            SetMainThreadName();
            SetConfigurationStorage();
            ReadConfigurationOrSetDefaultConfiguration();
            DetectStartTime();

            using (ISystemTrayIconView systemTrayIconView = new SystemTrayIconView())
            {
                SystemTrayIconPresenter systemTrayIconPresenter = new SystemTrayIconPresenter(systemTrayIconView, new WorkingHoursCalculation());

                systemTrayIconPresenter.Configure += Configure;
                systemTrayIconPresenter.Info += Info;
                systemTrayIconPresenter.Quit += Quit;
                systemTrayIconPresenter.Show();

                systemTrayIconPresenter.Configuration = configuration;

                Application.Run();

                systemTrayIconPresenter.Hide();
                systemTrayIconPresenter.Quit -= Quit;
                systemTrayIconPresenter.Configure -= Configure;
                systemTrayIconPresenter.Info -= Info;
            }

            WriteConfiguration();
        }

        /// <summary>
        /// Sets the name of the main thread if not set yet.
        /// </summary>
        private static void SetMainThreadName()
        {
            if (string.IsNullOrEmpty(Thread.CurrentThread.Name))
            {
                Thread.CurrentThread.Name = "Main";
            }
        }

        /// <summary>
        /// Sets the instance of <see cref="IConfigurationStorage"/> that shall be used.
        /// </summary>
        private static void SetConfigurationStorage()
        {
            configurationStorage = new DefaultConfigurationStorage();
        }

        /// <summary>
        /// Tries to read the persistent <see cref="UserConfiguration"/>.
        /// If the read fails the default configuration will be set.
        /// </summary>
        private static void ReadConfigurationOrSetDefaultConfiguration()
        {
            try
            {
                configuration = configurationStorage.Read();
            }
            catch
            {
                configuration = null;
            }

            if (configuration == null)
            {
                configuration = new UserConfiguration();
                configuration.Start = DateTime.MinValue; // Start time will be detected later
                configuration.Calculator = new NoBreakWorkingHoursCalculator();
            }
        }

        /// <summary>
        /// Uses the <see cref="StartTimeDetector"/> to detect the start of the working day.
        /// </summary>
        private static void DetectStartTime()
        {
            StartTimeDetector detector = new StartTimeDetector(configuration);
            detector.SetStartTime();
        }

        /// <summary>
        /// Writes the <see cref="UserConfiguration"/> and ignores errors.
        /// </summary>
        private static void WriteConfiguration()
        {
            try
            {
                configurationStorage.Write(configuration);
            }
            catch
            {
                // Errors will be ignored
            }
        }

        /// <summary>
        /// Handles the <see cref="SystemTrayIconPresenter.Info">info event of the presenter</see>.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private static void Info(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Yakka\nA system tray application calculating and displaying your working hours\nCopyright (C) 2017  Tobias Koch\n\nReleased under the GPL (V3)",
                Application.ProductName,
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Information);
        }

        /// <summary>
        /// Handles the <see cref="SystemTrayIconPresenter.Configure">configure event of the presenter</see>.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private static void Configure(object sender, EventArgs e)
        {
            using (ConfigurationForm configurationForm = new ConfigurationForm())
            {
                using (ConfigurationPresenter configurationPresenter = new ConfigurationPresenter(configurationForm, configuration))
                {
                    configurationForm.ShowDialog();
                    WriteConfiguration();
                }
            }
        }

        /// <summary>
        /// Handles the <see cref="SystemTrayIconPresenter.Quit">quit event of the presenter</see>.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private static void Quit(object sender, EventArgs e)
        {
            Application.ExitThread();
        }

        #endregion
    }
}
