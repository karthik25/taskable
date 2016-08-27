using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace TaskableRoslynCore.Analyzer
{
    public class RoslynHost
    {
        public List<Identifier> GetIdentifiers(SyntaxNode rootNode)
        {
            var customWalker = new CustomWalker();
            return customWalker.GetIdentifiers(rootNode);
        }
    }
}
