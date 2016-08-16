using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace TaskableRoslyn.Contracts
{
    public interface IDynamicCompilerProvider
    {
        IEnumerable<Compilation> GetCompilations(ISourceFileSyntaxProvider sourceProvider, IMetadataProvider metadataProvider);
    }
}
