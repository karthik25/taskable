using System;

namespace Taskable.Core.Exceptions
{
    public class DuplicateTaskDefinitionException : Exception
    {
        public DuplicateTaskDefinitionException(string taskName)
            : base(string.Format("Task {0} has already been registered", taskName))
        {

        }
    }
}
