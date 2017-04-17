using System.ComponentModel.DataAnnotations;
using TaskTracker.Classes.Db.Structure;

namespace TaskTracker.Models.Task
{
    public class TaskModel
    {
        public int? Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Details { get; set; }

        [Required]
        public TaskType Type { get; set; }

        public TaskStatus Status { get; set; }
    }
}