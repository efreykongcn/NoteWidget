using NoteWidgetAddIn.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NoteWidgetAddIn
{
    internal class DummyData
    {
        private static readonly IDictionary<NodeType, string> Files = new Dictionary<NodeType, string>()
        {
            {NodeType.Notebook, "NotebookHierarchy.xml" },
            {NodeType.SectionGroup, "SectionGroupHierarchy.xml" },
            {NodeType.Section, "SectionHierarchy.xml" },
            {NodeType.Page, "PageHierarchy.xml" }
        };

        private static readonly IDictionary<NodeType, string> ExpectedFiles = new Dictionary<NodeType, string>()
        {
            {NodeType.Notebook, "NotebookHierarchy_Expected.xml" },
            {NodeType.SectionGroup, "SectionGroupHierarchy_Expected.xml" },
            {NodeType.Section, "SectionHierarchy_Expected.xml" },
            {NodeType.Page, "PageHierarchy.xml" }
        };
        /// <summary>
        /// Returns file xml format content
        /// </summary>
        /// <param name="nodeType"></param>
        /// <returns></returns>
        public static string GetTargetedFileXmlContent(NodeType nodeType)
        {
            return GetFileContent(Files[nodeType]);
        }
        public static string GetExpectedFileXmlContent(NodeType nodeType)
        {
            return GetFileContent(ExpectedFiles[nodeType]);
        }
        public static string GetFileContent(string fileName)
        {
            var filePath = GetDummyFilePath(fileName);
            return File.ReadAllText(filePath);
        }
        public static string GetDummyFilePath(string fileName)
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return Path.Combine(path, "DummyData", fileName);
        }
    }
}
