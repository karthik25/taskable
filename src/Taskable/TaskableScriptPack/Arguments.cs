using CommandLine;
using System.Collections.Generic;
using TaskableCore.Extensions;

namespace TaskableScriptPack
{
    public class Arguments
    {
        [Option('c', "command-prefix", DefaultValue = "", HelpText = "Command prefix")]
        public string CommandPrefix { get; set; }

        [Option('p', "script-path", DefaultValue = null, HelpText = "Path for .csx files (")]
        public IEnumerable<string> ScriptPaths { get; set; }

        [Option('l', "log-level", DefaultValue = "", HelpText = "Log level (g, d, e)")]
        public string LogLevel { get; set; }

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
