using System.ComponentModel.DataAnnotations;

namespace ToDoAPI.Models
{
    public class Note
    {
        public int Id { get; set; }
        [Required]
        public string ToDoNote { get; set; }
        [Required]
        public bool Completed { get; set; } = false;
        [Required]
        public Priority Priority { get; set; } = Priority.Medium;
    }

    public enum Priority
    {
        Low, Medium, High
    }
}
