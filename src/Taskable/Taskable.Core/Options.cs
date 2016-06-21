using System;
using System.Collections.Generic;
using System.IO;

namespace Taskable.Core
{
    public class Options
    {
        public List<string> TaskDefinitionPaths { get; set; }
        public string CommandPrefix { get; set; }
        public string LogLevel { get; set; }

        public string ReplPrefix
        {
            get
            {
                return CommandPrefix ?? "> ";
            }
        }
        
        public static Options ParseFromFile(string optionsFile)
        {
            return ParseFromFile(new StreamReader(optionsFile));
        }
        
        public static Options ParseFromFile(StreamReader stream)
        {
            throw new NotImplementedException();
        }
    }
}
