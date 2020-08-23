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
    partial class DetailForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DetailForm));
            this.panelDetails = new System.Windows.Forms.Panel();
            this.label10Time = new Mjolnir.Forms.RenderingHintLabel();
            this.label10 = new Mjolnir.Forms.RenderingHintLabel();
            this.label8Time = new Mjolnir.Forms.RenderingHintLabel();
            this.label8 = new Mjolnir.Forms.RenderingHintLabel();
            this.label7Time = new Mjolnir.Forms.RenderingHintLabel();
            this.label7 = new Mjolnir.Forms.RenderingHintLabel();
            this.label6Time = new Mjolnir.Forms.RenderingHintLabel();
            this.label6 = new Mjolnir.Forms.RenderingHintLabel();
            this.labelBreak = new Mjolnir.Forms.RenderingHintLabel();
            this.labelWorkingHours = new Mjolnir.Forms.RenderingHintLabel();
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.panelDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // panelDetails
            // 
            this.panelDetails.BackColor = System.Drawing.Color.Transparent;
            this.panelDetails.Controls.Add(this.label10Time);
            this.panelDetails.Controls.Add(this.label10);
            this.panelDetails.Controls.Add(this.label8Time);
            this.panelDetails.Controls.Add(this.label8);
            this.panelDetails.Controls.Add(this.label7Time);
            this.panelDetails.Controls.Add(this.label7);
            this.panelDetails.Controls.Add(this.label6Time);
            this.panelDetails.Controls.Add(this.label6);
            this.panelDetails.Controls.Add(this.labelBreak);
            this.panelDetails.Controls.Add(this.labelWorkingHours);
            this.panelDetails.Controls.Add(this.pictureBoxLogo);
            this.panelDetails.Location = new System.Drawing.Point(221, 50);
            this.panelDetails.Name = "panelDetails";
            this.panelDetails.Size = new System.Drawing.Size(512, 750);
            this.panelDetails.TabIndex = 0;
            // 
            // label10Time
            // 
            this.label10Time.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label10Time.Location = new System.Drawing.Point(255, 644);
            this.label10Time.Name = "label10Time";
            this.label10Time.Size = new System.Drawing.Size(120, 50);
            this.label10Time.TabIndex = 3;
            this.label10Time.Text = "00:00";
            this.label10Time.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label10Time.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label10.Location = new System.Drawing.Point(163, 644);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(90, 50);
            this.label10.TabIndex = 3;
            this.label10.Text = "10h";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label10.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // label8Time
            // 
            this.label8Time.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label8Time.Location = new System.Drawing.Point(255, 594);
            this.label8Time.Name = "label8Time";
            this.label8Time.Size = new System.Drawing.Size(120, 50);
            this.label8Time.TabIndex = 3;
            this.label8Time.Text = "00:00";
            this.label8Time.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label8Time.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label8.Location = new System.Drawing.Point(163, 594);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(90, 50);
            this.label8.TabIndex = 3;
            this.label8.Text = "8h";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label8.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // label7Time
            // 
            this.label7Time.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label7Time.Location = new System.Drawing.Point(255, 544);
            this.label7Time.Name = "label7Time";
            this.label7Time.Size = new System.Drawing.Size(120, 50);
            this.label7Time.TabIndex = 3;
            this.label7Time.Text = "00:00";
            this.label7Time.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label7Time.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label7.Location = new System.Drawing.Point(163, 544);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 50);
            this.label7.TabIndex = 3;
            this.label7.Text = "7h";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label7.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // label6Time
            // 
            this.label6Time.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label6Time.Location = new System.Drawing.Point(255, 494);
            this.label6Time.Name = "label6Time";
            this.label6Time.Size = new System.Drawing.Size(120, 50);
            this.label6Time.TabIndex = 3;
            this.label6Time.Text = "00:00";
            this.label6Time.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label6Time.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label6.Location = new System.Drawing.Point(162, 494);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 50);
            this.label6.TabIndex = 3;
            this.label6.Text = "6h";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label6.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // labelBreak
            // 
            this.labelBreak.Font = new System.Drawing.Font("Segoe UI", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelBreak.Location = new System.Drawing.Point(0, 379);
            this.labelBreak.Name = "labelBreak";
            this.labelBreak.Size = new System.Drawing.Size(512, 80);
            this.labelBreak.TabIndex = 2;
            this.labelBreak.Text = "00:00";
            this.labelBreak.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelBreak.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // labelWorkingHours
            // 
            this.labelWorkingHours.Font = new System.Drawing.Font("Segoe UI", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelWorkingHours.Location = new System.Drawing.Point(0, 259);
            this.labelWorkingHours.Name = "labelWorkingHours";
            this.labelWorkingHours.Size = new System.Drawing.Size(512, 120);
            this.labelWorkingHours.TabIndex = 1;
            this.labelWorkingHours.Text = "00:00";
            this.labelWorkingHours.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelWorkingHours.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            // 
            // pictureBoxLogo
            // 
            this.pictureBoxLogo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBoxLogo.BackgroundImage")));
            this.pictureBoxLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBoxLogo.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(512, 256);
            this.pictureBoxLogo.TabIndex = 0;
            this.pictureBoxLogo.TabStop = false;
            // 
            // DetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(914, 800);
            this.Controls.Add(this.panelDetails);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(600, 800);
            this.Name = "DetailForm";
            this.Opacity = 0.9D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "YAKKA";
            this.TopMost = true;
            this.Shown += new System.EventHandler(this.DetailForm_Shown);
            this.Click += new System.EventHandler(this.DetailForm_Click);
            this.Resize += new System.EventHandler(this.DetailForm_Resize);
            this.panelDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelDetails;
        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private Mjolnir.Forms.RenderingHintLabel labelWorkingHours;
        private Mjolnir.Forms.RenderingHintLabel label6Time;
        private Mjolnir.Forms.RenderingHintLabel label6;
        private Mjolnir.Forms.RenderingHintLabel labelBreak;
        private Mjolnir.Forms.RenderingHintLabel label10Time;
        private Mjolnir.Forms.RenderingHintLabel label10;
        private Mjolnir.Forms.RenderingHintLabel label8Time;
        private Mjolnir.Forms.RenderingHintLabel label8;
        private Mjolnir.Forms.RenderingHintLabel label7Time;
        private Mjolnir.Forms.RenderingHintLabel label7;
    }
}