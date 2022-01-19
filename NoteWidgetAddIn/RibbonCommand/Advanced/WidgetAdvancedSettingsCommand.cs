// Copyright (c) Efrey Kong. All Rights Reserved.
// Licensed under the Apache License, Version 2.0.

using System.Threading.Tasks;
using System.Windows.Interop;
using NoteWidgetAddIn.RibbonCommand.Advanced;

namespace NoteWidgetAddIn.RibbonCommand
{
    internal class WidgetAdvancedSettingsCommand : Command
    {
        public override async Task ExecuteAsync(params object[] args)
        {
            var dialog = new AdvancedSettingsDialog();
            dialog.ShowDialog(OwnerWin32Window);
            await Task.Yield();
        }
    }
}
