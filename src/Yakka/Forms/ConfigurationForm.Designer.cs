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
            this.labelStartTime = new System.Windows.Forms.Label();
            this.dateTimeStartTime = new System.Windows.Forms.DateTimePicker();
            this.comboBoxCalculator = new System.Windows.Forms.ComboBox();
            this.labelCalculator = new System.Windows.Forms.Label();
            this.checkBoxManual = new System.Windows.Forms.CheckBox();
            this.maskedTextBoxManualBreak = new System.Windows.Forms.MaskedTextBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonApply = new System.Windows.Forms.Button();
            this.buttonConfirm = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelStartTime
            // 
            this.labelStartTime.AutoSize = true;
            this.labelStartTime.Location = new System.Drawing.Point(12, 18);
            this.labelStartTime.Name = "labelStartTime";
            this.labelStartTime.Size = new System.Drawing.Size(67, 15);
            this.labelStartTime.TabIndex = 0;
            this.labelStartTime.Text = "START TIME";
            // 
            // dateTimeStartTime
            // 
            this.dateTimeStartTime.CustomFormat = "dd.MM.yyyy HH:mm";
            this.dateTimeStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeStartTime.Location = new System.Drawing.Point(148, 12);
            this.dateTimeStartTime.Name = "dateTimeStartTime";
            this.dateTimeStartTime.Size = new System.Drawing.Size(300, 23);
            this.dateTimeStartTime.TabIndex = 1;
            // 
            // comboBoxCalculator
            // 
            this.comboBoxCalculator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCalculator.FormattingEnabled = true;
            this.comboBoxCalculator.Location = new System.Drawing.Point(148, 41);
            this.comboBoxCalculator.Name = "comboBoxCalculator";
            this.comboBoxCalculator.Size = new System.Drawing.Size(300, 23);
            this.comboBoxCalculator.TabIndex = 2;
            // 
            // labelCalculator
            // 
            this.labelCalculator.AutoSize = true;
            this.labelCalculator.Location = new System.Drawing.Point(12, 44);
            this.labelCalculator.Name = "labelCalculator";
            this.labelCalculator.Size = new System.Drawing.Size(79, 15);
            this.labelCalculator.TabIndex = 0;
            this.labelCalculator.Text = "CALCULATOR";
            // 
            // checkBoxManual
            // 
            this.checkBoxManual.AutoSize = true;
            this.checkBoxManual.Location = new System.Drawing.Point(12, 72);
            this.checkBoxManual.Name = "checkBoxManual";
            this.checkBoxManual.Size = new System.Drawing.Size(114, 19);
            this.checkBoxManual.TabIndex = 3;
            this.checkBoxManual.Text = "MANUAL BREAK";
            this.checkBoxManual.UseVisualStyleBackColor = true;
            this.checkBoxManual.CheckedChanged += new System.EventHandler(this.CheckBoxManual_CheckedChanged);
            // 
            // maskedTextBoxManualBreak
            // 
            this.maskedTextBoxManualBreak.Location = new System.Drawing.Point(148, 70);
            this.maskedTextBoxManualBreak.Mask = "90:00";
            this.maskedTextBoxManualBreak.Name = "maskedTextBoxManualBreak";
            this.maskedTextBoxManualBreak.ReadOnly = true;
            this.maskedTextBoxManualBreak.Size = new System.Drawing.Size(300, 23);
            this.maskedTextBoxManualBreak.TabIndex = 4;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(373, 99);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "CANCEL";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // buttonApply
            // 
            this.buttonApply.Location = new System.Drawing.Point(292, 99);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(75, 23);
            this.buttonApply.TabIndex = 5;
            this.buttonApply.Text = "APPLY";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.ButtonApply_Click);
            // 
            // buttonConfirm
            // 
            this.buttonConfirm.Location = new System.Drawing.Point(211, 99);
            this.buttonConfirm.Name = "buttonConfirm";
            this.buttonConfirm.Size = new System.Drawing.Size(75, 23);
            this.buttonConfirm.TabIndex = 5;
            this.buttonConfirm.Text = "OK";
            this.buttonConfirm.UseVisualStyleBackColor = true;
            this.buttonConfirm.Click += new System.EventHandler(this.ButtonConfirm_Click);
            // 
            // ConfigurationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 134);
            this.Controls.Add(this.buttonConfirm);
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.maskedTextBoxManualBreak);
            this.Controls.Add(this.checkBoxManual);
            this.Controls.Add(this.labelCalculator);
            this.Controls.Add(this.comboBoxCalculator);
            this.Controls.Add(this.dateTimeStartTime);
            this.Controls.Add(this.labelStartTime);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ConfigurationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "YAKKA CONFIGURATION";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelStartTime;
        private System.Windows.Forms.DateTimePicker dateTimeStartTime;
        private System.Windows.Forms.ComboBox comboBoxCalculator;
        private System.Windows.Forms.Label labelCalculator;
        private System.Windows.Forms.CheckBox checkBoxManual;
        private System.Windows.Forms.MaskedTextBox maskedTextBoxManualBreak;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.Button buttonConfirm;
    }
}