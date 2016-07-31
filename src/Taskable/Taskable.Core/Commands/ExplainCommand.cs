using System;

namespace TaskableCore.Commands
{
    public class ExplainCommand : ITaskerCommand
    {
        public void Execute(CommandContext context, string[] args)
        {
            Console.WriteLine("Explain command");
        }
    }
}
