// Copyright (c) Efrey Kong. All Rights Reserved.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.OneNote;

namespace NoteWidgetAddIn.Export
{
    internal class OneNoteHostedExporter : AbstractExportor
    {
        public OneNoteHostedExporter(ExportFormat fileFormat) : base(fileFormat)
        {
        }

        public override void ExportNodeToSingleFile(string nodeID, string filePath)
        {
            ExceptionAssertion.ThrowArgumentNullExceptionIfNull(nodeID, nameof(nodeID));
            CreatePageFile(nodeID, filePath);
        }

        protected override void CreatePageFile(string nodeID, string file)
        {
            NoteApp.Publish(nodeID, (PublishFormat)FileFormat, file);
        }
    }
}
