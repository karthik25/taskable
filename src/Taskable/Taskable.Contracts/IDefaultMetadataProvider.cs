using Microsoft.CodeAnalysis;

namespace Taskable.Contracts
{
    public interface IDefaultMetadataProvider
    {
        MetadataReference[] GenerateMetadaReferences();
    }
}
