﻿using System.Collections.Generic;
using TaskableCore.Commands;

namespace TaskableCore
{
    public sealed class CommandExecutionFactory
    {
        private CommandContext _commandContext;

        private CommandExecutionFactory(CommandContext commandContext)
        {
            _commandContext = commandContext;
        }

        public static CommandExecutionFactory Create(CommandContext commandContext)
        {
            return new CommandExecutionFactory(commandContext);
        }

        public void Execute(TaskerCommand command)
        {
            var executor = _commandExecutorMap[command];
            executor.Execute(_commandContext);
        }

        private static Dictionary<TaskerCommand, ITaskerCommand> _commandExecutorMap = new Dictionary<TaskerCommand, ITaskerCommand>
        {
            { TaskerCommand.List, new ListCommand() },
            { TaskerCommand.Explain, new ExplainCommand() },
            { TaskerCommand.Help, new HelpCommand() }
        };
    }
}
