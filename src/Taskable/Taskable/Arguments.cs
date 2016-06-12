using CommandLine;
using System.Collections.Generic;
using Taskable.Core.Extensions;

namespace Taskable
{
    public class Arguments
    {
        [Option('c', "command-prefix", DefaultValue = "", HelpText = "Command prefix")]
        public int CommandPrefix { get; set; }

        [Option('p', "script-path", DefaultValue = "", HelpText = "Path for .csx files (")]
        public IEnumerable<string> ScriptPaths { get; set; }

        public static Arguments Parse(string[] args)
        {
            var options = new Arguments();
            if (!Parser.Default.ParseArguments(args, options))
            {
                "Unable to parse the arguments, using defaults".PrintYellow();
            }
            return options;
        }
    }    
}
