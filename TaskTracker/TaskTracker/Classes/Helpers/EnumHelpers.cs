using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskTracker.Classes.Db.Structure;

namespace TaskTracker.Classes.Helpers
{
    public static class EnumHelpers
    {
        public static List<TaskStatus> GetTaskStatuses()
        {
            var lst = Enum.GetValues(typeof(TaskStatus)).Cast<TaskStatus>().ToList();
            return lst;
        }

        public static List<TaskType> GetTaskTypes()
        {
            var lst = Enum.GetValues(typeof(TaskType)).Cast<TaskType>().ToList();
            return lst;
        }
    }
}