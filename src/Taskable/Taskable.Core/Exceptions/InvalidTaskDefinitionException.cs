using System;

namespace TaskableCore.Exceptions
{
    public class InvalidTaskDefinitionException : Exception
    {
        public InvalidTaskDefinitionException(string message)
            : base(message)
        {

        }
    }
}
