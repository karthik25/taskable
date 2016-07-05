using System;

namespace Taskable.ScriptCs.Contracts
{
    public interface ISimpleTask
    {
        string Name { get; }
        string Pattern { get; }
        string Example { get; }
        Action<string[]> Stuff { get; }
    }
}
