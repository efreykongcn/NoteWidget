using Microsoft.Office.Interop.OneNote;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.ComponentModel.Design.Serialization;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

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

        [TestMethod]
        public void TestXDocument()
        {
            var xml = $@"<?xml version=""1.0"" encoding=""utf-8""?>
<package xmlns=""http://schemas.microsoft.com/packaging/2013/05/nuspec.xsd"">
  <metadata>
    <id>Markdig</id>
    <version>0.27.0</version>
    <authors>Alexandre Mutel</authors>
    <license type=""expression"">BSD-2-Clause</license>
    <licenseUrl>https://licenses.nuget.org/BSD-2-Clause</licenseUrl>
    <icon>markdig.png</icon>
    <readme>readme.md</readme>
    <projectUrl>https://github.com/lunet-io/markdig</projectUrl>
    <description>A fast, powerful, CommonMark compliant, extensible Markdown processor for .NET with 20+ builtin extensions (pipetables, footnotes, definition lists... etc.)</description>
    <releaseNotes>https://github.com/lunet-io/markdig/blob/master/changelog.md</releaseNotes>
    <copyright>Alexandre Mutel</copyright>
    <tags>Markdown CommonMark md html md2html</tags>
    <repository type=""git"" url=""https://github.com/xoofx/markdig"" commit=""5e3527b7d2c103eef66834177e63565089af9534"" />
    <dependencies>
      <group targetFramework="".NETFramework4.5.2"">
        <dependency id=""System.Memory"" version=""4.5.4"" exclude=""Build,Analyzers"" />
      </group>
      <group targetFramework="".NETCoreApp2.1"" />
      <group targetFramework="".NETCoreApp3.1"" />
      <group targetFramework="".NETStandard2.0"">
        <dependency id=""System.Memory"" version=""4.5.4"" exclude=""Build,Analyzers"" />
      </group>
      <group targetFramework="".NETStandard2.1"" />
    </dependencies>
  </metadata>
</package>";
            var xe = XElement.Parse(xml);
            var ns = xe.Name.Namespace;
            Console.WriteLine(xe.Element(ns + "metadata").Value);
            Console.WriteLine(xe.Element(ns + "metadata").Element(ns + "id").Value);
        }
    }
}
