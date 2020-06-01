#region GNU General Public License 3
// Yakka - A system tray application calculating and displaying your working hours
// Copyright (C) 2017-2018  Tobias Koch
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

namespace Yakka.Forms
{
    /// <summary>
    /// Represents the control displayed within the context menu of the application.
    /// </summary>
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
            this.panelIcon = new System.Windows.Forms.Panel();
            this.pictureBoxIcon = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanelHorizontal = new System.Windows.Forms.TableLayoutPanel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.labelWorkingHours = new Mjolnir.Forms.RenderingHintLabel();
            this.labelBreak = new Mjolnir.Forms.RenderingHintLabel();
            this.panelIcon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).BeginInit();
            this.tableLayoutPanelHorizontal.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelIcon
            // 
            this.panelIcon.Controls.Add(this.pictureBoxIcon);
            this.panelIcon.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelIcon.Location = new System.Drawing.Point(105, 0);
            this.panelIcon.Margin = new System.Windows.Forms.Padding(0);
            this.panelIcon.Name = "panelIcon";
            this.panelIcon.Size = new System.Drawing.Size(70, 80);
            this.panelIcon.TabIndex = 0;
            // 
            // pictureBoxIcon
            // 
            this.pictureBoxIcon.Image = global::Yakka.Properties.Resources.Yakka_64;
            this.pictureBoxIcon.Location = new System.Drawing.Point(3, 3);
            this.pictureBoxIcon.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBoxIcon.Name = "pictureBoxIcon";
            this.pictureBoxIcon.Size = new System.Drawing.Size(64, 64);
            this.pictureBoxIcon.TabIndex = 0;
            this.pictureBoxIcon.TabStop = false;
            this.pictureBoxIcon.Click += new System.EventHandler(this.PictureBoxIconClick);
            // 
            // tableLayoutPanelHorizontal
            // 
            this.tableLayoutPanelHorizontal.ColumnCount = 1;
            this.tableLayoutPanelHorizontal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelHorizontal.Controls.Add(this.labelWorkingHours, 0, 0);
            this.tableLayoutPanelHorizontal.Controls.Add(this.labelBreak, 0, 1);
            this.tableLayoutPanelHorizontal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelHorizontal.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelHorizontal.Name = "tableLayoutPanelHorizontal";
            this.tableLayoutPanelHorizontal.RowCount = 3;
            this.tableLayoutPanelHorizontal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanelHorizontal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelHorizontal.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelHorizontal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelHorizontal.Size = new System.Drawing.Size(105, 80);
            this.tableLayoutPanelHorizontal.TabIndex = 1;
            // 
            // labelWorkingHours
            // 
            this.labelWorkingHours.AutoSize = true;
            this.labelWorkingHours.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelWorkingHours.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelWorkingHours.Location = new System.Drawing.Point(3, 0);
            this.labelWorkingHours.Name = "labelWorkingHours";
            this.labelWorkingHours.Size = new System.Drawing.Size(99, 35);
            this.labelWorkingHours.TabIndex = 1;
            this.labelWorkingHours.Text = "XX:XX";
            this.labelWorkingHours.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelBreak
            // 
            this.labelBreak.AutoSize = true;
            this.labelBreak.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelBreak.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBreak.Location = new System.Drawing.Point(3, 35);
            this.labelBreak.Name = "labelBreak";
            this.labelBreak.Size = new System.Drawing.Size(99, 30);
            this.labelBreak.TabIndex = 2;
            this.labelBreak.Text = "YY:YY";
            this.labelBreak.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ContextMenuDisplayControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.tableLayoutPanelHorizontal);
            this.Controls.Add(this.panelIcon);
            this.Name = "ContextMenuDisplayControl";
            this.Size = new System.Drawing.Size(175, 80);
            this.panelIcon.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).EndInit();
            this.tableLayoutPanelHorizontal.ResumeLayout(false);
            this.tableLayoutPanelHorizontal.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelIcon;
        private System.Windows.Forms.PictureBox pictureBoxIcon;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelHorizontal;
        private Mjolnir.Forms.RenderingHintLabel labelWorkingHours;
        private Mjolnir.Forms.RenderingHintLabel labelBreak;
        private System.Windows.Forms.ToolTip toolTip;
    }
}
