// Copyright (c) Efrey Kong. All Rights Reserved.
// Licensed under the Apache License, Version 2.0.

using System;
using NoteWidget.Markdown;

namespace NoteWidget.AddIn
{
    public class WidgetSettingsManager
    {
        private WidgetSettingsManager(){}
        private static WidgetSettingsManager _instance;
        public static WidgetSettingsManager Current
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new WidgetSettingsManager();
                }
                return _instance;
            }
        }
        public bool AlwaysPreviewInSameWindow { get; set; }
        public HighlightTheme PreviewHighlightTheme { get; set; }
        public ColorScheme PreviewColorScheme { get; set; }
        public void LoadSettings()
        {
            var settings = Properties.Settings.Default;
            AlwaysPreviewInSameWindow = settings.Markdown_Preview_Singleton;
            PreviewColorScheme = Enum.TryParse<ColorScheme>(settings.Markdown_ColorScheme, true, out var scheme) ? scheme : ColorScheme.System;
            PreviewHighlightTheme = Enum.TryParse<HighlightTheme>(settings.Markdown_HighlightTheme, true, out var theme) ? theme : HighlightTheme.Default;
        }

        public void Unload()
        {
            _instance = null;
        }

        public void SaveSettings()
        {
            var settings = Properties.Settings.Default;
            settings.Markdown_Preview_Singleton = AlwaysPreviewInSameWindow;
            settings.Markdown_ColorScheme = PreviewColorScheme.ToString();
            settings.Markdown_HighlightTheme = PreviewHighlightTheme.ToString();
            settings.Save();
        }
    }
}
