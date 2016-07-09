using ScriptCs.Contracts;
using System.IO;
using System.Reflection;

namespace TaskableScriptPack
{
    public class TaskableScriptPack : ScriptPack<Taskable>
    {
        public override void Initialize(IScriptPackSession session)
        {
            session.ImportNamespace(this.GetType().Namespace);
            var basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            session.AddReference(string.Format(@"{0}\Taskable.ScriptCs.Contracts.dll", basePath));
            session.ImportNamespace("TaskableScriptCs.Contracts");
            var arguments = Arguments.Parse(session.ScriptArgs ?? new string[] { });
            var options = arguments.CreateAsOptions();
            this.Context = new Taskable(options);
        }
    }
}
