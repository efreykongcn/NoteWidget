// Copyright (c) Efrey Kong. All Rights Reserved.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using NLog;

namespace NoteWidgetAddIn
{
    public sealed class WpfAddInApplication
    {
        private static ILogger _logger = LogManager.GetCurrentClassLogger();
        private static bool _appCreatedInThisAppDomain = false;

        public WpfAddInApplication()
        {
            if (!_appCreatedInThisAppDomain)
            {
                _appCreatedInThisAppDomain = true;
                var source = new TaskCompletionSource<object>();
                var t = new Thread(() =>
                {
                    this.Dispatcher = Dispatcher.CurrentDispatcher;
                    source.SetResult(null);
                    Dispatcher.Run();
                });
                t.SetApartmentState(ApartmentState.STA);
                t.Start();

                var tsk = source.Task.Result;
            }
            else
            {
                throw new InvalidOperationException("Cannot create more than one NoteWidget.WpfAddInApplication instance in the same AppDomain");
            }
        }

        public static WpfAddInApplication Current { get; set; }

        public void Invoke(Action callback)
        {
            try
            {
                Dispatcher.Invoke(callback);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Invoke error in WpfAddInApplication instance.");
            }
        }
        /// <summary>
        /// Invoke callback in a STA thread 
        /// </summary>
        /// <param name="callback"></param>
        public async Task BeginInvoke(Action callback)
        {
            try
            {
                await Dispatcher.BeginInvoke(callback);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "BeginInvoke error in WpfAddInApplication instance.");
            }
        }

        public void Shutdown()
        {
            if (WpfAddInApplication.Current != null)
            {
                var dispatcher = this.Dispatcher;
                Dispatcher = null;
                WpfAddInApplication.Current = null;
                dispatcher.BeginInvokeShutdown(DispatcherPriority.Normal);
            }
        }

        private Dispatcher Dispatcher { get; set; }
    }
}
