using System;
using System.Linq;
using Taskable.Core.Concrete;
using Taskable.Core.Extensions;

namespace Taskable.Core
{
    public static class TaskFinder
    {
        public static ComputedTask FindTask(this ILookup<string, ComputedTask> lookup, string command)
        {
            if (lookup == null)
                throw new Exception("lookup cannot be null");

            if (string.IsNullOrEmpty(command))
                throw new Exception("command cannot be null");

            var taskIdentifier = command.Shift();
            var possibleDefinitions = lookup[taskIdentifier].ToList();

            var requiredTask = possibleDefinitions.SingleOrDefault(t => t.Data.Regex.IsMatch(command));
            return requiredTask;
        }
    }
}
