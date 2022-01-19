using Microsoft.VisualStudio.TestTools.UnitTesting;
using NoteWidgetAddIn.RibbonCommand.Markdown;
using NoteWidgetAddIn.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;
using NoteWidgetAddIn.RibbonCommand.Advanced;
using NoteWidgetAddIn.Markdown;

namespace NoteWidgetAddIn
{
    [TestClass]
    public class MarkdownTest : BaseDummyTest
    {
        [TestMethod]
        public void TestUnique2()
        {
            Hashtable hashCodesSeen = new Hashtable(); 
            LinkedList<object> l = new LinkedList<object>();
            int n = 0;
            while (true)
            {
                var o = new WebBrowserWindow();
                // Remember objects so that they don't get collected.
                // This does not make any difference though :(
                l.AddFirst(o);
                o.BrowserHtmlContent = n.ToString();
                int hashCode = RuntimeHelpers.GetHashCode(o);
                n++;
                if (hashCodesSeen.ContainsKey(hashCode))
                {
                    // Same hashCode seen twice for DIFFERENT objects (n is as low as 5322).
                    Console.WriteLine("Hashcode seen twice: " + n + " (" + hashCode + ")");
                    break;
                }
                hashCodesSeen.Add(hashCode, null);
            }            
        }
        [TestMethod]
        public void TestTemplateToHtml()
        {
            var html = HtmlTemplate.OnlineResourceTemplate.ToHtml("My Title", "```csharp\r\n    Console.WriteLine(\"Hello world!\");\r\n```");
            var filePath = $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\MD_{Guid.NewGuid()}.html";
            using (var writer = System.IO.File.CreateText(filePath))
            {
                writer.WriteLine(html);
            }
            Assert.IsTrue(System.IO.File.Exists(filePath));
        }
    }
}
