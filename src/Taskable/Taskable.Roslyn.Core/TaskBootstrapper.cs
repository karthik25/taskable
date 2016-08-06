using System;
using System.Collections.Generic;
using System.Linq;
using TaskableCore;
using TaskableRoslynCore.TaskLoader;
using TaskableScriptCs.Contracts;

namespace TaskableRoslynCore
{
    public class TaskBootstrapper
    {
        public IEnumerable<ISimpleTask> GetTasks(Options options)
        {
            var sourceProvider = SourceFileProviderFactory.CreateSourceProvider(LanguageType.CSharp);
            var metadataProvider = new DefaultMetadataProvider();
            var compilationProvider = new CSharpCompilationProvider(options);
            var compilation = compilationProvider.GetCompilation(sourceProvider, metadataProvider);

            var assemblyProvider = new DefaultDynamicAssemblyProvider();
            var assembly = assemblyProvider.GetTaskAssembly(compilation);

            if (assembly != null)
            {
                var tasks = assembly.GetTypes().Where(t => typeof(ISimpleTask).IsAssignableFrom(t));
                foreach (var task in tasks)
                {
                    var instance = (ISimpleTask)Activator.CreateInstance(task, null);
                    yield return instance;
                }
            }
        }
    }
}
