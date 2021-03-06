﻿using System;

namespace TaskableCore.Commands
{
    public class HelpCommand : ITaskerCommand
    {
        public void Execute(CommandContext context, string[] args)
        {
            Console.WriteLine("Taskable help");
            Console.WriteLine();
            Console.WriteLine("You can enter any of the following commands");
            Console.WriteLine("help: see what you see now again!");
            Console.WriteLine("list: list of all tasks registered");
            Console.WriteLine("explain <task_name>: provide more details about a task");
        }
    }
}
