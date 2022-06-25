using Microsoft.VisualStudio.TestTools.UnitTesting;
using NoteWidgetAddIn.Model;
using System;
using System.IO;
using System.Linq;

namespace NoteWidgetAddIn
{
    [TestClass]
    public class DummyDataExportTest : BaseDummyTest
    {
        #region Custom Export
        [TestMethod]
        public void TestCustomExportToFile()
        {
            var dummyNoteApp = DummyContext.CreateApplication();
            var node = dummyNoteApp.GetNoteNodeHierarchy(dummyNoteApp.CurrentNotebookID);

            DoTestCustomExportToFile(node, ExportFormat.Markdown);
            DoTestCustomExportToFile(node, ExportFormat.Html);
        }
        private void DoTestCustomExportToFile(NoteNode node, ExportFormat format)
        {
            var nodeID = node.ID;
            string exportPath = Path.GetTempPath();
            var file = Path.Combine(exportPath, Guid.NewGuid().ToString() + ExportHelper.GetExportFormatFileExtension(format));
            IExportor exportor = ExportFactory.CreateExportor(DummyContext, format);
            exportor.ExportNodeToSingleFile(nodeID, file);

            Assert.IsTrue(File.Exists(file));
            string fileExtension = ExportHelper.GetExportFormatFileExtension(format);
            Assert.AreEqual(Path.GetExtension(file), fileExtension);

            var content = File.ReadAllText(file);
            File.Delete(file);

            Assert.IsTrue(!string.IsNullOrEmpty(content));
            Console.Write(content);
        }

        [TestMethod]
        public void TestExportToPath()
        {
            DoTestExportToPath(ExportFormat.Html);
            DoTestExportToPath(ExportFormat.Markdown);
        }
        private void DoTestExportToPath(ExportFormat fileFormat)
        {
            var dummyNoteApp = DummyContext.CreateApplication();
            var nodeID = dummyNoteApp.CurrentNotebookID;
            string exportPath = Path.GetTempPath();

            IExportor exportor = ExportFactory.CreateExportor(DummyContext, fileFormat);
            var path = exportor.ExportNodeToHierarchicalFiles(nodeID, exportPath);

            Assert.IsTrue(Directory.Exists(path));
            Console.WriteLine(path);
            var di = new DirectoryInfo(path);

            var xeExpected = GetDummyExpectedNotebookXDoc();
            FileInfo[] fs;
            fs = di.GetFiles("*.*", SearchOption.AllDirectories);
            DirectoryInfo[] ds = di.GetDirectories("*", SearchOption.AllDirectories);
            var dlist = xeExpected.Descendants().Where(e => e.Name.LocalName != "Page" && e.Name.LocalName != "Notebook").Select(e => e.Attribute("name").Value).ToList();
            foreach (var d in dlist)
            {
                Assert.IsTrue(ds.Any(e => e.Name == d));
            }

            string fileExtension = ExportHelper.GetExportFormatFileExtension(fileFormat);

            var plist = xeExpected.Descendants(xeExpected.Root.Name.Namespace + "Page").Select(e => e.Attribute("name").Value + fileExtension).ToList();
            Assert.AreEqual(fs.Length, plist.Count);
            foreach (var f in fs)
            {
                Assert.IsTrue(plist.Any(n => f.Name.EndsWith(n)));
                Console.WriteLine(f.Name);
            }

            Directory.Delete(path, true);
        }
        #endregion

        [TestMethod]
        public void TestGetExportFormatExtPattern()
        {
            Assert.AreEqual("*.md", ExportHelper.GetExportFormatExtPattern(ExportFormat.Markdown));
            Assert.AreEqual("*.docx", ExportHelper.GetExportFormatExtPattern(ExportFormat.Word));
            Assert.AreEqual("*.html", ExportHelper.GetExportFormatExtPattern(ExportFormat.Html));
            Assert.AreEqual("*.mht", ExportHelper.GetExportFormatExtPattern(ExportFormat.MHTML));
            Assert.AreEqual("*.pdf", ExportHelper.GetExportFormatExtPattern(ExportFormat.PDF));
        }
        [TestMethod]
        public void TestHtmlExport()
        {
            var dummyNoteApp = DummyContext.CreateApplication();
            var exportor = ExportFactory.CreateExportor(DummyContext, ExportFormat.Html);
            var pageID = dummyNoteApp.CurrentPageID;
            var filePath = Path.Combine(Path.GetTempPath(), $"test{DateTime.Now.Ticks}.html");
            exportor.ExportNodeToSingleFile(pageID, filePath);
            Assert.IsTrue(File.Exists(filePath));
        }
    }
}
