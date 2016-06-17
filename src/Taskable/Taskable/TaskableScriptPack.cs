using ScriptCs.Contracts;

namespace TaskableBase
{
    public class TaskableScriptPack : ScriptPack<Taskable>
    {
        public override void Initialize(IScriptPackSession session)
        {
            session.ImportNamespace(this.GetType().Namespace);
            var arguments = Arguments.Parse(session.ScriptArgs);
            var options = arguments.AsOptions();
            this.Context = new Taskable(options);
        }
    }
}
