// Copyright (c) Efrey Kong. All Rights Reserved.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Extensibility;
using NLog;
using NoteWidgetAddIn.RibbonCommand;

#pragma warning disable CS3008 // Type is not CLS-compliant
#pragma warning disable CS3003 // Type is not CLS-compliant
#pragma warning disable CS3001 // Type is not CLS-compliant
namespace NoteWidgetAddIn
{
    [ComVisible(true)]
    [Guid("EEE896F2-39B1-4D71-8A54-3EFDFB48BB06"), ProgId("NoteWidget.AddIn")]
    public partial class AddIn : IDTExtensibility2
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private CommandFactory _commandFactory;
        static AddIn()
        {
            var nlogConfigPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "NLog.config");
            LogManager.Configuration = new NLog.Config.XmlLoggingConfiguration(nlogConfigPath);
        }

        public void OnConnection(object Application, ext_ConnectMode ConnectMode, object AddInInst, ref Array custom)
        {
            //OneNote window is not ready to use.
            _logger.Debug("OnConnection");
            WpfAddInApplication.Current = new WpfAddInApplication();
        }

        public void OnDisconnection(ext_DisconnectMode RemoveMode, ref Array custom)
        {
            GC.Collect();
            //GC.WaitForPendingFinalizers();
            Environment.Exit(0);
        }

        public void OnAddInsUpdate(ref Array custom)
        {
        }

        public void OnStartupComplete(ref Array custom)
        {
            //Leave factory instainced here, since from now on OneNote Window is ready to access.
            try
            {
                NoteApplicationContext context = new NoteApplicationContext();
                _commandFactory = new CommandFactory(context);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        public void OnBeginShutdown(ref Array custom)
        {
            _logger.Debug("OnBeginShutdown");
            try
            {
                _commandFactory = null;
                WpfAddInApplication.Current.Shutdown();
            }
            catch(Exception ex)
            {
                _logger.Error(ex);
            }
        }
    }
}
