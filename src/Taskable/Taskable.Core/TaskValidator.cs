using Taskable.Contracts;
using Taskable.Core.Exceptions;

namespace Taskable.Core
{
    public static class TaskValidator
    {
        public static void ValidateTask(ISimpleTask simpleTask)
        {
            if (string.IsNullOrEmpty(simpleTask.Name))
                throw new InvalidTaskDefinitionException("Task name cannot be null or empty");

            if (string.IsNullOrEmpty(simpleTask.Pattern))
                throw new InvalidTaskDefinitionException("Task pattern cannot be null or empty");

            if (simpleTask.Stuff == null)
                throw new InvalidTaskDefinitionException("Action to be performed on invoke cannot be null");
        }
    }
}
