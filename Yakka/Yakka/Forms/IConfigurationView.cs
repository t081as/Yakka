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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace Yakka.Forms
{
    /// <summary>
    /// Provides the interface to a view managed by the <see cref="ConfigurationPresenter"/>.
    /// </summary>
    public interface IConfigurationView
    {
        #region Properties

        /// <summary>
        /// Gets or sets the configuration used to calculate the working hours.
        /// </summary>
        UserConfiguration Configuration
        {
            get;
            set;
        }

        #endregion
    }
}
