using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace TaskableRoslyn.Contracts
{
    public interface IDynamicAssemblyProvider
    {
        AssemblyResult GetTaskAssembly(IEnumerable<Compilation> compilations);
    }
}
