using Microsoft.VisualStudio.TestTools.UnitTesting;
using NoteWidgetAddIn.Model;
using System.Xml.Linq;

namespace NoteWidgetAddIn
{
    [TestClass]
    public abstract class BaseDummyTest
    {
        protected NoteApplicationContext DummyContext { get; private set; }
        [TestInitialize]
        public void Setup()
        {
            DummyContext = new NoteApplicationContext(typeof(DummyApplication));
        }

        [TestCleanup]
        public void Teardown()
        {
        }

        protected XDocument GetDummyNotebookXDoc()
        {
            return XDocument.Parse(DummyData.GetTargetedFileXmlContent(NodeType.Notebook));
        }

        protected XDocument GetDummyExpectedNotebookXDoc()
        {
            return XDocument.Parse(DummyData.GetExpectedFileXmlContent(NodeType.Notebook));
        }
    }
}
