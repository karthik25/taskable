using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using TaskableRoslyn.Contracts;

namespace TaskableRoslynCore.TaskLoader
{
    public class DefaultMetadataProvider : IMetadataProvider
    {
        public MetadataReference[] GenerateMetadaReferences(IEnumerable<string> additionalReferences)
        {
            var references = new List<MetadataReference>
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(WebClient).Assembly.Location)
            };

            if (additionalReferences.Any())
            {
                foreach (var reference in additionalReferences)
                {
                    references.Add(MetadataReference.CreateFromFile(reference));
                }
            }

            return references.ToArray();
        }
    }
}
