// Copyright (c) Efrey Kong. All Rights Reserved.
// Licensed under the Apache License, Version 2.0.

using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Microsoft.Office.Core;
using NoteWidgetAddIn.RibbonCommand;

#pragma warning disable CS3001 // Argument type is not CLS-compliant
namespace NoteWidgetAddIn
{
    public partial class AddIn : IRibbonExtensibility
    {

        #region implements IRibbonExtensibility
        /// <summary>
        /// Gets the defined ribbon tab xml string content
        /// This method is triggered after OnAddInsUpdate
        /// </summary>
        /// <param name="RibbonID"></param>
        /// <returns>The xml string content which customized ribbon menu items</returns>
        public string GetCustomUI(string RibbonID)
        {
            return Properties.Resources.ribbon;
        }
        #endregion

        #region Custom Ribbon methods
        public IStream GetImage(string imageName)
        {
            var ms = new MemoryStream();
            BindingFlags flags = BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            var b = typeof(Properties.Resources).GetProperty(Path.GetFileNameWithoutExtension(imageName), flags).GetValue(null, null) as Bitmap;
            b.Save(ms, ImageFormat.Png);
            return new CCOMStreamWrapper(ms);
        }

        #endregion

        #region Ribbon Action Commands
        /// <summary>
        /// Export to file
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        public async Task ExportFileCmd(IRibbonControl control) => await _commandFactory.Run<ExportToFileCommand>(control.Tag);

        /// <summary>
        /// Export to hierarchical files
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        public async Task ExportPathCmd(IRibbonControl control) => await _commandFactory.Run<ExportToPathCommand>(control.Tag, control.Context);

        /// <summary>
        /// View current page's markdown content as Html in a new window.
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        public async Task PreviewMarkdownCmd(IRibbonControl control) => await _commandFactory.Run<PreviewMarkdownCommand>();        
        /// <summary>
        /// Show markdown cheat sheet window
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        public async Task MarkdownCheatsheetCmd(IRibbonControl control) => await _commandFactory.Run<CheatsheetCommand>();
        /// <summary>
        /// Show advanced settings window
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        public async Task WidgetAdvancedSettingsCmd(IRibbonControl control) => await _commandFactory.Run<WidgetAdvancedSettingsCommand>();

        #endregion
    }
}
