// Copyright (c) Efrey Kong. All Rights Reserved.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NoteWidgetAddIn.RibbonCommand.Markdown;
using NoteWidgetAddIn.Markdown;

namespace NoteWidgetAddIn.RibbonCommand
{
    internal class PreviewMarkdownCommand : Command
    {
        private static IDictionary<int, WebBrowserWindow> windowContainer = new Dictionary<int, WebBrowserWindow>();
        public override async Task ExecuteAsync(params object[] args)
        {
            var settings = Properties.Settings.Default;
            if (TryGetPageContent(out var htmlContent))
            {
                if (settings.Markdown_Preview_Singleton && windowContainer.Count > 0)
                {
                    var window = windowContainer.First().Value;
                    window.Dispatcher.Invoke(() =>
                    {
                        window.BrowserHtmlContent = htmlContent;
                        window.RefreshBrowser();
                    });
                }
                else
                {
                    await WpfAddInApplication.Current.BeginInvoke(() =>
                    {
                        var window = new WebBrowserWindow();
                        window.RememberMeIdentifier = "Markdown_Preview";
                        window.BrowserHtmlContent = htmlContent;
                        var helper = new System.Windows.Interop.WindowInteropHelper(window);
                        helper.Owner = OwnerWin32Window.Handle;
                        window.Closed += (s, e) =>
                        {
                            var key = window.GetHashCode();
                            if (windowContainer.ContainsKey(key))
                            {
                                windowContainer.Remove(key);
                            }
                        };
                        windowContainer.Add(window.GetHashCode(), window);
                        window.Show();
                    });
                }
            }
            await Task.Yield();
        }
        private bool TryGetPageContent(out string htmlContent)
        {
            try
            {
                string title, markdownText;
                using (var app = Context.CreateApplication())
                {
                    var page = app.GetCurrentNotePage();
                    title = page.Title.InnerText;
                    markdownText = page.ContentInnerText;
                }
                var htmlBody = MarkdownHelper.MarkdownToHtml(markdownText);
                htmlContent = HtmlTemplate.LocalResourceTemplate.ToHtml(title, htmlBody);
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            htmlContent = null;
            return false;
        }
    }
}
