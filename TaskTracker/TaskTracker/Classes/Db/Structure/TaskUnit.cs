using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TaskTracker.Classes.Db.Structure
{
    public class TaskUnit
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Details { get; set; }

        public TaskType Type { get; set; }

        public TaskStatus Status { get; set; }
    }

    public enum TaskType
    {
        [Description("Task")]
        Task = 1,

        [Description("Error")]
        Error = 2
    }

    public enum TaskStatus
    {
        [Description("TODO")]
        ToDo = 1,

        [Description("In Progress")]
        InProgress = 2,

        [Description("Testing")]
        Testing = 3,

        [Description("Done")]
        Done = 4
    }
}