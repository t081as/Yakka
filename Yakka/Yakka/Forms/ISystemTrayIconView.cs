﻿#region GNU General Public License 3
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
#endregion

namespace Yakka.Forms
{
    /// <summary>
    /// Provides the interface to a view managed by the <see cref="SystemTrayIconPresenter"/>.
    /// </summary>
    /// <remarks>
    /// see https://en.wikipedia.org/wiki/Model-view-presenter for additional information about the pattern
    /// </remarks>
    public interface ISystemTrayIconView : IDisposable
    {
        #region Events

        /// <summary>
        /// Occurs when the user wants to edit the configuration.
        /// </summary>
        event EventHandler Configure;

        /// <summary>
        /// Occurs when the user wants to display software information.
        /// </summary>
        event EventHandler Info;

        /// <summary>
        /// Occurs when the user wants to quit the application.
        /// </summary>
        event EventHandler Quit;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether the system tray icon is visible.
        /// </summary>
        bool Visible
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the working hours information.
        /// </summary>
        WorkingHours WorkingHours
        {
            get;
            set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Shows a message to the user.
        /// </summary>
        /// <param name="message">The message that shall be shown to the user.</param>
        /// <exception cref="ArgumentNullException"><c>message</c> is <c>null</c>.</exception>
        /// <exception cref="ObjectDisposedException">The object has been disposed.</exception>
        void ShowMessage(string message);

        #endregion
    }
}
