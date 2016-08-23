namespace TaskableRoslynCore
{
    public class Identifier
    {
        public IdentifierType Type { get; set; }
        public string Prefix { get; set; }
        public string Name { get; set; }
        public int Line { get; set; }
        public int Offset { get; set; }

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
    }

    public enum IdentifierType
    {
        Class,
        Method,
        Interface,
        Property,
        Field
    }
}
