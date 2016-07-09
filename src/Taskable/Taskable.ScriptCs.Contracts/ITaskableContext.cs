namespace TaskableScriptCs.Contracts
{
    public interface ITaskableContext
    {
        void Initialize();
        void WaitForCommands();
        void RegisterTask(ISimpleTask simpleTask);        
    }
}
