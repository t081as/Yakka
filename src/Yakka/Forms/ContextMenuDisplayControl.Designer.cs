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

namespace Yakka.Forms
{
    partial class ContextMenuDisplayControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContextMenuDisplayControl));
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.labelWorkingHours = new Mjolnir.Forms.RenderingHintLabel();
            this.labelBreak = new Mjolnir.Forms.RenderingHintLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxLogo
            // 
            this.pictureBoxLogo.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.pictureBoxLogo, "pictureBoxLogo");
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.TabStop = false;
            this.pictureBoxLogo.Click += new System.EventHandler(this.PictureBoxLogoClick);
            // 
            // labelWorkingHours
            // 
            resources.ApplyResources(this.labelWorkingHours, "labelWorkingHours");
            this.labelWorkingHours.Name = "labelWorkingHours";
            this.labelWorkingHours.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            // 
            // labelBreak
            // 
            resources.ApplyResources(this.labelBreak, "labelBreak");
            this.labelBreak.Name = "labelBreak";
            this.labelBreak.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            // 
            // ContextMenuDisplayControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.labelBreak);
            this.Controls.Add(this.labelWorkingHours);
            this.Controls.Add(this.pictureBoxLogo);
            this.Name = "ContextMenuDisplayControl";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private Mjolnir.Forms.RenderingHintLabel labelWorkingHours;
        private Mjolnir.Forms.RenderingHintLabel labelBreak;
    }
}
