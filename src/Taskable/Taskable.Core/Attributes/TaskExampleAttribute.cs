using System;

namespace TaskableCore.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class TaskExampleAttribute : Attribute
    {
        private readonly string _exampleCmd;

        public TaskExampleAttribute(string exampleCmd)
        {
            _exampleCmd = exampleCmd;
        }
    }
}
