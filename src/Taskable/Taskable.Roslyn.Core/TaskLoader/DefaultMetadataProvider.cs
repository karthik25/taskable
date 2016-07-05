using Microsoft.CodeAnalysis;
using System;
using Taskable.Roslyn.Contracts;

namespace Taskable.Roslyn.Core.TaskLoader
{
    public class DefaultMetadataProvider : IMetadataProvider
    {
        public MetadataReference[] GenerateMetadaReferences()
        {
            throw new NotImplementedException();
        }
    }
}
