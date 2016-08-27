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
                    Prefix = string.Empty,
                    Name = nameSpace != null ? string.Format("{0}.{1}", nameSpace.Name, classType.Identifier) : string.Format("{0}", classType.Identifier),
                    Type = IdentifierType.Class,
                    StartLine = classType.GetLocation().GetLineSpan().StartLinePosition.Line,
                    OffsetStart = classType.FullSpan.Start
                };
                identifiers.Add(classInfo);

                var methodTypes = classType.DescendantNodes().OfType<MethodDeclarationSyntax>();                
                foreach (var methodType in methodTypes)
                {
                    var methodIdentifier = new Identifier
                    {
                        Prefix = classInfo.Name,
                        Name = methodType.Identifier.ToString(),
                        OffsetStart = methodType.Span.Start,
                        StartLine = methodType.GetLocation().GetLineSpan().StartLinePosition.Line,
                        Type = IdentifierType.Method
                    };
                    identifiers.Add(methodIdentifier);
                }

                var propertyTypes = classType.DescendantNodes().OfType<PropertyDeclarationSyntax>();
                foreach (var propertyType in propertyTypes)
                {
                    var propertyIdentifier = new Identifier
                    {
                        Prefix = classInfo.Name,
                        Name = propertyType.Identifier.ToString(),
                        OffsetStart = propertyType.Span.Start,
                        StartLine = propertyType.GetLocation().GetLineSpan().StartLinePosition.Line,
                        Type = IdentifierType.Property
                    };
                    identifiers.Add(propertyIdentifier);
                }

                var fieldTypes = classType.DescendantNodes().OfType<FieldDeclarationSyntax>();
                foreach (var fieldType in fieldTypes)
                {
                    var fieldIdentifier = new Identifier
                    {
                        Prefix = classInfo.Name,
                        Name = fieldType.Declaration.Variables.First().Identifier.ToString(),
                        OffsetStart = fieldType.Span.Start,
                        StartLine = fieldType.GetLocation().GetLineSpan().StartLinePosition.Line,
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
