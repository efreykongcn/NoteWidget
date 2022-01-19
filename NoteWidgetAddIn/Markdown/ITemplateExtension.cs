// Copyright (c) Efrey Kong. All Rights Reserved.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteWidgetAddIn.Markdown
{
    public enum TemplateResourceType
    {
        Local,
        Online
    }
    public enum ColorScheme
    {
        /// <summary>
        /// Follow system theme. Default theme.
        /// </summary>
        [Description("Use system setting")]
        System = 0,
        [Description("Light")]
        Light = 1,
        [Description("Dark")]
        Dark = 2
    }
    /// <summary>
    /// Source code hight theme
    /// </summary>
    public enum HighlightTheme
    {
        Default,
        Coy,
        Dark,
        Funky,
        Okaidia,
        Solarizedlight,
        Tomorrow,
        Twilight
    }
    public interface ITemplateExtension
    {
        void Setup(HtmlTemplateBuilder builder, TemplateResourceType resourceType);
    }
}
