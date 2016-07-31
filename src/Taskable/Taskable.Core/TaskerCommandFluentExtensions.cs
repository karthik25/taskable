using System;
using TaskableCore.Extensions;

namespace TaskableCore
{
    public static class TaskerCommandFluentExtensions
    {
        public static bool IsCommand(this string command, out TaskerCommand type)
        {
            var firstWord = command.Shift();
            return Enum.TryParse(firstWord, true, out type);
        }
    }
}
