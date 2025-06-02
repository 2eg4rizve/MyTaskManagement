using System;
using System.ComponentModel.DataAnnotations;

namespace MyTaskManagement.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;

        [Required]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }
        public bool IsCompleted { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DueDate { get; set; }
    }
}