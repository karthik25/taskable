namespace TaskableRoslynCore
{
    public class Identifier
    {
        public IdentifierType Type { get; set; }
        public string FullName { get; set; }
        public int LineNumber { get; set; }
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
