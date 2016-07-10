using System;

namespace TaskableScriptCs.Contracts
{
    public interface ISimpleTask
    {
        string Pattern { get; }
        Action<string[]> Stuff { get; }
    }
}
