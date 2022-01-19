// Copyright (c) Efrey Kong. All Rights Reserved.
// Licensed under the Apache License, Version 2.0.

namespace NoteWidgetAddIn
{
    public interface IExportor
    {
        void SetContext(NoteApplicationContext context);
        /// <summary>
        /// Gets export file format
        /// </summary>
        ExportFormat FileFormat { get; }
        /// <summary>
        /// Export all OneNote pages under specified node to a single file.
        /// </summary>
        /// <param name="nodeID">A Notebook, Section Group, Section or Page ID</param>
        /// <param name="filePath">File name with absolute path</param>
        /// <returns>the file name of created single file.</returns>
        void ExportNodeToSingleFile(string nodeID, string filePath);
        /// <summary>
        /// Export OneNote pages under specified node, Each page a file.
        /// </summary>
        /// <param name="nodeID">A Notebook, Section Group, Section or Page ID</param>
        /// <param name="exportPath">A absolute path which export file(s) will be created to</param>
        /// <param name="createsHierarchicalFolder">Creates folder for each Notebook/Section Group/Section</param>
        /// <returns>the path contains exported files</returns>
        string ExportNodeToHierarchicalFiles(string nodeID, string exportPath, bool createsHierarchicalFolder = true);
    }
}
