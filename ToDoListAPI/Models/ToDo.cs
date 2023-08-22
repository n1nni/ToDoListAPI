namespace ToDoListAPI.Models
{
    public class ToDo
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public string? Description { get; set; }
        public bool Completed { get; set; }

    }
}
