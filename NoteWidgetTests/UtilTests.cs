using Microsoft.Office.Interop.OneNote;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.ComponentModel.Design.Serialization;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NoteWidgetAddIn
{
    [TestClass]
    public class UtilTests
    {
        [TestMethod]
        public void TestGetFileExtension()
        {
            var ext = ExportHelper.GetExportFormatFileExtension(ExportFormat.Markdown);
            Assert.AreEqual(".md", ext);
        }

        [TestMethod]
        public void TestAvailableExportFormats()
        {
            var formats = ExportHelper.GetAvailableExportFormats(Model.NodeType.Notebook);
            Assert.IsTrue(formats.Length == 4);

            formats = ExportHelper.GetAvailableExportFormats(Model.NodeType.Section);
            Assert.IsTrue(formats.Length == 6);
        }
    }
}
