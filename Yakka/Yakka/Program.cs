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
using System.Windows.Forms;
using Yakka.Forms;
#endregion

namespace Yakka
{
    /// <summary>
    /// Contains the main entry point for the application.
    /// </summary>
    public static class Program
    {
        #region Methods

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (ISystemTrayIconView systemTrayIconView = new SystemTrayIconView())
            {
                SystemTrayIconPresenter systemTrayIconPresenter = new SystemTrayIconPresenter(systemTrayIconView);

                systemTrayIconPresenter.Quit += Quit;
                systemTrayIconPresenter.Show();

                Application.Run();

                systemTrayIconPresenter.Hide();
                systemTrayIconPresenter.Quit -= Quit;
            }
        }

        /// <summary>
        /// Handles the <see cref="SystemTrayIconPresenter.Quit">quit event of the presenter</see>.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The empty event arguments.</param>
        private static void Quit(object sender, EventArgs e)
        {
            Application.ExitThread();
        }

        #endregion
    }
}
