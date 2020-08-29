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
    partial class AboutForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageInformation = new System.Windows.Forms.TabPage();
            this.tableLayoutPanelInformation = new System.Windows.Forms.TableLayoutPanel();
            this.labelDescription = new System.Windows.Forms.Label();
            this.labelVersion = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.labelName = new System.Windows.Forms.Label();
            this.labelCopyright = new System.Windows.Forms.Label();
            this.tabPageAuthors = new System.Windows.Forms.TabPage();
            this.tableLayoutPanelAuthors = new System.Windows.Forms.TableLayoutPanel();
            this.labelAuthorOrder = new System.Windows.Forms.Label();
            this.listBoxAuthors = new System.Windows.Forms.ListBox();
            this.tabPageLicense = new System.Windows.Forms.TabPage();
            this.textBoxLicense = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabPageInformation.SuspendLayout();
            this.tableLayoutPanelInformation.SuspendLayout();
            this.tabPageAuthors.SuspendLayout();
            this.tableLayoutPanelAuthors.SuspendLayout();
            this.tabPageLicense.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Controls.Add(this.pictureBoxLogo, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.tabControl, 0, 1);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 2;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 276F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(584, 561);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // pictureBoxLogo
            // 
            this.pictureBoxLogo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBoxLogo.BackgroundImage")));
            this.pictureBoxLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBoxLogo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxLogo.Location = new System.Drawing.Point(3, 3);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(578, 270);
            this.pictureBoxLogo.TabIndex = 0;
            this.pictureBoxLogo.TabStop = false;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageInformation);
            this.tabControl.Controls.Add(this.tabPageAuthors);
            this.tabControl.Controls.Add(this.tabPageLicense);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(3, 279);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(578, 279);
            this.tabControl.TabIndex = 1;
            // 
            // tabPageInformation
            // 
            this.tabPageInformation.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tabPageInformation.Controls.Add(this.tableLayoutPanelInformation);
            this.tabPageInformation.Location = new System.Drawing.Point(4, 24);
            this.tabPageInformation.Name = "tabPageInformation";
            this.tabPageInformation.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageInformation.Size = new System.Drawing.Size(570, 251);
            this.tabPageInformation.TabIndex = 0;
            this.tabPageInformation.Text = "INFORMATION";
            // 
            // tableLayoutPanelInformation
            // 
            this.tableLayoutPanelInformation.ColumnCount = 1;
            this.tableLayoutPanelInformation.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelInformation.Controls.Add(this.labelDescription, 0, 2);
            this.tableLayoutPanelInformation.Controls.Add(this.labelVersion, 0, 1);
            this.tableLayoutPanelInformation.Controls.Add(this.buttonClose, 0, 4);
            this.tableLayoutPanelInformation.Controls.Add(this.labelName, 0, 0);
            this.tableLayoutPanelInformation.Controls.Add(this.labelCopyright, 0, 3);
            this.tableLayoutPanelInformation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelInformation.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanelInformation.Name = "tableLayoutPanelInformation";
            this.tableLayoutPanelInformation.RowCount = 5;
            this.tableLayoutPanelInformation.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelInformation.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelInformation.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelInformation.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelInformation.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelInformation.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelInformation.Size = new System.Drawing.Size(564, 245);
            this.tableLayoutPanelInformation.TabIndex = 0;
            // 
            // labelDescription
            // 
            this.labelDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelDescription.Location = new System.Drawing.Point(3, 98);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(558, 49);
            this.labelDescription.TabIndex = 1;
            this.labelDescription.Text = "DESCRIPTION";
            this.labelDescription.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelVersion
            // 
            this.labelVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelVersion.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelVersion.Location = new System.Drawing.Point(3, 49);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(558, 49);
            this.labelVersion.TabIndex = 1;
            this.labelVersion.Text = "VERSION";
            this.labelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(486, 219);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 0;
            this.buttonClose.Text = "CLOSE";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.ButtonCloseClick);
            // 
            // labelName
            // 
            this.labelName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelName.Font = new System.Drawing.Font("Segoe UI Semibold", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelName.Location = new System.Drawing.Point(3, 0);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(558, 49);
            this.labelName.TabIndex = 1;
            this.labelName.Text = "NAME";
            this.labelName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelCopyright
            // 
            this.labelCopyright.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelCopyright.Location = new System.Drawing.Point(3, 147);
            this.labelCopyright.Name = "labelCopyright";
            this.labelCopyright.Size = new System.Drawing.Size(558, 49);
            this.labelCopyright.TabIndex = 2;
            this.labelCopyright.Text = "COPYRIGHT";
            this.labelCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabPageAuthors
            // 
            this.tabPageAuthors.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tabPageAuthors.Controls.Add(this.tableLayoutPanelAuthors);
            this.tabPageAuthors.Location = new System.Drawing.Point(4, 24);
            this.tabPageAuthors.Name = "tabPageAuthors";
            this.tabPageAuthors.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAuthors.Size = new System.Drawing.Size(570, 251);
            this.tabPageAuthors.TabIndex = 1;
            this.tabPageAuthors.Text = "AUTHORS";
            // 
            // tableLayoutPanelAuthors
            // 
            this.tableLayoutPanelAuthors.ColumnCount = 1;
            this.tableLayoutPanelAuthors.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelAuthors.Controls.Add(this.labelAuthorOrder, 0, 0);
            this.tableLayoutPanelAuthors.Controls.Add(this.listBoxAuthors, 0, 1);
            this.tableLayoutPanelAuthors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelAuthors.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanelAuthors.Name = "tableLayoutPanelAuthors";
            this.tableLayoutPanelAuthors.RowCount = 2;
            this.tableLayoutPanelAuthors.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelAuthors.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelAuthors.Size = new System.Drawing.Size(564, 245);
            this.tableLayoutPanelAuthors.TabIndex = 0;
            // 
            // labelAuthorOrder
            // 
            this.labelAuthorOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelAuthorOrder.Location = new System.Drawing.Point(3, 0);
            this.labelAuthorOrder.Name = "labelAuthorOrder";
            this.labelAuthorOrder.Size = new System.Drawing.Size(558, 30);
            this.labelAuthorOrder.TabIndex = 0;
            this.labelAuthorOrder.Text = "ORDER";
            this.labelAuthorOrder.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // listBoxAuthors
            // 
            this.listBoxAuthors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxAuthors.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.listBoxAuthors.FormattingEnabled = true;
            this.listBoxAuthors.ItemHeight = 15;
            this.listBoxAuthors.Location = new System.Drawing.Point(3, 33);
            this.listBoxAuthors.Name = "listBoxAuthors";
            this.listBoxAuthors.ScrollAlwaysVisible = true;
            this.listBoxAuthors.Size = new System.Drawing.Size(558, 209);
            this.listBoxAuthors.TabIndex = 1;
            this.listBoxAuthors.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.ListBoxAuthorsDrawItem);
            // 
            // tabPageLicense
            // 
            this.tabPageLicense.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tabPageLicense.Controls.Add(this.textBoxLicense);
            this.tabPageLicense.Location = new System.Drawing.Point(4, 24);
            this.tabPageLicense.Name = "tabPageLicense";
            this.tabPageLicense.Size = new System.Drawing.Size(570, 251);
            this.tabPageLicense.TabIndex = 2;
            this.tabPageLicense.Text = "LICENSE";
            // 
            // textBoxLicense
            // 
            this.textBoxLicense.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBoxLicense.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxLicense.Location = new System.Drawing.Point(0, 0);
            this.textBoxLicense.Multiline = true;
            this.textBoxLicense.Name = "textBoxLicense";
            this.textBoxLicense.ReadOnly = true;
            this.textBoxLicense.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxLicense.Size = new System.Drawing.Size(570, 251);
            this.textBoxLicense.TabIndex = 0;
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 561);
            this.Controls.Add(this.tableLayoutPanel);
            this.MinimumSize = new System.Drawing.Size(600, 600);
            this.Name = "AboutForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ABOUT";
            this.tableLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tabPageInformation.ResumeLayout(false);
            this.tableLayoutPanelInformation.ResumeLayout(false);
            this.tabPageAuthors.ResumeLayout(false);
            this.tableLayoutPanelAuthors.ResumeLayout(false);
            this.tabPageLicense.ResumeLayout(false);
            this.tabPageLicense.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageInformation;
        private System.Windows.Forms.TabPage tabPageAuthors;
        private System.Windows.Forms.TabPage tabPageLicense;
        private System.Windows.Forms.TextBox textBoxLicense;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelInformation;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelCopyright;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelAuthors;
        private System.Windows.Forms.Label labelAuthorOrder;
        private System.Windows.Forms.ListBox listBoxAuthors;
    }
}