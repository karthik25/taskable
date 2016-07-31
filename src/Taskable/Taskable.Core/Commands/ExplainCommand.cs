using System;
using System.Linq;

namespace TaskableCore.Commands
{
    public class ExplainCommand : ITaskerCommand
    {
        public void Execute(CommandContext context, string[] args)
        {
            if (!context.TaskLookup.Contains(args.First()))
            {
                Console.WriteLine("Task not found");
                return;
            }

            Console.WriteLine("Details about " + args.First());
            Console.WriteLine();

            var tasks = context.TaskLookup[args.First()];
            foreach (var task in tasks)
            {
                Console.WriteLine("Details about: " + task.Name);
                Console.WriteLine();
                Console.WriteLine("Examples:");
                foreach (var example in task.Examples)
                {
                    Console.WriteLine(example);
                }
            }
        }
    }
}
