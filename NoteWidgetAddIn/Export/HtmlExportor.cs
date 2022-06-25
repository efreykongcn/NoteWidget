// Copyright (c) Efrey Kong. All Rights Reserved.
// Licensed under the Apache License, Version 2.0.

using System.IO;
using NoteWidgetAddIn.Markdown;
using NoteWidgetAddIn.Model;

namespace NoteWidgetAddIn.Export
{
    internal class HtmlExportor : CustomFileExportor
    {
        public HtmlExportor(ExportFormat fileFormat) : base(fileFormat) 
        {
            
        }
        protected override StreamWriter BeginCreateFile(string file)
        {
            var writer = File.CreateText(file);
            var title = Path.GetFileNameWithoutExtension(file);
            var header = HtmlTemplate.OnlineResourceTemplate.ToHead(title);
            writer.WriteLine(header);
            writer.Flush();
            return writer;
        }
        protected override void WriteFileContent(StreamWriter writer, NotePage page, string title = null)
        {
            if (!string.IsNullOrEmpty(title))
            {
                writer.WriteLine("<hr>");
                writer.WriteLine($"<h1>{title}</h1>");
                writer.WriteLine("<hr>");
            }
            writer.WriteLine(MarkdownHelper.MarkdownToHtml(page.ContentInnerText));
            writer.WriteLine("<br/><br/>");
            writer.Flush();
        }

        protected override void EndCreateFile(StreamWriter writer)
        {
            writer.WriteLine(HtmlTemplate.OnlineResourceTemplate.ToFoot());
            writer.Flush();
        }
    }
}
