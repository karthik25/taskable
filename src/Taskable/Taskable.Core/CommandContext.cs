using System.Linq;
using TaskableCore.Commands;
using TaskableCore.Concrete;

namespace TaskableCore
{
    public class CommandContext
    {
        public ILookup<string, ComputedTask> TaskLookup { get; set; }
        public IWriter Writer { get; set; }
    }
}
