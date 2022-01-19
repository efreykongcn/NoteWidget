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
                template.PostScripts.Add("<script>try { mermaid.initialize({ startOnLoad: true }); } catch (e) { }</script>");
            }
        }
        public void Setup(HtmlTemplateBuilder builder, TemplateResourceType resourceType)
        {
            builder.Renders.Add(new DiagramRender { ResourceType = resourceType });
        }
    }
}
