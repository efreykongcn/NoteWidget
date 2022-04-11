// Copyright (c) Efrey Kong. All Rights Reserved.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using System.Text;
using NoteWidgetAddIn.Model;

namespace NoteWidgetAddIn.Export
{
    internal abstract class AbstractExportor : IExportor
    {
        private NoteApplicationContext _context;
        public AbstractExportor(ExportFormat fileFormat)
        {
            FileFormat = fileFormat;
        }
        public ExportFormat FileFormat { get; private set; }
        protected NoteApplication NoteApp { get; private set; }

        public string FileExtension
        {
            get
            {
                return ExportHelper.GetExportFormatFileExtension(FileFormat);
            }
        }
        public void SetContext(NoteApplicationContext context)
        {
            _context = context;
            NoteApp = _context.CreateApplication();
        }

        public abstract void ExportNodeToSingleFile(string nodeID, string filePath);

        protected abstract void CreatePageFile(string pageID, string file);

        public virtual string ExportNodeToHierarchicalFiles(string nodeID, string exportPath, bool createsHierarchicalFolder = true)
        {
            ExceptionAssertion.ThrowArgumentNullExceptionIfNull(nodeID, nameof(nodeID));

            if (createsHierarchicalFolder)
            {
                var rootNode = NoteApp.GetNoteNodeHierarchy(nodeID);
                if (rootNode != null)
                {
                    return ExportFileHierarchyRecursively(rootNode, exportPath); 
                }
                return null;
            }
            else
            {
                string rootPath;
                var rootNode = NoteApp.GetNoteNodeHierarchy(nodeID);
                if (rootNode != null)
                {
                    var folderName = $"OneNote_{rootNode.Name}_{DateTime.Now.ToString("yyyyMMddHHmmss")}";
                    rootPath = PathHelper.MakeUniqueFolderName(Path.Combine(exportPath, folderName));
                    Directory.CreateDirectory(rootPath);
                    foreach (var node in rootNode.Descendants(n => n.NodeType == NodeType.Page))
                    {
                        var filePath = Path.Combine(rootPath, GetFullPathNodeName(node, '_') + FileExtension);
                        CreatePageFile(node.ID, PathHelper.MakeUniqueFileName(filePath));
                    }
                    return rootPath;
                }
                return null;
            }
        }

        private string ExportFileHierarchyRecursively(NoteNode parentNode, string hierarchyFolderPath)
        {
            if (parentNode.NodeType == NodeType.Page)
            {
                var file = Path.Combine(hierarchyFolderPath, PathHelper.MakeValidFileName(parentNode.Name) + FileExtension);
                CreatePageFile(parentNode.ID, PathHelper.MakeUniqueFileName(file));
            }
            string currentFolderPath;
            if (parentNode.NodeType != NodeType.Page || (parentNode.NodeType == NodeType.Page && parentNode.Children.Count > 0))
            {
                currentFolderPath = PathHelper.MakeUniqueFolderName(Path.Combine(hierarchyFolderPath, PathHelper.MakeValidFileName(parentNode.Name)));
                Directory.CreateDirectory(currentFolderPath);
            }
            else
            {
                currentFolderPath = hierarchyFolderPath;
            }
            foreach (var childNode in parentNode.Children)
            {
                ExportFileHierarchyRecursively(childNode, currentFolderPath);
            }

            return currentFolderPath;
        }

        protected string GetFullPathNodeName(NoteNode node, char seperatorChar)
        {
            var fileNameBuilder = new StringBuilder();
            NoteNode tmp = node;
            while (tmp != null)
            {
                var nodeName = PathHelper.MakeValidFileName(tmp.Name);
                if (fileNameBuilder.Length == 0)
                {
                    fileNameBuilder.Append(nodeName);
                }
                else
                {
                    fileNameBuilder.Insert(0, nodeName + seperatorChar.ToString());
                }
                tmp = tmp.Parent;
            }
            return fileNameBuilder.ToString();
        }
    }
}
