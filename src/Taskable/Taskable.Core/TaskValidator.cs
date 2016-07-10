using TaskableCore.Exceptions;
using TaskableScriptCs.Contracts;

namespace TaskableCore
{
    public static class TaskValidator
    {
        public static void ValidateTask(ISimpleTask simpleTask)
        {
            if (string.IsNullOrEmpty(simpleTask.Pattern))
                throw new InvalidTaskDefinitionException("Task pattern cannot be null or empty");

            if (simpleTask.Stuff == null)
                throw new InvalidTaskDefinitionException("Action to be performed on invoke cannot be null");
        }
    }
}
