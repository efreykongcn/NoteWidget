// Copyright (c) Efrey Kong. All Rights Reserved.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.IO;
using NoteWidgetAddIn.Model;

namespace NoteWidgetAddIn.Export
{
    internal abstract class CustomFileExportor : AbstractExportor
    {
        protected CustomFileExportor(ExportFormat fileFormat) : base(fileFormat) { }
        public override void ExportNodeToSingleFile(string nodeID, string filePath)
        {
            ExceptionAssertion.ThrowArgumentNullExceptionIfNull(nodeID, nameof(nodeID));
            ExceptionAssertion.ThrowArgumentNullExceptionIfNull(filePath, nameof(filePath));

            var rootNode = NoteApp.GetNoteNodeHierarchy(nodeID);
            if (rootNode != null)
            {
                using (var writer = BeginCreateFile(filePath))
                {
                    foreach (var node in rootNode.Descendants(n => n.NodeType == NodeType.Page))
                    {
                        WriteFileContent(writer, NoteApp.GetNotePage(node.ID), GetFullPathNodeName(node, '\\'));
                    }
                    EndCreateFile(writer);
                }
            }
        }

        protected override void CreatePageFile(string pageID, string file)
        {
            var page = NoteApp.GetNotePage(pageID);
            using (var writer = BeginCreateFile(file))
            {
                WriteFileContent(writer, page); 
                EndCreateFile(writer);
            }
        }

        protected abstract StreamWriter BeginCreateFile(string file);
        protected abstract void WriteFileContent(StreamWriter writer, NotePage page, string title = null);
        protected abstract void EndCreateFile(StreamWriter writer);
    }
}
