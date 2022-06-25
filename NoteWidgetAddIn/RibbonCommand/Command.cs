// Copyright (c) Efrey Kong. All Rights Reserved.
// Licensed under the Apache License, Version 2.0.

using System.Threading.Tasks;
using System.Windows.Forms;
using NLog;

#pragma warning disable CS3003 // Type is not CLS-compliant
namespace NoteWidgetAddIn.RibbonCommand
{
    public abstract class Command
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        public Command SetContext(NoteApplicationContext context)
        {
            Context = context;
            return this;
        }
        public Command SetOwner(IWin32Window owner)
        {
            OwnerWin32Window = owner;
            return this;
        }
        protected NoteApplicationContext Context { get; private set; }
        protected IWin32Window OwnerWin32Window { get; private set; }
        protected ILogger Logger => _logger;
        public abstract Task ExecuteAsync(params object[] args);
    }
}
