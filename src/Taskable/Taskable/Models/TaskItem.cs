namespace TaskableApp.Models
{
    public class TaskItem
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
