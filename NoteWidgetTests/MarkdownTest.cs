﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using NoteWidgetAddIn.Markdown;
using NoteWidgetAddIn.RibbonCommand.Markdown;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

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
        [TestMethod]
        public void TestWebView2()
        {
            var browser = new WebBrowserWindow();
            browser.BrowserHtmlContent = "<html><head></head><body><div id=\"content\"><span>Hello world!</span></div></body></html>";
            browser.webBrowser.NavigationCompleted += async delegate {
                var result = await browser.webBrowser.ExecuteScriptAsync("document.getElementById('content').innerHTML;");
                Console.WriteLine(result);
                await browser.webBrowser.ExecuteScriptAsync("document.getElementById('content').outerHTML = '<span>I am changed</span>';");
            };

            browser.Show();
            var app = new System.Windows.Application();
            app.MainWindow = browser;
            app.Run();
        }
    }
}
