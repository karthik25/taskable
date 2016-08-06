using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TaskableCore;
using TaskableRoslyn.Contracts;

namespace TaskableRoslynCore.TaskLoader
{
    public class CSharpCompilationProvider : IDynamicCompilerProvider
    {
        private Options _options;

        public CSharpCompilationProvider(Options options)
        {
            _options = options;
        }

        public Compilation GetCompilation(ISourceFileSyntaxProvider sourceProvider, IMetadataProvider metadataProvider)
        {
            var compilerOptions = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary);
            var references = metadataProvider.GenerateMetadaReferences(new List<string>());
            var trees = sourceProvider.GetTaskSyntaxTrees(_options.TaskDefinitionPaths).ToArray();
            string assemblyName = Path.GetRandomFileName();

            CSharpCompilation compilation = CSharpCompilation.Create(
                assemblyName,
                syntaxTrees: trees,
                references: references,
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            return compilation;
        }
    }
}
