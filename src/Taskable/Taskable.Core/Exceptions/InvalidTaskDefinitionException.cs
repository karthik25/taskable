using System;

namespace Taskable.Core.Exceptions
{
    public class InvalidTaskDefinitionException : Exception
    {
        public InvalidTaskDefinitionException(string message)
            : base(message)
        {

        }
    }
}
