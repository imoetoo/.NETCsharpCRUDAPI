// Models/Todo.cs
//Think of a model as a blueprint for the kind of data our application will work with. It helps us organize and manage this data efficiently.
using System.ComponentModel.DataAnnotations;

namespace TodoAPI.Models
{
    public class Todo
    {
        [Key]
        public Guid Id { get; set; } //this is the key, GUID is a hexadecimal identifier separated by 4 hyphens
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsComplete { get; set; }
        public DateTime DueDate { get; set; }
        public int Priority { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Todo()
        {
            IsComplete = false;
        }
    }
}