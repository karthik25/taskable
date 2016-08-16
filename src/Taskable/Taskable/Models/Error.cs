namespace TaskableApp.Models
{
    public class Error
    {
        public Error(string content)
        {
            this.Content = content;
        }

        public string Content { get; set; }
    }
}
