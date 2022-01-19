// Copyright (c) Efrey Kong. All Rights Reserved.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using NoteWidgetAddIn.Model;

namespace NoteWidgetAddIn.Export
{
    internal class MarkdownExportor : CustomFileExportor
    {
        public MarkdownExportor(ExportFormat fileFormat) : base(fileFormat) {}

        protected override StreamWriter BeginCreateFile(string file)
        {
            return File.CreateText(file);
        }
        protected override void WriteFileContent(StreamWriter writer, NotePage page, string title = null)
        {
            if (!string.IsNullOrEmpty(title))
            {
                writer.WriteLine(new String('-', 50) + " ");
                writer.WriteLine(title + " ");
                writer.WriteLine(new String('-', 50) + " ");
            }
            writer.WriteLine(page.ContentInnerText);
            writer.WriteLine("\r\n");
            writer.Flush();
        }

        protected override void EndCreateFile(StreamWriter writer)
        {
            //Nothing to do
        }
    }
}
