using Microsoft.CodeAnalysis;

namespace TaskableRoslyn.Contracts
{
    public interface IMetadataProvider
    {
        MetadataReference[] GenerateMetadaReferences();
    }
}
