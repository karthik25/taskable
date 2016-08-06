using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Generic;
using System.IO;
using TaskableRoslyn.Contracts;

namespace TaskableRoslynCore.TaskLoader
{
    public class CSharpSourceFileSyntaxProvider : ISourceFileSyntaxProvider
    {
        public IEnumerable<SyntaxTree> GetTaskSyntaxTrees(IEnumerable<string> taskDefinitionPaths)
        {
            foreach (var directory in taskDefinitionPaths)
            {
                var files = Directory.GetFiles(directory, "*.cs");
                foreach (var file in files)
                {
                    var tree = CSharpSyntaxTree.ParseText(File.ReadAllText(file));
                    if (tree.ContainsTask())
                    {
                        yield return tree;
                    }
                }
            }
        }
    }
}
