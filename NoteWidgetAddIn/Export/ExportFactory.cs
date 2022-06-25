// Copyright (c) Efrey Kong. All Rights Reserved.
// Licensed under the Apache License, Version 2.0.

using NoteWidgetAddIn.Export;

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
