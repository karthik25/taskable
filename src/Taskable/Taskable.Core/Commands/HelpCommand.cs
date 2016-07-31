using System;

namespace TaskableCore.Commands
{
    public class HelpCommand : ITaskerCommand
    {
        public void Execute(CommandContext context, string[] args)
        {
            Console.WriteLine("Help command");
        }
    }
}
