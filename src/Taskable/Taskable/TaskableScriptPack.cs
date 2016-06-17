using ScriptCs.Contracts;

namespace TaskableBase
{
    public class TaskableScriptPack : ScriptPack<Taskable>
    {
        public override void Initialize(IScriptPackSession session)
        {
            var arguments = Arguments.Parse(session.ScriptArgs);
            var options = arguments.AsOptions();
            this.Context = new Taskable(options);
        }
    }
}
