using System.Linq;
using TaskableCore.Commands;
using TaskableCore.Concrete;

namespace TaskableCore
{
    public class CommandContext
    {
        ILookup<string, ComputedTask> TaskLookup { get; set; }
        IWriter Writer { get; set; }
    }
}
