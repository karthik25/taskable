using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Linq;
using TaskableRoslyn.Contracts;

namespace TaskableRoslynCore.TaskLoader
{
    public class DefaultMetadataProvider : IMetadataProvider
    {
        public MetadataReference[] GenerateMetadaReferences(IEnumerable<string> additionalReferences)
        {
            MetadataReference[] references = new MetadataReference[]
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location)
            };
            return references;
        }
    }
}
