using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NoteWidgetAddIn
{
    [TestClass]
    public abstract class BaseInteropTest
    {        
        protected NoteApplicationContext InteropContext { get; private set; }
        [TestInitialize]
        public void Setup()
        {
            InteropContext = new NoteApplicationContext();
        }

        [TestCleanup]
        public void Teardown()
        {
        }
    }
}
