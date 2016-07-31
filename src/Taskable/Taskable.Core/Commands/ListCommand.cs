using System;
using System.Linq;

namespace TaskableCore.Commands
{
    public class ListCommand : ITaskerCommand
    {
        public void Execute(CommandContext context, string[] args)
        {
            Console.WriteLine("List of all tasks");
            Console.WriteLine();

            foreach(var entry in context.TaskLookup)
            {
                foreach (var item in entry)
                {
                    Console.WriteLine(item.Name);
                }
            }
        }
    }
}
