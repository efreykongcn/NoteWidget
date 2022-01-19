// Copyright (c) Efrey Kong. All Rights Reserved.
// Licensed under the Apache License, Version 2.0.

namespace NoteWidgetAddIn.RibbonCommand.Advanced
{
    partial class AdvancedSettingsDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdvancedSettingsDialog));
            this.label1 = new System.Windows.Forms.Label();
            this.cmbColorScheme = new System.Windows.Forms.ComboBox();
            this.cbSameWindowPreview = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cmbHighlightTheme = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabWidget = new System.Windows.Forms.TabControl();
            this.tabPreviewPage = new System.Windows.Forms.TabPage();
            this.tabWidget.SuspendLayout();
            this.tabPreviewPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Color Scheme: ";
            // 
            // cmbColorScheme
            // 
            this.cmbColorScheme.DropDownHeight = 100;
            this.cmbColorScheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbColorScheme.DropDownWidth = 250;
            this.cmbColorScheme.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.cmbColorScheme.FormattingEnabled = true;
            this.cmbColorScheme.IntegralHeight = false;
            this.cmbColorScheme.Location = new System.Drawing.Point(147, 12);
            this.cmbColorScheme.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmbColorScheme.Name = "cmbColorScheme";
            this.cmbColorScheme.Size = new System.Drawing.Size(250, 28);
            this.cmbColorScheme.TabIndex = 1;
            // 
            // cbSameWindowPreview
            // 
            this.cbSameWindowPreview.AutoSize = true;
            this.cbSameWindowPreview.Location = new System.Drawing.Point(147, 86);
            this.cbSameWindowPreview.Name = "cbSameWindowPreview";
            this.cbSameWindowPreview.Size = new System.Drawing.Size(247, 23);
            this.cbSameWindowPreview.TabIndex = 3;
            this.cbSameWindowPreview.Text = "Always preview in the same window";
            this.cbSameWindowPreview.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Location = new System.Drawing.Point(315, 190);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 30);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(425, 190);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 30);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // cmbHighlightTheme
            // 
            this.cmbHighlightTheme.DropDownHeight = 100;
            this.cmbHighlightTheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHighlightTheme.DropDownWidth = 250;
            this.cmbHighlightTheme.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.cmbHighlightTheme.FormattingEnabled = true;
            this.cmbHighlightTheme.IntegralHeight = false;
            this.cmbHighlightTheme.Items.AddRange(new object[] {
            "GitHub Flavored Markdown"});
            this.cmbHighlightTheme.Location = new System.Drawing.Point(147, 49);
            this.cmbHighlightTheme.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmbHighlightTheme.Name = "cmbHighlightTheme";
            this.cmbHighlightTheme.Size = new System.Drawing.Size(250, 28);
            this.cmbHighlightTheme.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 54);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 19);
            this.label2.TabIndex = 5;
            this.label2.Text = "Highlight Theme: ";
            // 
            // tabWidget
            // 
            this.tabWidget.Controls.Add(this.tabPreviewPage);
            this.tabWidget.Location = new System.Drawing.Point(4, 4);
            this.tabWidget.Name = "tabWidget";
            this.tabWidget.SelectedIndex = 0;
            this.tabWidget.Size = new System.Drawing.Size(516, 175);
            this.tabWidget.TabIndex = 7;
            // 
            // tabPreviewPage
            // 
            this.tabPreviewPage.BackColor = System.Drawing.Color.White;
            this.tabPreviewPage.Controls.Add(this.cmbHighlightTheme);
            this.tabPreviewPage.Controls.Add(this.label1);
            this.tabPreviewPage.Controls.Add(this.label2);
            this.tabPreviewPage.Controls.Add(this.cmbColorScheme);
            this.tabPreviewPage.Controls.Add(this.cbSameWindowPreview);
            this.tabPreviewPage.Location = new System.Drawing.Point(4, 28);
            this.tabPreviewPage.Name = "tabPreviewPage";
            this.tabPreviewPage.Padding = new System.Windows.Forms.Padding(3);
            this.tabPreviewPage.Size = new System.Drawing.Size(508, 143);
            this.tabPreviewPage.TabIndex = 0;
            this.tabPreviewPage.Text = "Markdown";
            // 
            // AdvancedSettingsDialog
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(523, 241);
            this.Controls.Add(this.tabWidget);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AdvancedSettingsDialog";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Advanced Widget Setting";
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.tabWidget.ResumeLayout(false);
            this.tabPreviewPage.ResumeLayout(false);
            this.tabPreviewPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbColorScheme;
        private System.Windows.Forms.CheckBox cbSameWindowPreview;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cmbHighlightTheme;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabControl tabWidget;
        private System.Windows.Forms.TabPage tabPreviewPage;
    }
}
