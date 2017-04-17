using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskTracker.Models.Task
{
    public class UpdateTaskModel
    {
        public UpdateTaskModel()
        {
            TaskStatuses = new List<SelectListItem>();
            TaskTypes = new List<SelectListItem>();
        }

        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Details { get; set; }

        public int StatusId { get; set; }

        public int TypeId { get; set; }

        public List<SelectListItem> TaskStatuses { get; set; }

        public List<SelectListItem> TaskTypes { get; set; }
    }
}