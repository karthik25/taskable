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

        public IEnumerable<Compilation> GetCompilations(ISourceFileSyntaxProvider sourceProvider, IMetadataProvider metadataProvider)
        {
            var compilerOptions = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary);
            var references = metadataProvider.GenerateMetadaReferences(_options.AdditionalReferences);
            var syntaxTrees = sourceProvider.GetTaskSyntaxTrees(_options.TaskDefinitionPaths).ToArray();
            string assemblyName = Path.GetRandomFileName();

            foreach (var syntaxTree in syntaxTrees)
            {
                CSharpCompilation compilation = CSharpCompilation.Create(
                assemblyName,
                syntaxTrees: new[] { syntaxTree },
                references: references,
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

                yield return compilation;
            }
        }
    }
}
