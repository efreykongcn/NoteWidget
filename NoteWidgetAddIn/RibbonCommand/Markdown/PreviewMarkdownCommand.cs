// Copyright (c) Efrey Kong. All Rights Reserved.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NoteWidgetAddIn.Markdown;
using NoteWidgetAddIn.Model;
using NoteWidgetAddIn.RibbonCommand.Markdown;

namespace NoteWidgetAddIn.RibbonCommand
{
    internal class PreviewMarkdownCommand : Command
    {
        #region PreviewWindowHolder
        class PreviewWindowHolder
        {
            public PreviewWindowHolder(string pageID, string pageLastModifiedTime, WebBrowserWindow previewWindow)
            {
                PageID = pageID;
                PageLastModifiedTime = pageLastModifiedTime;
                PreviewWindow = previewWindow;
            }

            public string PageID { get; set; }
            public string PageLastModifiedTime { get; set; }
            public WebBrowserWindow PreviewWindow { get; }
        }
        #endregion

        private static Dictionary<int, PreviewWindowHolder> _windowContainer = new Dictionary<int, PreviewWindowHolder>();
        public override async Task ExecuteAsync(params object[] args)
        {
            await PreviewCurrentNotePage(false);
        }

        private async Task PreviewCurrentNotePage(bool isRefresh)
        { 
            var settings = Properties.Settings.Default;
            if (TryGetCurrentNotePage(out var notePage))
            {
                var existedHolder = _windowContainer.Select(c => c.Value).FirstOrDefault(c => c.PageID == notePage.PageID);
                if (existedHolder != null)
                {
                    if (existedHolder.PageLastModifiedTime == notePage.LastModifiedTime)
                    {
                        return;
                    }
                    existedHolder.PageLastModifiedTime = notePage.LastModifiedTime;
                    var htmlContent = GetHtmlContent(notePage);
                    existedHolder.PreviewWindow.Dispatcher.Invoke(() =>
                    {
                        existedHolder.PreviewWindow.BrowserHtmlContent = htmlContent;
                        existedHolder.PreviewWindow.RefreshBrowser();
                    });
                }
                else if (settings.Markdown_Preview_Singleton && _windowContainer.Count > 0)
                {
                    var holder = _windowContainer.First().Value;
                    holder.PageID = notePage.PageID;
                    holder.PageLastModifiedTime = notePage.LastModifiedTime;
                    var htmlContent = GetHtmlContent(notePage);
                    holder.PreviewWindow.Dispatcher.Invoke(() =>
                    {
                        holder.PreviewWindow.BrowserHtmlContent = htmlContent;
                        holder.PreviewWindow.RefreshBrowser();
                    });
                }
                else
                {
                    var htmlContent = GetHtmlContent(notePage);
                    await WpfAddInApplication.Current.BeginInvoke(() =>
                    {
                        var window = new WebBrowserWindow();
                        window.RememberMeIdentifier = "Markdown_Preview";
                        window.BrowserHtmlContent = htmlContent;
                        var helper = new System.Windows.Interop.WindowInteropHelper(window);
                        helper.Owner = OwnerWin32Window.Handle;

                        var interval = settings.Markdown_PreviewRefresh_Interval;
                        window.InitTimer(new TimeSpan(0, 0, interval), () =>
                        {
                            Task.Run(async () =>
                            {
                                await PreviewCurrentNotePage(isRefresh: true);
                            });
                        });

                        window.KeyDown += (s, e) =>
                        {
                            //Refresh
                            if (e.Key == System.Windows.Input.Key.F5)
                            {
                                Task.Run(async () =>
                                {
                                    await PreviewCurrentNotePage(isRefresh: true);
                                });
                            }
                        };

                        window.Closed += (s, e) =>
                        {
                            var key = window.GetHashCode();
                            if (_windowContainer.ContainsKey(key))
                            {
                                _windowContainer.Remove(key);
                            }
                        };

                        _windowContainer.Add(window.GetHashCode(), new PreviewWindowHolder(notePage.PageID, notePage.LastModifiedTime, window));
                        window.Show();
                    });
                }
            }
            await Task.Yield();
        }

        private string GetHtmlContent(NotePage page)
        {
            var htmlBody = MarkdownHelper.MarkdownToHtml(page.ContentInnerText);
            return HtmlTemplate.LocalResourceTemplate.ToHtml(page.Title.InnerText, htmlBody);
        }

        private bool TryGetCurrentNotePage(out NotePage currentNotePage)
        {
            try
            {
                using (var app = Context.CreateApplication())
                {
                    currentNotePage = app.GetCurrentNotePage();
                }
                if (currentNotePage == null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            currentNotePage = null;
            return false;
        }

        //private static IDictionary<int, WebBrowserWindow> windowContainer = new Dictionary<int, WebBrowserWindow>();
        //public override async Task ExecuteAsync(params object[] args)
        //{
        //    var settings = Properties.Settings.Default;
        //    if (TryGetPageContent(out var htmlContent))
        //    {
        //        if (settings.Markdown_Preview_Singleton && windowContainer.Count > 0)
        //        {
        //            var window = windowContainer.First().Value;
        //            window.Dispatcher.Invoke(() =>
        //            {
        //                window.BrowserHtmlContent = htmlContent;
        //                window.RefreshBrowser();
        //            });
        //        }
        //        else
        //        {
        //            await WpfAddInApplication.Current.BeginInvoke(() =>
        //            {
        //                var window = new WebBrowserWindow();
        //                window.RememberMeIdentifier = "Markdown_Preview";
        //                window.BrowserHtmlContent = htmlContent;
        //                var helper = new System.Windows.Interop.WindowInteropHelper(window);
        //                helper.Owner = OwnerWin32Window.Handle;
        //                window.Closed += (s, e) =>
        //                {
        //                    var key = window.GetHashCode();
        //                    if (windowContainer.ContainsKey(key))
        //                    {
        //                        windowContainer.Remove(key);
        //                    }
        //                };
        //                windowContainer.Add(window.GetHashCode(), window);
        //                window.Show();
        //            });
        //        }
        //    }
        //    await Task.Yield();
        //}
        //private bool TryGetPageContent(out string htmlContent)
        //{
        //    try
        //    {
        //        string title, markdownText;
        //        using (var app = Context.CreateApplication())
        //        {
        //            var page = app.GetCurrentNotePage();
        //            title = page.Title.InnerText;
        //            markdownText = page.ContentInnerText;
        //        }
        //        var htmlBody = MarkdownHelper.MarkdownToHtml(markdownText);
        //        htmlContent = HtmlTemplate.LocalResourceTemplate.ToHtml(title, htmlBody);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(ex);
        //    }
        //    htmlContent = null;
        //    return false;
        //}
    }
}
