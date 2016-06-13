using Microsoft.CodeAnalysis;

namespace Taskable.Contracts
{
    public interface IMetadataProvider
    {
        MetadataReference[] GenerateMetadaReferences();
    }
}
