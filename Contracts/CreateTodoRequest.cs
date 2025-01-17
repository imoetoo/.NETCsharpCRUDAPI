//DTO
// This DTO defines the structure and validation rules for creating a new Todo item

using System.ComponentModel.DataAnnotations;

namespace TodoAPI.Contracts
{
    public class CreateTodoRequest
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        // cannot be null, to track when the Todo item needs to be completed
        [Required]
        public DateTime DueDate { get; set; }

        [Range(1, 5)]
        public int Priority { get; set; }
    }
}