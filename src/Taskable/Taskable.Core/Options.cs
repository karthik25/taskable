namespace Taskable.Core
{
    public class Options
    {
        public string[] TaskDefinitionPaths { get; set; }
        public string CommandPrefix { get; set; }

        public string ReplPrefix
        {
            get
            {
                return CommandPrefix ?? "> ";
            }
        }
    }
}
