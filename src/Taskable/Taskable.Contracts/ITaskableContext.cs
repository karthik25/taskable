namespace Taskable.Contracts
{
    public interface ITaskableContext
    {
        void Initialize();
        void WaitForCommands();
        void RegisterTask(ISimpleTask simpleTask);        
    }
}
