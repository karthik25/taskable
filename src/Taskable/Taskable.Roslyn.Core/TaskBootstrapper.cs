using System;
using System.Linq;
using TaskableCore;
using TaskableRoslynCore.TaskLoader;
using TaskableScriptCs.Contracts;

namespace TaskableRoslynCore
{
    public class TaskBootstrapper
    {
        public TaskResult GetTasks(Options options)
        {
            var sourceProvider = SourceFileProviderFactory.CreateSourceProvider(LanguageType.CSharp);
            var metadataProvider = new DefaultMetadataProvider();
            var compilationProvider = new CSharpCompilationProvider(options);
            var compilations = compilationProvider.GetCompilations(sourceProvider, metadataProvider);

            var assemblyProvider = new DefaultDynamicAssemblyProvider();
            var assemblyResult = assemblyProvider.GetTaskAssembly(compilations);

            var taskResult = new TaskResult();
            if (assemblyResult != null)
            {
                var tasks = assemblyResult.Assemblies.SelectMany(a => a.GetTypes()).Where(t => typeof(ISimpleTask).IsAssignableFrom(t));
                foreach (var task in tasks)
                {
                    var instance = (ISimpleTask)Activator.CreateInstance(task, null);
                    taskResult.Tasks.Add(instance);
                }
            }
            taskResult.Errors = assemblyResult.CompileErrors;
            return taskResult;
        }
    }
}
