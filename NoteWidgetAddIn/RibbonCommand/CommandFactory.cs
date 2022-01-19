// Copyright (c) Efrey Kong. All Rights Reserved.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using NLog;

namespace NoteWidgetAddIn.RibbonCommand
{
    public class CommandFactory
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private readonly NoteApplicationContext _context;
        private readonly IWin32Window _owner;
        public CommandFactory(NoteApplicationContext content)
        {
            _context = content;
            using(var app = _context.CreateApplication())
            {
                _owner = app.CreateCurrentWin32Window();
                Debug.Assert(_owner.Handle.ToInt64() > 0, "");
            }
        }
        public async Task<Command> Run<T>(params object[] args) where T : Command, new()
        {
            var command = new T();
            await Run(command, args);

            return command;
        }

        private async Task Run(Command command, params object[] args)
        {
            var type = command.GetType();
            _logger.Info($"Running command {type.Name}");

            command.SetContext(_context).SetOwner(_owner);

            try
            {
                await command.ExecuteAsync(args);
                _logger.Info($"Command {type.Name} ran successfully.");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Exception occourred when running command {type.FullName}.");
            }
        }
    }
}
