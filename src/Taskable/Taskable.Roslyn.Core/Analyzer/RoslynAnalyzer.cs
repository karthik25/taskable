using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace TaskableRoslynCore.Analyzer
{
    public static class RoslynAnalyzer
    {
        public static IEnumerable<Identifier> GetIdentifiers(this string sourceText)
        {
            var identifiers = new List<Identifier>();

            var tree = CSharpSyntaxTree.ParseText(sourceText);
            var root = tree.GetRoot();

            var classTypes = root.DescendantNodes().OfType<ClassDeclarationSyntax>();

            foreach (var classType in classTypes)
            {
                var isNestedNamespace = false;
                var nameSpace = classType.GetNamespace(out isNestedNamespace);
                var classInfo = new Identifier
                {
                    FullName = nameSpace != null ? string.Format("{0}.{1}", nameSpace.Name, classType.Identifier) : string.Format("{0}", classType.Identifier),
                    Type = IdentifierType.Class,
                    Line = classType.GetLocation().GetLineSpan().StartLinePosition.Line,
                    Offset = classType.FullSpan.Start
                };
                identifiers.Add(classInfo);

                var methodTypes = classType.DescendantNodes().OfType<MethodDeclarationSyntax>();                
                foreach (var methodType in methodTypes)
                {
                    var methodIdentifier = new Identifier
                    {
                        FullName = string.Format("{0}.{1}", classInfo.FullName, methodType.Identifier),
                        Offset = methodType.FullSpan.Start,
                        Line = methodType.GetLocation().GetLineSpan().StartLinePosition.Line,
                        Type = IdentifierType.Method
                    };
                    identifiers.Add(methodIdentifier);
                }

                var propertyTypes = classType.DescendantNodes().OfType<PropertyDeclarationSyntax>();
                foreach (var propertyType in propertyTypes)
                {
                    var propertyIdentifier = new Identifier
                    {
                        FullName = string.Format("{0}.{1}", classInfo.FullName, propertyType.Identifier),
                        Offset = propertyType.FullSpan.Start,
                        Line = propertyType.GetLocation().GetLineSpan().StartLinePosition.Line,
                        Type = IdentifierType.Property
                    };
                    identifiers.Add(propertyIdentifier);
                }

                var fieldTypes = classType.DescendantNodes().OfType<FieldDeclarationSyntax>();
                foreach (var fieldType in fieldTypes)
                {
                    var fieldIdentifier = new Identifier
                    {
                        FullName = string.Format("{0}.{1}", classInfo.FullName, fieldType.Declaration.Variables.First().Identifier),
                        Offset = fieldType.FullSpan.Start,
                        Line = fieldType.GetLocation().GetLineSpan().StartLinePosition.Line,
                        Type = IdentifierType.Property
                    };
                    identifiers.Add(fieldIdentifier);
                }
            }

            return identifiers;
        }

        private static NamespaceDeclarationSyntax GetNamespace(this SyntaxNode classDeclarationSyntax, out bool isNested)
        {
            var parentNode = classDeclarationSyntax.Parent;

            if (parentNode == null)
            {
                isNested = false;
                return null;
            }

            isNested = parentNode.Parent is NamespaceDeclarationSyntax;
            var ns = parentNode as NamespaceDeclarationSyntax;
            return ns ?? parentNode.GetNamespace(out isNested);
        }
    }
}
