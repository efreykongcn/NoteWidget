// Copyright (c) Efrey Kong. All Rights Reserved.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using System.Text;
using System.Xml.Linq;
using Microsoft.Office.Interop.OneNote;
using NoteWidgetAddIn.Export;
using NoteWidgetAddIn.Model;

namespace NoteWidgetAddIn
{
    public class ExportFactory
    {
        /// <summary>
        /// Creates an instance from IExportor
        /// </summary>
        /// <param name="format"></param>
        /// <param name="exportFilePath"></param>
        /// <returns></returns>
        public static IExportor CreateExportor(NoteApplicationContext context,ExportFormat format)
        {
            IExportor exportor;
            if (format == ExportFormat.Markdown)
            {
                exportor = new MarkdownExportor(format);
            }
            else if (format == ExportFormat.Html)
            {
                exportor = new HtmlExportor(format);
            }
            else
            {
                exportor = new OneNoteHostedExporter(format);
            }
            exportor.SetContext(context);

            return exportor;
        }
    }
}
