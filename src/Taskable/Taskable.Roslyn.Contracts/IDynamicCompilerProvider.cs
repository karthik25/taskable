using Microsoft.CodeAnalysis;

namespace TaskableRoslyn.Contracts
{
    public interface IDynamicCompilerProvider
    {
        Compilation GetCompilation(ISourceFileSyntaxProvider sourceProvider, IMetadataProvider metadataProvider);
    }
}
