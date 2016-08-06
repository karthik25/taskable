using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace TaskableRoslyn.Contracts
{
    public interface ISourceFileSyntaxProvider
    {
        IEnumerable<SyntaxTree> GetTaskSyntaxTrees(IEnumerable<string> taskDefinitionPaths);
    }
}
