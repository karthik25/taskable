using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace TaskableRoslyn.Contracts
{
    public interface IMetadataProvider
    {
        MetadataReference[] GenerateMetadaReferences(IEnumerable<string> additionalReferences);
    }
}
