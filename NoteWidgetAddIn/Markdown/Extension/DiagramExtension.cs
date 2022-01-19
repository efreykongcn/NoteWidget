// Copyright (c) Efrey Kong. All Rights Reserved.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteWidgetAddIn.Markdown.Extension
{
    internal class DiagramExtension : ITemplateExtension
    {
        internal class DiagramRender : ITemplateRender
        {
            public ColorScheme ColorScheme { get; set; }
            public TemplateResourceType ResourceType { get; set; }
            public void Render(HtmlTemplate template)
            {
                string mermaidJsUrl;
                if (ResourceType == TemplateResourceType.Local)
                {
                    mermaidJsUrl = "http://notewidget-vitual-host/resources/js/mermaid.min.js";
                }
                else
                {
                    mermaidJsUrl = "https://cdn.jsdelivr.net/npm/mermaid/dist/mermaid.min.js";
                }
                template.PostScripts.Add($"<script src=\"{mermaidJsUrl}\"></script>");

                if (ColorScheme == ColorScheme.Light)
                {
                    template.PostScripts.Add("<script>try { mermaid.initialize({ 'securityLevel': 'loose', 'theme': 'forest', startOnLoad: true }); } catch (e) { }</script>");
                }
                else if (ColorScheme == ColorScheme.Dark)
                {
                    template.PostScripts.Add("<script>try { mermaid.initialize({ 'securityLevel': 'loose', 'theme': 'dark', startOnLoad: true }); } catch (e) { }</script>");
                }
                else //Use System Settings
                {
                    template.PostScripts.Add("<script>if (window.matchMedia && window.matchMedia('(prefers-color-scheme: dark)').matches){ try { mermaid.initialize({ 'securityLevel': 'loose', 'theme': 'dark', startOnLoad: true }); } catch (e) { } } else {try { mermaid.initialize({ 'securityLevel': 'loose', 'theme': 'forest', startOnLoad: true }); } catch (e) { }}</script>");
                }
            }
        }
        public void Setup(HtmlTemplateBuilder builder, TemplateResourceType resourceType)
        {
            var scheme = Enum.TryParse<ColorScheme>(Properties.Settings.Default.Markdown_ColorScheme ?? String.Empty, out var result) ? result : ColorScheme.System;
            builder.Renders.Add(new DiagramRender { ResourceType = resourceType, ColorScheme = scheme });
        }
    }
}
