namespace TaskableCore.Commands
{
    public interface ITaskerCommand
    {
        void Execute(CommandContext context);
    }
}
