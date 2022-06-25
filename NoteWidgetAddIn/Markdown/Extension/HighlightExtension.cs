// Copyright (c) Efrey Kong. All Rights Reserved.
// Licensed under the Apache License, Version 2.0.

using System;

namespace NoteWidgetAddIn.Markdown.Extension
{
    internal class HighlightExtension : ITemplateExtension
    {
        internal class HighlightTemplateRender : ITemplateRender
        {
            public HighlightTheme HighlightTheme { get; set; }
            public TemplateResourceType ResourceType { get; set; }
            public void Render(HtmlTemplate template)
            {
                string highlightCssFileName;
                if (HighlightTheme == HighlightTheme.Default)
                {
                    highlightCssFileName = "prism.css";
                }
                else
                {
                    highlightCssFileName = $"prism-{HighlightTheme.ToString().ToLower()}.min.css";
                }
                if (ResourceType == TemplateResourceType.Local)
                {
                    template.Stylesheets.Add($"<link href=\"http://notewidget-vitual-host/resources/css/{highlightCssFileName}\" rel=\"stylesheet\" />");
                    template.PostScripts.Add("<script src=\"http://notewidget-vitual-host/resources/js/prism.js\"></script>");
                }
                else
                {
                    template.Stylesheets.Add($"<link href=\"https://cdn.jsdelivr.net/npm/prismjs/themes/{highlightCssFileName}\" rel=\"stylesheet\" />");
                    template.PostScripts.Add("<script src=\"https://cdn.jsdelivr.net/npm/prismjs/prism.js\"></script>");
                    template.PostScripts.Add("<script src=\"https://cdn.jsdelivr.net/npm/prismjs/plugins/autoloader/prism-autoloader.min.js\"></script>");
                }
            }
        }
        public void Setup(HtmlTemplateBuilder builder, TemplateResourceType resourceType)
        {
            var theme = Enum.TryParse<HighlightTheme>(Properties.Settings.Default.Markdown_HighlightTheme ?? String.Empty, out var result) ? result : HighlightTheme.Default;
            var render = new HighlightTemplateRender { HighlightTheme = theme, ResourceType = resourceType };
            builder.Renders.Add(render);
        }
    }
}
