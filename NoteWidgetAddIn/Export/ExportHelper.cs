// Copyright (c) Efrey Kong. All Rights Reserved.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using NoteWidgetAddIn.Model;

namespace NoteWidgetAddIn
{
    public class ExportHelper
    {
        private static IDictionary<NodeType, ExportFormat[]> dictAvailableFormats = new Dictionary<NodeType, ExportFormat[]>(); 
        public static ExportFormat[] GetAvailableExportFormats(NodeType nodeType)
        {
            if (!dictAvailableFormats.ContainsKey(nodeType))
            {
                var result = new List<ExportFormat>();
                foreach (var e in (ExportFormat[])Enum.GetValues(typeof(ExportFormat)))
                {
                    var restricted = e.GetRestrictedNodeTypes();
                    if (restricted.Length == 0 || (e.GetRestrictedNodeTypes().Contains(nodeType)))
                    {
                        result.Add(e);
                    }
                }
                dictAvailableFormats.Add(nodeType, result.ToArray());
            }
            return dictAvailableFormats[nodeType];
        }

        public static string GetExportFormatExtPattern(ExportFormat format)
        {
            var desc = format.GetDescription();
            var match = Regex.Match(desc, @"\(([^)]*)\)");
            if (match.Success)
            {
                return match.Groups[1].Value;
            }
            throw new InvalidOperationException($"No extension described in {format.GetType()}.{format}");
        }

        public static string GetExportFormatFileExtension(ExportFormat format)
        {
            return Path.GetExtension(GetExportFormatExtPattern(format));
        }
    }
}
