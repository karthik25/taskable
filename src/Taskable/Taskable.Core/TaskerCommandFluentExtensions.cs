using System;

namespace TaskableCore
{
    public static class TaskerCommandFluentExtensions
    {
        public static bool IsCommand(this string command, out TaskerCommand type)
        {
            return Enum.TryParse(command, out type);
        }
    }
}
