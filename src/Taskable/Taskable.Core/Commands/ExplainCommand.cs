using System;
using System.Linq;

namespace TaskableCore.Commands
{
    public class ExplainCommand : ITaskerCommand
    {
        public void Execute(CommandContext context, string[] args)
        {
            var taskName = args.First();

            if (!context.TaskLookup.Contains(taskName))
            {
                Console.WriteLine("Task not found");
                return;
            }
            
            var tasks = context.TaskLookup[taskName];
            foreach (var task in tasks)
            {
                Console.WriteLine("Details about: " + task.Command);
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
