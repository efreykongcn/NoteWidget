// Copyright (c) Efrey Kong. All Rights Reserved.
// Licensed under the Apache License, Version 2.0.

using System.ComponentModel;
using Microsoft.Office.Interop.OneNote;
using NoteWidgetAddIn.Model;

#pragma warning disable CS3016
namespace NoteWidgetAddIn
{
    public enum ExportFormat
    {
        [Description("PDF(*.pdf)")]
        [RestrictedNodeType(NodeType.Notebook, NodeType.Section, NodeType.Page)]
        PDF = PublishFormat.pfPDF,
        [Description("XPS Document(*.xps)")]
        [RestrictedNodeType(NodeType.Notebook, NodeType.Section, NodeType.Page)]
        XPS = PublishFormat.pfXPS,
        [Description("Single File Web Page(*.mht)")]
        [RestrictedNodeType(NodeType.Section)]
        MHTML = PublishFormat.pfMHTML,
        [Description("Microsoft Word XML Document(*.docx)")]
        [RestrictedNodeType(NodeType.Section)]
        Word = PublishFormat.pfWord,
        [Description("Markdown Document(*.md)")]
        Markdown = 100,
        [Description("Html Document from markdown(*.html)")]
        Html = 101
    }
}
