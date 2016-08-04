using CommandLine;
using TaskableCore.Extensions;

namespace TaskableScriptPack
{
    public class Arguments
    {
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
