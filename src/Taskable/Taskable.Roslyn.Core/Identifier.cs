namespace TaskableRoslynCore
{
    public class Identifier
    {
        public IdentifierType Type { get; set; }
        public string FullName { get; set; }
        public int LineNumber { get; set; }

        public string Image
        {
            get
            {
                switch (Type)
                {
                    case IdentifierType.Class:
                    case IdentifierType.Method:
                    case IdentifierType.Property:
                    case IdentifierType.Field:
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
