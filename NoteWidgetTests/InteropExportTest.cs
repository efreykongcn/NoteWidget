using Microsoft.VisualStudio.TestTools.UnitTesting;
using NoteWidgetAddIn.RibbonCommand;
using NoteWidgetAddIn.RibbonCommand.Advanced;
using NoteWidgetAddIn.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NoteWidgetAddIn
{
    [TestClass]
    public class InteropExportTest : BaseInteropTest
    {
        #region Integrated with OneNote Export
        [TestMethod]
        public void TestIntegratedExportToFile()
        {
            var targetedNodes = new List<NoteNode>();
            var path = Path.Combine(Path.GetTempPath(), DateTime.Now.ToString("yyyyMMddHHmmss"));
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var interopNoteApp = InteropContext.CreateApplication();
            Assert.IsNotNull(interopNoteApp.CurrentPageID, "TestIntegratedExportToFile requires OneNote app running.");
            var list = interopNoteApp.GetAllNotebookHierarchy();
            var notebook = list.Last();
            targetedNodes.Add(notebook);
            targetedNodes.Add(notebook.Descendants(n => n.NodeType == NodeType.SectionGroup).First());
            targetedNodes.Add(notebook.Descendants(n => n.NodeType == NodeType.Section).First());
            targetedNodes.Add(notebook.Descendants(n => n.NodeType == NodeType.Page).First());

            foreach (var node in targetedNodes)
            {
                foreach (var format in ExportHelper.GetAvailableExportFormats(node.NodeType))
                {
                    DoTestIntegratedExportToFile(node, format, path);
                    Console.WriteLine($"Exported to File: NodeType: {node.NodeType}, format: {format}");
                }
            }
        }
        private void DoTestIntegratedExportToFile(NoteNode node, ExportFormat fileFormat, string exportPath)
        {
            var recommendedFileName = PathHelper.MakeValidFileName(node.Name) + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ExportHelper.GetExportFormatFileExtension(fileFormat);
            var file = Path.Combine(exportPath, recommendedFileName);
            IExportor exportor = ExportFactory.CreateExportor(InteropContext, fileFormat);
            exportor.ExportNodeToSingleFile(node.ID, file);
            Assert.IsTrue(File.Exists(file));
            Console.WriteLine(file);
            File.Delete(file);
        }

        [TestMethod]
        public void TestIntegratedExportToPath()
        {
            var targetedNodes = new List<NoteNode>();
            var path = Path.GetTempPath();
            var interopNoteApp = InteropContext.CreateApplication();
            Assert.IsNotNull(interopNoteApp.CurrentPageID, "TestIntegratedExportToPath requires OneNote app running.");
            var list = interopNoteApp.GetAllNotebookHierarchy();
            var notebook = list.Last();
            targetedNodes.Add(notebook);
            targetedNodes.Add(notebook.Descendants(n => n.NodeType == NodeType.SectionGroup).First());
            targetedNodes.Add(notebook.Descendants(n => n.NodeType == NodeType.Section).First());

            foreach (var node in targetedNodes)
            {
                Assert.IsNotNull(node);
                var toBeTestedFormats = ExportHelper.GetAvailableExportFormats(node.NodeType);
                foreach (var format in toBeTestedFormats)
                {
                    DoTestIntegratedExportToPath(node, format, path);
                    Console.WriteLine($"Exported to Path: NodeType: {node.NodeType}, format: {format}");
                }
            }
        }
        private void DoTestIntegratedExportToPath(NoteNode node, ExportFormat fileFormat, string exportPath)
        {
            IExportor exportor = ExportFactory.CreateExportor(InteropContext, fileFormat);
            var rootPath = exportor.ExportNodeToHierarchicalFiles(node.ID, exportPath);
            Assert.IsTrue(Directory.Exists(rootPath));
            Console.WriteLine($"{fileFormat}: {rootPath}");
            Directory.Delete(rootPath, true);
        }
        #endregion
    }
}
