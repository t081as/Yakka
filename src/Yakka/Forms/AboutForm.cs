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
        /// The lock object for synchronized access to the <see cref="authors"/> member.
        /// </summary>
        private object authorsLock = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="AboutForm"/> class.
        /// </summary>
        public AboutForm()
        {
            this.authors = new List<Author>();

            this.InitializeComponent();

            this.Icon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location);
        }

        /// <inheritdoc />
        public string ApplicationName
        {
            get => this.labelName.Text;
            set => this.labelName.Text = value;
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
                    this.authors = value;
                }

                this.UpdateAuthors();
            }
        }

        /// <inheritdoc />
        public string LicenseText
        {
            get => this.textBoxLicense.Text;
            set => this.labelCopyright.Text = value;
        }

        /// <summary>
        /// Updates the displayed authors list.
        /// </summary>
        protected virtual void UpdateAuthors()
        {
            var authorsBuilder = new StringBuilder();

            lock (this.authorsLock)
            {
                if (!this.authors.Any())
                {
                    return;
                }

                foreach (var author in this.authors)
                {
                    authorsBuilder.AppendLine($"{author.Name} <{author.EMailAddress}>");
                }
            }

            using var font = new Font("Arial", 12f);
            var requiredSize = TextRenderer.MeasureText(authorsBuilder.ToString(), font);

            Image image = new Bitmap(this.pictureBoxAuthors.Width, requiredSize.Height);

            TextFormatFlags flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.WordBreak;

            using Graphics graphics = Graphics.FromImage(image);
            TextRenderer.DrawText(graphics, authorsBuilder.ToString(), font, Point.Empty, Color.Black, flags);

            this.pictureBoxAuthors.Image = image;
        }

        /// <summary>
        /// Handles the resize event of the <see cref="pictureBoxAuthors"/>.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The empty <see cref="EventArgs"/>.</param>
        private void PictureBoxAuthorsResize(object sender, EventArgs e)
        {
            this.UpdateAuthors();
        }
    }
}
