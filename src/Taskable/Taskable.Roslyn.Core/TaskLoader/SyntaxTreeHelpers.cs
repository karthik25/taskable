using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

namespace TaskableRoslynCore.TaskLoader
{
    public static class SyntaxTreeHelpers
    {
        public static bool ContainsTask(this SyntaxTree tree)
        {
            var classes = tree.GetRoot()
                              .DescendantNodes()
                              .OfType<ClassDeclarationSyntax>()
                              .Where(c => c.HasBaseTypes());
            foreach (var cls in classes)
            {
                foreach (var type in cls.BaseList.Types)
                {
                    var idSyntax = type.Type as IdentifierNameSyntax;
                    if (idSyntax != null && idSyntax.Identifier.ToString().EndsWith("ISimpleTask"))
                        return true;

                    var qualifiedSyntax = type.Type as QualifiedNameSyntax;
                    if (qualifiedSyntax != null && qualifiedSyntax.ToString().EndsWith("ISimpleTask"))
                        return true;
                }
            }
            return false;
        }

        private static bool HasBaseTypes(this ClassDeclarationSyntax classDeclaration)
        {
            return classDeclaration.BaseList != null &&
                   classDeclaration.BaseList.Types != null &&
                   classDeclaration.BaseList.Types.Any();
        }
    }
}
