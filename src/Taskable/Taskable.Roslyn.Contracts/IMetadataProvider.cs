using Microsoft.CodeAnalysis;

namespace Taskable.Roslyn.Contracts
{
    public interface IMetadataProvider
    {
        MetadataReference[] GenerateMetadaReferences();
    }
}
