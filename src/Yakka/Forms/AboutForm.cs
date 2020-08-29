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
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Mjolnir.IO;

namespace Yakka.Forms
{
    /// <summary>
    /// Represents the window that shows application information.
    /// </summary>
    public partial class AboutForm : Form, IAboutView
    {
        /// <summary>
        /// The list of the software authors.
        /// </summary>
        private IEnumerable<Author> authors;

        /// <summary>
        /// Serves as lock object for the synchronized access to the <see cref="authors"/> member.
        /// </summary>
        private object authorsLock = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="AboutForm"/> class.
        /// </summary>
        public AboutForm()
        {
            this.authors = new List<Author>();

            this.InitializeComponent();

            this.listBoxAuthors.ItemHeight = 40;

            this.Icon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location);
            this.Text = string.Format(CultureInfo.CurrentCulture, AboutFormResources.TitleText, string.Empty).Trim();
            this.tabPageInformation.Text = AboutFormResources.InformationText;
            this.tabPageAuthors.Text = AboutFormResources.AuthorsText;
            this.tabPageLicense.Text = AboutFormResources.LicenseText;
            this.buttonClose.Text = AboutFormResources.CloseText;
            this.labelAuthorOrder.Text = AboutFormResources.OrderText;
        }

        /// <inheritdoc />
        public string ApplicationName
        {
            get => this.labelName.Text;
            set
            {
                this.labelName.Text = value;
                this.Text = string.Format(CultureInfo.CurrentCulture, AboutFormResources.TitleText, value).Trim();
            }
        }

        /// <inheritdoc />
        public string ApplicationVersion
        {
            get => this.labelVersion.Text;
            set => this.labelVersion.Text = value;
        }

        /// <inheritdoc />
        public string ApplicationDescription
        {
            get => this.labelDescription.Text;
            set => this.labelDescription.Text = value;
        }

        /// <inheritdoc />
        public string ApplicatioCopyright
        {
            get => this.labelCopyright.Text;
            set => this.labelCopyright.Text = value;
        }

        /// <inheritdoc />
        public IEnumerable<Author> Authors
        {
            get
            {
                lock (this.authorsLock)
                {
                    return this.authors;
                }
            }

            set
            {
                lock (this.authorsLock)
                {
                    this.authors = value ?? throw new ArgumentNullException(nameof(value));
                }

                this.listBoxAuthors.Items.Clear();
                foreach (var author in value)
                {
                    this.listBoxAuthors.Items.Add(author);
                }
            }
        }

        /// <inheritdoc />
        public string LicenseText
        {
            get => this.textBoxLicense.Text;
            set => this.textBoxLicense.Text = value;
        }

        /// <summary>
        /// Handles the click event of the <see cref="buttonClose"/>.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The empty <see cref="EventArgs"/>.</param>
        private void ButtonCloseClick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ListBoxAuthorsDrawItem(object sender, DrawItemEventArgs e)
        {
            Author item;

            lock (this.authorsLock)
            {
                item = this.authors.ElementAt(e.Index);
            }

            using var foregroundBrush = new SolidBrush(this.listBoxAuthors.ForeColor);
            using var backgroundBrush = new SolidBrush(this.listBoxAuthors.BackColor);

            e.Graphics.FillRectangle(backgroundBrush, e.Bounds);
            e.Graphics.DrawString($"{item.Name}\n{item.EMailAddress}", e.Font, foregroundBrush, new PointF(e.Bounds.X + 5, e.Bounds.Y));
        }
    }
}
