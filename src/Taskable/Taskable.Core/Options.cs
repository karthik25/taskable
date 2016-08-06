using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace TaskableCore
{
    public class Options
    {
        public List<string> TaskDefinitionPaths { get; set; }
        public List<string> AdditionalReferences { get; set; }

        public static Options ParseFromString(string strContent)
        {
            return ParseFromFile(new StringReader(strContent));
        }

        public static Options ParseFromFile(string optionsFile)
        {
            return ParseFromFile(new StreamReader(optionsFile));
        }
        
        public static Options ParseFromFile(TextReader stream)
        {
            var deserializer = new Deserializer(namingConvention: new CamelCaseNamingConvention());
            var options = deserializer.Deserialize<Options>(stream);
            return options;
        }

        public static void CreateDefaultOptionsFile(string configPath)
        {
            var options = new Options();
            var serializer = new Serializer();
            var writer = new StreamWriter(configPath);
            serializer.Serialize(writer, options);
        }

        public static Options OverrideWithReferences(Options options, IEnumerable<string> references)
        {
            if (options.AdditionalReferences == null)
                options.AdditionalReferences = new List<string>();
            options.AdditionalReferences.AddRange(references);
            return options;
        }
    }
}
