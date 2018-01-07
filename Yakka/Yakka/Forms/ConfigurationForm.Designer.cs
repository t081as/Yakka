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
    partial class ConfigurationForm
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
            this.buttonAccept = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.dateTimePickerStart = new System.Windows.Forms.DateTimePicker();
            this.comboBoxCalculator = new System.Windows.Forms.ComboBox();
            this.smoothLabelCalculator = new Yakka.Utility.SmoothLabel();
            this.smoothLabelStart = new Yakka.Utility.SmoothLabel();
            this.SuspendLayout();
            // 
            // buttonAccept
            // 
            this.buttonAccept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonAccept.Location = new System.Drawing.Point(261, 65);
            this.buttonAccept.Name = "buttonAccept";
            this.buttonAccept.Size = new System.Drawing.Size(75, 23);
            this.buttonAccept.TabIndex = 0;
            this.buttonAccept.Text = "OK";
            this.buttonAccept.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(342, 65);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // dateTimePickerStart
            // 
            this.dateTimePickerStart.CustomFormat = "dd.MM.yyyy HH:mm";
            this.dateTimePickerStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerStart.Location = new System.Drawing.Point(87, 12);
            this.dateTimePickerStart.Name = "dateTimePickerStart";
            this.dateTimePickerStart.Size = new System.Drawing.Size(330, 20);
            this.dateTimePickerStart.TabIndex = 4;
            // 
            // comboBoxCalculator
            // 
            this.comboBoxCalculator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCalculator.FormattingEnabled = true;
            this.comboBoxCalculator.Location = new System.Drawing.Point(87, 38);
            this.comboBoxCalculator.Name = "comboBoxCalculator";
            this.comboBoxCalculator.Size = new System.Drawing.Size(330, 21);
            this.comboBoxCalculator.TabIndex = 7;
            // 
            // smoothLabelCalculator
            // 
            this.smoothLabelCalculator.AutoSize = true;
            this.smoothLabelCalculator.Location = new System.Drawing.Point(12, 41);
            this.smoothLabelCalculator.Name = "smoothLabelCalculator";
            this.smoothLabelCalculator.Size = new System.Drawing.Size(54, 13);
            this.smoothLabelCalculator.TabIndex = 6;
            this.smoothLabelCalculator.Text = "Calculator";
            // 
            // smoothLabelStart
            // 
            this.smoothLabelStart.AutoSize = true;
            this.smoothLabelStart.Location = new System.Drawing.Point(12, 18);
            this.smoothLabelStart.Name = "smoothLabelStart";
            this.smoothLabelStart.Size = new System.Drawing.Size(29, 13);
            this.smoothLabelStart.TabIndex = 5;
            this.smoothLabelStart.Text = "Start";
            // 
            // ConfigurationForm
            // 
            this.AcceptButton = this.buttonAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(429, 101);
            this.Controls.Add(this.comboBoxCalculator);
            this.Controls.Add(this.smoothLabelCalculator);
            this.Controls.Add(this.smoothLabelStart);
            this.Controls.Add(this.dateTimePickerStart);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonAccept);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigurationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuration";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConfigurationForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonAccept;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.DateTimePicker dateTimePickerStart;
        private Utility.SmoothLabel smoothLabelStart;
        private Utility.SmoothLabel smoothLabelCalculator;
        private System.Windows.Forms.ComboBox comboBoxCalculator;
    }
}