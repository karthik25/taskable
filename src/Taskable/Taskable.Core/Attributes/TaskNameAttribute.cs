using System;

namespace TaskableCore.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class TaskNameAttribute : Attribute
    {
        private readonly string _taskName;

        public TaskNameAttribute(string taskName)
        {
            _taskName = taskName;
        }

        public string Name
        {
            get
            {
                return _taskName;
            }
        }
    }
}
