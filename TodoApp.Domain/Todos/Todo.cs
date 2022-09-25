
using System.ComponentModel.DataAnnotations;

namespace TodoApp.Domain.Todos
{
    public class Todo
    {
        [Required]
        public Guid Id { get; set; }
        [Required()]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public bool IsDone { get; set; }
        public DateTime Time { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
