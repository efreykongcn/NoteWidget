// Copyright (c) Efrey Kong. All Rights Reserved.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteWidgetAddIn.Markdown.Extension
{
    internal class ColorSchemeExtension : ITemplateExtension
    {
        internal class ColorSchemeRender : ITemplateRender
        {
            public ColorScheme ColorScheme { get; set; }
            public TemplateResourceType ResourceType { get; set; }
            public void Render(HtmlTemplate template)
            {
                string githubCssFileName;
                string schemeStyleVar;
                if  (ColorScheme == ColorScheme.Light)
                {
                    githubCssFileName = "github-markdown-light.css";
                    schemeStyleVar = "body{--color-previewwindow-default: #ffffff;}.notewidget-footer{--color-fg-default:#24292f;--color-canvas-default:#ffffff}";
                }
                else if (ColorScheme == ColorScheme.Dark)
                {
                    githubCssFileName = "github-markdown-dark.css";
                    schemeStyleVar = "body{--color-previewwindow-default: #101010;}.markdown-body code {color: #d63384;font-size: .875em;}.notewidget-footer{--color-fg-default:#c9d1d9;--color-canvas-default:#202020;}::-webkit-scrollbar { width: 10px; height: 10px;}::-webkit-scrollbar-button {  background-color: #666; }::-webkit-scrollbar-track {  background-color: #202020;}::-webkit-scrollbar-track-piece { background-color: #000;}::-webkit-scrollbar-thumb { height: 50px; background-color: #666; border-radius: 3px;}::-webkit-scrollbar-corner { background-color: #202020;}::-webkit-resizer { background-color: #666;}";
                }
                else //Use System Settings
                {
                    githubCssFileName = "github-markdown.min.css";
                    schemeStyleVar = "@media (prefers-color-scheme: dark) {body{--color-previewwindow-default: #202020;}.markdown-body code {color: #d63384;font-size: .875em;}.notewidget-footer{--color-fg-default:#c9d1d9;--color-canvas-default:#0d1117;}::-webkit-scrollbar { width: 10px; height: 10px;}::-webkit-scrollbar-button {  background-color: #666; }::-webkit-scrollbar-track {  background-color: #202020;}::-webkit-scrollbar-track-piece { background-color: #000;}::-webkit-scrollbar-thumb { height: 50px; background-color: #666; border-radius: 3px;}::-webkit-scrollbar-corner { background-color: #202020;}::-webkit-resizer { background-color: #666;}}@media(prefers-color-scheme: light){body{--color-previewwindow-default: #ffffff;}.notewidget-footer{--color-fg-default:#24292f;--color-canvas-default:#ffffff}}";
                }
                string schemeStyle = "<style>" + schemeStyleVar + "html,body{margin:0;flex-direction: column;background-color: var(--color-previewwindow-default) !important;height:100%;}.notewidget-footer{bottom: 0px;width: 100%;text-align:center;color: var(--color-fg-default);background-color: var(--color-canvas-default);font-family: -apple-system,BlinkMacSystemFont,\"Segoe UI\",Helvetica,Arial,sans-serif,\"Apple Color Emoji\",\"Segoe UI Emoji\";font-size: 16px;line-height: 1.5;}</style>";

                string gitHubCssUrl;
                if (ResourceType == TemplateResourceType.Local)
                {
                    gitHubCssUrl = $"http://notewidget-vitual-host/resources/css/{githubCssFileName}";
                }
                else
                {
                    gitHubCssUrl = $"https://cdn.jsdelivr.net/npm/github-markdown-css/{githubCssFileName}";
                }
                template.Stylesheets.Add($"<link href=\"{gitHubCssUrl}\" rel=\"stylesheet\" />");
                template.Stylesheets.Add(schemeStyle);
            }
        }
        public void Setup(HtmlTemplateBuilder builder, TemplateResourceType resourceType)
        {
            var scheme = Enum.TryParse<ColorScheme>(Properties.Settings.Default.Markdown_ColorScheme ?? String.Empty, out var result) ? result : ColorScheme.System;
            var render = new ColorSchemeRender { ColorScheme = scheme, ResourceType = resourceType };
            builder.Renders.Add(render);
        }
    }
}
