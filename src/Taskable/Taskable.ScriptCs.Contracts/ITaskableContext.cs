namespace TaskableScriptCs.Contracts
{
    public interface ITaskableContext
    {
        void WaitForCommands();
        void RegisterTask(ISimpleTask simpleTask);        
    }
}
