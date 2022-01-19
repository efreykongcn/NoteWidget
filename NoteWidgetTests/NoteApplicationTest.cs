using Microsoft.VisualStudio.TestTools.UnitTesting;
using NoteWidgetAddIn.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace NoteWidgetAddIn
{
    [TestClass]
    public class NoteApplicationTest : BaseDummyTest
    {
        #region NoteApplication Hierarchy Node
        [TestMethod]
        public void TestHierarchicalNodesFromNotebook()
        {
            AssertNoteNode(DummyData.GetExpectedFileXmlContent(NodeType.Notebook), DummyData.GetTargetedFileXmlContent(NodeType.Notebook));
        }
        [TestMethod]
        public void TestHierarchicalNodesFromSectionGroup()
        {
            AssertNoteNode(DummyData.GetExpectedFileXmlContent(NodeType.SectionGroup), DummyData.GetTargetedFileXmlContent(NodeType.SectionGroup));
        }
        [TestMethod]
        public void TestHierarchicalNodesFromSection()
        {
            AssertNoteNode(DummyData.GetExpectedFileXmlContent(NodeType.Section), DummyData.GetTargetedFileXmlContent(NodeType.Section));
        }
        [TestMethod]
        public void TestHierarchicalNodesFromPage()
        {
            AssertNoteNode(DummyData.GetExpectedFileXmlContent(NodeType.Page), DummyData.GetTargetedFileXmlContent(NodeType.Page));
        }

        private void AssertNoteNode(string expectedFileXml, string targetedFileXml)
        {
            var list = NoteNode.HierarchicalNodesFrom(targetedFileXml);
            var xdocExpected = XDocument.Parse(expectedFileXml);
            Assert.IsTrue(list.Count == 1);
            var rootNode = list.First();
            AssertNoteNodeAttribute(xdocExpected.Root, rootNode);
            AssertNoteNodeChildren(xdocExpected.Root, rootNode);
        }

        private void AssertNoteNodeChildren(XElement expectedParent, NoteNode targetParent)
        {
            var expects = expectedParent.Descendants()
                                 .Where(e => e.Parent?.Attribute("ID")?.Value == expectedParent.Attribute("ID")?.Value
                                          && !(e.Attribute("isRecycleBin")?.Value == "true" || e.Attribute("isInRecycleBin")?.Value == "true"))
                                 .GetEnumerator();

            var targets = targetParent.Children.GetEnumerator();
            while (targets.MoveNext())
            {
                expects.MoveNext();
                var t = targets.Current;
                var e = expects.Current;

                AssertNoteNodeAttribute(e, t);
                AssertNoteNodeChildren(e, t);
            }
        }

        private void AssertNoteNodeAttribute(XElement expected, NoteNode target)
        {
            Assert.AreEqual(expected.Name.LocalName, target.NodeType.ToString());
            Assert.AreEqual(expected.Attribute("ID").Value, target.ID);
            Assert.AreEqual(expected.Attribute("name")?.Value, target.Name);
            Assert.AreEqual(expected.Attribute("lastModifiedTime")?.Value, target.LastModifiedTime);
            Assert.AreEqual(expected.Attribute("isCurrentlyViewed")?.Value ?? "False", target.IsCurrentlyViewed.ToString(), true);
            Assert.AreEqual(expected.Attribute("path")?.Value, target.Path);
            Assert.AreEqual(expected.Attribute("color")?.Value, target.Color);
            Assert.AreEqual(expected.Attribute("nickname")?.Value, target.Nickname);
            Assert.AreEqual(expected.Attribute("dateTime")?.Value, target.CreatedTime);
            Assert.AreEqual(expected.Attribute("pageLevel")?.Value ?? "0", target.PageLevel.ToString());
        }
        #endregion

        #region Extensions
        [TestMethod]
        public void TestPageNoteNodeDescendants()
        {
            var dummyNoteApp = DummyContext.CreateApplication();
            var root = dummyNoteApp.GetNoteNodeHierarchy(dummyNoteApp.CurrentNotebookID);
            Assert.IsNotNull(root);
            var targetCount = root.Descendants(n => n.NodeType == NodeType.Page).Count();

            var xe = GetDummyNotebookXDoc();
            var expectedCount = xe.Descendants(xe.Root.Name.Namespace + "Page")
                .Where(e => !(e.Attribute("isInRecycleBin")?.Value == "true"))
                .Count();

            Assert.AreEqual(expectedCount, targetCount);
        }

        [TestMethod]
        public void TestNoteNodeDescendants()
        {
            var dummyNoteApp = DummyContext.CreateApplication();
            var root = dummyNoteApp.GetNoteNodeHierarchy(dummyNoteApp.CurrentNotebookID);
            Assert.IsNotNull(root);
            var targetCount = root.Descendants().Count();

            var xdoc = GetDummyNotebookXDoc();
            var expectedCount = xdoc.Descendants()
                .Where(e => !(e.Attribute("isRecycleBin")?.Value == "true" || e.Attribute("isInRecycleBin")?.Value == "true"))
                .Count();

            Assert.AreEqual(expectedCount, targetCount);
        }
        #endregion
    }
}
