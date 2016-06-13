using System;
using Microsoft.CodeAnalysis;
using Taskable.Contracts;

namespace Taskable.Core.TaskLoader
{
    public class DefaultMetadataProvider : IMetadataProvider
    {
        private readonly Options _options;

        public DefaultMetadataProvider(Options options)
        {
            _options = options;
        }

        public MetadataReference[] GenerateMetadaReferences()
        {
            throw new NotImplementedException();
        }
    }
}
