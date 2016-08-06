using Microsoft.CodeAnalysis;
using System.Reflection;

namespace TaskableRoslyn.Contracts
{
    public interface IDynamicAssemblyProvider
    {
        Assembly GetTaskAssembly(Compilation compilation);
    }
}
