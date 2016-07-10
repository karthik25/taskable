using ScriptCs.Contracts;

namespace TaskableScriptPack
{
    public class TaskableScriptPack : ScriptPack<Taskable>
    {
        public override void Initialize(IScriptPackSession session)
        {
            session.ImportNamespace(this.GetType().Namespace);
            session.ImportNamespace("TaskableScriptCs.Contracts");
            var arguments = Arguments.Parse(session.ScriptArgs ?? new string[] { });
            var options = arguments.CreateAsOptions();
            this.Context = new Taskable(options);
        }
    }
}
