// Copyright (c) Efrey Kong. All Rights Reserved.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NLog;
using NoteWidgetAddIn.Markdown;

namespace NoteWidgetAddIn.RibbonCommand.Advanced
{
    public partial class AdvancedSettingsDialog : Form
    {
        private static ILogger _logger = LogManager.GetCurrentClassLogger();
        public AdvancedSettingsDialog()
        {
            InitializeComponent();
            Load += AdvancedSettingsDialog_Load;
            btnSave.Click += BtnSave_Click;
            btnCancel.Click += BtnCancel_Click;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var settings = Properties.Settings.Default;
                settings.Markdown_Preview_Singleton = cbSameWindowPreview.Checked;
                settings.Markdown_ColorScheme = cmbColorScheme.SelectedValue.ToString();
                settings.Markdown_HighlightTheme = cmbHighlightTheme.SelectedValue.ToString();
                settings.Save();
                HtmlTemplate.BuildDefaultTemplates();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            Close();
        }

        private void AdvancedSettingsDialog_Load(object sender, EventArgs e)
        {
            RestoreSettings();
        }
        private void RestoreSettings()
        {
            try
            {
                var settings = Properties.Settings.Default;
                cbSameWindowPreview.Checked = settings.Markdown_Preview_Singleton;
                //
                var schemes = (ColorScheme[])Enum.GetValues(typeof(ColorScheme));
                cmbColorScheme.DisplayMember = "Text";
                cmbColorScheme.ValueMember = "Value";
                var dataSource = new ArrayList();
                foreach(var e in schemes)
                {
                    dataSource.Add(new { Value = e, Text = e.GetDescription() });
                }
                cmbColorScheme.DataSource = dataSource;
                cmbColorScheme.SelectedValue = Enum.Parse(typeof(ColorScheme), settings.Markdown_ColorScheme);
                //
                var themes = (HighlightTheme[])Enum.GetValues(typeof(HighlightTheme));
                cmbHighlightTheme.DisplayMember = "Text";
                cmbHighlightTheme.ValueMember = "Value";
                var dataSource2 = new ArrayList();
                foreach(var e in themes)
                {
                    dataSource2.Add(new {Value = e, Text = e.GetDescription()});
                }
                cmbHighlightTheme.DataSource = dataSource2;
                cmbHighlightTheme.SelectedValue = Enum.Parse(typeof(HighlightTheme), settings.Markdown_HighlightTheme);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }
    }
}
