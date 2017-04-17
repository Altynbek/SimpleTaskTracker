using System.Collections.Generic;

namespace TaskTracker.Models.Task
{
    public class TaskListModel
    {
        public TaskListModel()
        {
            Tasks = new List<TaskModel>();
        }

        public List<TaskModel> Tasks { get; set; }
    }
}