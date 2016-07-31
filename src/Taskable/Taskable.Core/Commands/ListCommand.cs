using System;

namespace TaskableCore.Commands
{
    public class ListCommand : ITaskerCommand
    {
        public void Execute(CommandContext context, string[] args)
        {
            Console.WriteLine("List command");
        }
    }
}
