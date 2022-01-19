// Copyright (c) Efrey Kong. All Rights Reserved.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Interop;
using NoteWidgetAddIn.Model;

namespace NoteWidgetAddIn.RibbonCommand
{
    internal class ExportToPathCommand : Command
    {
        public override async Task ExecuteAsync(params object[] args)
        {
            if (args.Length > 0)
            {
                if (Enum.TryParse<NodeType>(args[0].ToString(), out var scope))
                {
                    string nodeID = null;
                    using (var app = Context.CreateApplication())
                    {
                        nodeID = scope == NodeType.Notebook ? app.CurrentNotebookID :
                            (scope == NodeType.SectionGroup ? app.CurrentSectionGroupID : app.CurrentSectionID);
                    }
                    try
                    {
                        var dialog = new ExportToPathDialog(scope);
                        if (dialog.ShowDialog(OwnerWin32Window) == DialogResult.OK)
                        {
                            var format = dialog.ExportFormat;
                            var exportPath = dialog.SelectedPath;
                            IExportor exportor = ExportFactory.CreateExportor(Context, format);
                            exportor.ExportNodeToHierarchicalFiles(nodeID, exportPath);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex);
                    }
                }
                else
                {
                    MessageBox.Show(OwnerWin32Window, "Invalid scope argument");
                    Logger.Error("ExportToPathCommand.ExecuteAsync: Invalid args!");
                }
            }
            else
            {
                MessageBox.Show(OwnerWin32Window, "\"scope\" argument required");
                Logger.Error("ExportToPathCommand.ExecuteAsync: Empty args!");
            }

            await Task.Yield();
        }
    }
}
