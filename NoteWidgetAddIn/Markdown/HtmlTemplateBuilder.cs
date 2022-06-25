// Copyright (c) Efrey Kong. All Rights Reserved.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;
using System.Linq;
using NoteWidgetAddIn.Markdown.Extension;

namespace NoteWidgetAddIn.Markdown
{
    public class HtmlTemplateBuilder
    {
        private HtmlTemplateBuilder(TemplateResourceType resourceType)
        {
            _extensions = new List<ITemplateExtension>();
            Renders = new List<ITemplateRender>();
            ResourceType = resourceType;
        }
        public TemplateResourceType ResourceType { get; }
        private List<ITemplateExtension> _extensions;
        public IEnumerable<ITemplateExtension> Extensions
        {
            get
            {
                return _extensions;
            }
        }
        public HtmlTemplateBuilder AddExtensionIfNotExists<T>() where T: ITemplateExtension, new()
        {
            if (!Contains<T>())
            {
                _extensions.Add(new T());
            }
            return this;
        }

        public bool Contains<T>()
        {
            return _extensions.Any(e => e is T);
        }

        public static HtmlTemplateBuilder CreateDefaultHtmlTemplateBuilder(TemplateResourceType resourceType)
        {
            return new HtmlTemplateBuilder(resourceType)
                       .AddExtensionIfNotExists<ColorSchemeExtension>()
                       .AddExtensionIfNotExists<HighlightExtension>()
                       .AddExtensionIfNotExists<DiagramExtension>();
        }

        public List<ITemplateRender> Renders { get; }

        public HtmlTemplate Build()
        {
            foreach(var extension in Extensions)
            {
                extension.Setup(this, ResourceType);
            }
            var template = new HtmlTemplate();
            foreach(var render in Renders)
            {
                render.Render(template);
            }
            return template;
        }
    }
}
