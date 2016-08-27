using System.Collections.Generic;

namespace TaskableRoslynCore
{
    public class Identifier
    {
        public IdentifierType Type { get; set; }
        public string Prefix { get; set; }
        public string Name { get; set; }
        public int StartLine { get; set; }
        public int EndLine { get; set; }
        public int OffsetStart { get; set; }
        public int OffsetEnd { get; set; }
        public List<string> Parameters { get; set; }
        public int Index { get; set; }

        public string FullName
        {
            get
            {
                if (string.IsNullOrEmpty(Prefix))
                    return Name;
                return string.Format("{0}.{1}", Prefix, Name);
            }
            set { }
        }

        public string QualifiedPrefix
        {
            get
            {
                if (string.IsNullOrEmpty(Prefix))
                    return string.Empty;
                return string.Format("{0}.", Prefix);
            }
            set { }
        }

        public string Image
        {
            get
            {
                switch (Type)
                {
                    case IdentifierType.Class:
                        return "/Images/class_16xLG.png";
                    case IdentifierType.Method:
                        return "/Images/method_16xLG.png";
                    case IdentifierType.Property:
                        return "/Images/properties_16xLG.png";
                    case IdentifierType.Field:
                        return "/Images/field_16xLG.png";
                    case IdentifierType.Interface:
                        return "/Images/interface_16xLG.png";
                    default:
                        return "/Images/vscode.png";
                }
            }
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}, {3}, {4} [{5}] ({6})", FullName, StartLine, EndLine, OffsetStart, OffsetEnd, Type, Index);
        }
    }

    public enum IdentifierType
    {
        Namespace,
        Class,
        Method,
        Interface,
        Property,
        Field,
        Enumeration,
        EnumerationMember,
        Delegate,
        Event
    }
}
