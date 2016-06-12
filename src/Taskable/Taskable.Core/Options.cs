using System.Collections.Generic;

namespace Taskable.Core
{
    public class Options
    {
        public List<string> TaskDefinitionPaths { get; set; }
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
