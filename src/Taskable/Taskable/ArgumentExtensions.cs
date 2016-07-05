using Taskable.Core;

namespace TaskableBase
{
    public static class ArgumentExtensions
    {
        public static Options CreateAsOptions(this Arguments arguments)
        {
            return new Options();
        }
    }
}
