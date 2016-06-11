using ScriptCs.Contracts;

namespace Taskable
{
    public class TaskableScriptPack : ScriptPack<TaskableContext>
    {
        public override void Initialize(IScriptPackSession session)
        {
            var scriptArgs = session.ScriptArgs;
            this.Context = new TaskableContext(null);
        }
    }
}
