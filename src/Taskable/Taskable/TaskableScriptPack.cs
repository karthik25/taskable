using ScriptCs.Contracts;

namespace Taskable
{
    public class TaskableScriptPack : ScriptPack<TaskableContext>
    {
        public override void Initialize(IScriptPackSession session)
        {
            var arguments = Arguments.Parse(session.ScriptArgs);
            var options = arguments.AsOptions();
            this.Context = new TaskableContext(options);
        }
    }
}
