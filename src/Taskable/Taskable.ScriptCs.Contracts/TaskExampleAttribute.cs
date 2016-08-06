using System;

namespace TaskableScriptCs.Contracts
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class TaskExampleAttribute : Attribute
    {
        private readonly string _exampleCmd;

        public TaskExampleAttribute(string exampleCmd)
        {
            _exampleCmd = exampleCmd;
        }

        public string Example
        {
            get
            {
                return _exampleCmd;
            }
        }
    }
}
