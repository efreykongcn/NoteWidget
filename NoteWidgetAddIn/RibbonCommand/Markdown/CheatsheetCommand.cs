// Copyright (c) Efrey Kong. All Rights Reserved.
// Licensed under the Apache License, Version 2.0.

using System.Threading.Tasks;
using NoteWidgetAddIn.Markdown;
using NoteWidgetAddIn.RibbonCommand.Markdown;

namespace NoteWidgetAddIn.RibbonCommand
{
    internal class CheatsheetCommand : Command
    {
        private static bool _windowOpened = false;
        public override async Task ExecuteAsync(params object[] args)
        {
            if (!_windowOpened)
            {
                var body = System.IO.File.ReadAllText($"{PathHelper.GetWidgetRootPath()}/resources/MarkdownCheatSheet.html");
                await WpfAddInApplication.Current.BeginInvoke(() =>
                {
                    var window = new WebBrowserWindow();
                    window.RememberMeIdentifier = "Markdown_CheatSheet";
                    window.BrowserHtmlContent = HtmlTemplate.LocalResourceTemplate.ToHtml("Markdown Cheat sheet", body);
                    var helper = new System.Windows.Interop.WindowInteropHelper(window);
                    helper.Owner = OwnerWin32Window.Handle;
                    window.Closed += (s, e) =>
                    {
                        _windowOpened = false;
                    };
                    window.Show();
                    _windowOpened = true;
                });
            }
            await Task.Yield();
        }
    }
}
