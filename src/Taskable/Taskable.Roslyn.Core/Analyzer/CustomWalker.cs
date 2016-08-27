using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace TaskableRoslynCore.Analyzer
{
    internal class CustomWalker : CSharpSyntaxWalker
    {
        private int _nodeIndex;
        private List<Identifier> _identifiers;

        public CustomWalker()
        {
            _nodeIndex = 0;
            _identifiers = new List<Identifier>();
        }

        public List<Identifier> GetIdentifiers(SyntaxNode rootNode)
        {
            this.Visit(rootNode);
            return _identifiers;
        }

        public override void VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
        {
            var identifier = GetBasicInfo(node, node.Name.ToString(), IdentifierType.Namespace);
            _identifiers.Add(identifier);
            base.VisitNamespaceDeclaration(node);
        }

        public override void VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            var identifier = GetBasicInfo(node, node.Identifier.ToString(), IdentifierType.Class);
            _identifiers.Add(identifier);
            base.VisitClassDeclaration(node);
        }

        public override void VisitConstructorDeclaration(ConstructorDeclarationSyntax node)
        {
            var identifier = GetBasicInfo(node, node.Identifier.ToString(), IdentifierType.Constructor);
            _identifiers.Add(identifier);
            base.VisitConstructorDeclaration(node);
        }

        public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            var identifier = GetBasicInfo(node, node.Identifier.ToString(), IdentifierType.Method);
            _identifiers.Add(identifier);
            base.VisitMethodDeclaration(node);
        }

        public override void VisitInterfaceDeclaration(InterfaceDeclarationSyntax node)
        {
            var identifier = GetBasicInfo(node, node.Identifier.ToString(), IdentifierType.Interface);
            _identifiers.Add(identifier);
            base.VisitInterfaceDeclaration(node);
        }

        public override void VisitPropertyDeclaration(PropertyDeclarationSyntax node)
        {
            var identifier = GetBasicInfo(node, node.Identifier.ToString(), IdentifierType.Property);
            _identifiers.Add(identifier);
            base.VisitPropertyDeclaration(node);
        }

        public override void VisitFieldDeclaration(FieldDeclarationSyntax node)
        {
            var identifier = GetBasicInfo(node, node.Declaration.Variables.First().ToString(), IdentifierType.Field);
            _identifiers.Add(identifier);
            base.VisitFieldDeclaration(node);
        }

        public override void VisitDelegateDeclaration(DelegateDeclarationSyntax node)
        {
            var identifier = GetBasicInfo(node, node.Identifier.ToString(), IdentifierType.Delegate);
            _identifiers.Add(identifier);
            base.VisitDelegateDeclaration(node);
        }

        public override void VisitEnumDeclaration(EnumDeclarationSyntax node)
        {
            var identifier = GetBasicInfo(node, node.Identifier.ToString(), IdentifierType.Enumeration);
            _identifiers.Add(identifier);
            base.VisitEnumDeclaration(node);
        }

        public override void VisitEnumMemberDeclaration(EnumMemberDeclarationSyntax node)
        {
            var identifier = GetBasicInfo(node, node.Identifier.ToString(), IdentifierType.EnumerationMember);
            _identifiers.Add(identifier);
            base.VisitEnumMemberDeclaration(node);
        }

        public override void VisitEventDeclaration(EventDeclarationSyntax node)
        {
            var identifier = GetBasicInfo(node, node.Identifier.ToString(), IdentifierType.Event);
            _identifiers.Add(identifier);
            base.VisitEventDeclaration(node);
        }

        private Identifier GetBasicInfo(SyntaxNode node, string name, IdentifierType type)
        {
            return new Identifier
            {
                Name = name,
                StartLine = node.GetLocation().GetLineSpan().StartLinePosition.Line,
                EndLine = node.GetLocation().GetLineSpan().EndLinePosition.Line,
                OffsetStart = node.Span.Start,
                OffsetEnd = node.Span.End,
                Index = _nodeIndex++,
                Type = type,
                Prefix = GetPrefix(node)
            };
        }

        private string GetPrefix(SyntaxNode node)
        {
            var prefixes = new List<string>();

            var parent = node.Parent;
            while (parent != null)
            {
                if (parent is NamespaceDeclarationSyntax)
                {
                    prefixes.Add(((NamespaceDeclarationSyntax)parent).Name.ToString());
                }
                if (parent is ClassDeclarationSyntax)
                {
                    prefixes.Add(((ClassDeclarationSyntax)parent).Identifier.ToString());
                }
                parent = parent.Parent;
            }

            prefixes.Reverse();

            return prefixes.Any() ? string.Join(".", prefixes) : string.Empty;
        }
    }
}
