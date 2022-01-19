// Copyright (c) Efrey Kong. All Rights Reserved.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NoteWidgetAddIn.Model;

namespace NoteWidgetAddIn.RibbonCommand
{
    public partial class ExportToPathDialog : Form
    {
        public ExportToPathDialog(NodeType nodeType)
        {
            InitializeComponent();
            NodeType = nodeType;
            Load += ExportToPathDialog_Load;            
        }

        private void ExportToPathDialog_Load(object sender, EventArgs e)
        {
            tbSelectedPath.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            var formats = ExportHelper.GetAvailableExportFormats(NodeType);
            cmbFileFormat.DisplayMember = "Text";
            cmbFileFormat.ValueMember = "Value";
            var dataSource = new ArrayList();
            foreach (var f in formats)
            {
                dataSource.Add(new { Value = f, Text = f.GetDescription() });
            }
            cmbFileFormat.DataSource = dataSource;
            cmbFileFormat.SelectedIndex = 0;
        }

        #region Export Options
        public NodeType NodeType { get; }
        public string SelectedPath => tbSelectedPath.Text;
        public ExportFormat ExportFormat => (ExportFormat)cmbFileFormat.SelectedValue;
        #endregion

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(SelectedPath) && Directory.Exists(SelectedPath))
            {
                Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnBrowseFolder_Click(object sender, EventArgs e)
        {
            this.SingleThreadedInvoke(() =>
            {
                using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
                {
                    if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.SelectedPath))
                    {
                        tbSelectedPath.Text = dialog.SelectedPath;
                    }
                }
            });
        }
    }
}
