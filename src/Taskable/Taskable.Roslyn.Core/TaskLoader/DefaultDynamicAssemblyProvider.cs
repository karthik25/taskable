using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Emit;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using TaskableRoslyn.Contracts;

namespace TaskableRoslynCore.TaskLoader
{
    public class DefaultDynamicAssemblyProvider : IDynamicAssemblyProvider
    {
        public AssemblyResult GetTaskAssembly(IEnumerable<Compilation> compilations)
        {
            var assemblyResult = new AssemblyResult();
            foreach (var compilation in compilations)
            {
                using (var ms = new MemoryStream())
                {
                    EmitResult result = compilation.Emit(ms);
                    if (result.Success)
                    {
                        ms.Seek(0, SeekOrigin.Begin);
                        var assembly = Assembly.Load(ms.ToArray());
                        assemblyResult.Assemblies.Add(assembly);
                    }
                    else
                    {
                        IEnumerable<Diagnostic> failures = result.Diagnostics.Where(diagnostic =>
                            diagnostic.IsWarningAsError ||
                            diagnostic.Severity == DiagnosticSeverity.Error);

                        foreach (Diagnostic diagnostic in failures)
                        {
                            assemblyResult.CompileErrors.Add(string.Format("{0}: {1}", diagnostic.Id, diagnostic.GetMessage()));
                        }
                    }
                }
            }
            return assemblyResult;
        }
    }
}
