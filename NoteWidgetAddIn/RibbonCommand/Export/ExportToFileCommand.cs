// Copyright (c) Efrey Kong. All Rights Reserved.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NoteWidgetAddIn.Model;

namespace NoteWidgetAddIn.RibbonCommand
{
    internal class ExportToFileCommand : Command
    {
        public override async Task ExecuteAsync(params object[] args)
        {
            if (args.Length > 0)
            {
                if (Enum.TryParse<NodeType>(args[0].ToString(), out var scope))
                {
                    NoteNode node;
                    using (var app = Context.CreateApplication())
                    {
                        var nodeID = scope == NodeType.Notebook ? app.CurrentNotebookID :
                            (scope == NodeType.SectionGroup ? app.CurrentSectionGroupID :
                            (scope == NodeType.Section ? app.CurrentSectionID : app.CurrentPageID));
                        node = app.GetNoteNodeHierarchy(nodeID);
                    }
                    var availableFormats = ExportHelper.GetAvailableExportFormats(scope);
                    await this.SingleThreadedInvoke(() =>
                    {
                        var dialog = new SaveFileDialog();
                        var recommendedFileName = PathHelper.MakeValidFileName(node.Name) + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                        dialog.FileName = recommendedFileName;
                        dialog.Filter = GetExportFormatFilter(availableFormats);
                        dialog.OverwritePrompt = true;

                        if (dialog.ShowDialog(OwnerWin32Window) == DialogResult.OK)
                        {
                            var format = availableFormats[dialog.FilterIndex - 1];
                            var exportFileName = dialog.FileName;
                            IExportor exportor = ExportFactory.CreateExportor(Context, format);
                            exportor.ExportNodeToSingleFile(node.ID, exportFileName);
                        }
                    });
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

        private string GetExportFormatFilter(ExportFormat[] formats)
        {
            StringBuilder sb = new StringBuilder();
            if (formats != null)
            {
                foreach (var format in formats)
                {
                    var desc = format.GetDescription();
                    var pattern = ExportHelper.GetExportFormatExtPattern(format);
                    if (sb.Length == 0)
                    {
                        sb.Append(desc + "|" + pattern);
                    }
                    else
                    {
                        sb.Append("|" + desc + "|" + pattern);
                    }
                }
            }
            return sb.ToString();
        }
    }
}
