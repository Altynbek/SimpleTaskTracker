using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskTracker.Models.Task
{
    public class SearchListModel
    {
        public SearchListModel()
        {
            Tasks = new List<TaskModel>();
        }

        public List<TaskModel> Tasks { get; set; }
    }
}