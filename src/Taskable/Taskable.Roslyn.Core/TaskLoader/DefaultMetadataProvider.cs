using Microsoft.CodeAnalysis;
using System;
using TaskableRoslyn.Contracts;

namespace TaskableRoslynCore.TaskLoader
{
    public class DefaultMetadataProvider : IMetadataProvider
    {
        public MetadataReference[] GenerateMetadaReferences()
        {
            throw new NotImplementedException();
        }
    }
}
