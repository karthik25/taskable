using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskableRoslynCore.Analyzer
{
    public static class RoslynHost
    {
        public async static Task<List<Identifier>> GetIdentifiers(this string sourceText)
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(sourceText);
            var root = await syntaxTree.GetRootAsync();
            var customWalker = new CustomWalker();
            return customWalker.GetIdentifiers(root);
        }
    }
}
