using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskTracker.Classes;
using TaskTracker.Classes.Db.Structure;
using TaskTracker.Classes.Helpers;
using TaskTracker.Models.Task;

namespace TaskTracker.Controllers
{
    public class TaskController : Controller
    {
        private TaskManager _taskManager = null;

        public TaskController()
        {
            var dbContext = new DbContext();
            _taskManager = new TaskManager(dbContext);
        }

        public ActionResult Index()
        {
            var model = new TaskListModel();
            model.Tasks = _taskManager.GetAllTasks();

            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new CreateTaskModel();

            var statuses = EnumHelpers.GetTaskStatuses();
            foreach(var st in statuses)
                model.TaskStatuses.Add(new SelectListItem() { Text = st.DescriptionAttr(), Value = ((int)st).ToString() });

            var types = EnumHelpers.GetTaskTypes();
            foreach (var taskType in types)
                model.TaskTypes.Add(new SelectListItem() { Text = taskType.DescriptionAttr(), Value = ((int)taskType).ToString() });

            return View(model);
        }

        [HttpPost]
        public JsonResult Create(CreateTaskModel model)
        {
            bool succeed = false;
            if(model != null)
            {
                succeed = _taskManager.CreateTask(model);
            }
            return Json(new { success = succeed });
        }

        [HttpGet]
        public ActionResult Update(string taskId)
        {
            var model = new UpdateTaskModel();
            if (!String.IsNullOrEmpty(taskId))
            {
                TaskModel task = _taskManager.GetTaskById(int.Parse(taskId));
                model.Id = (int)task.Id;
                model.Title = task.Title;
                model.Details = task.Details;
                model.StatusId = (int)task.Status;
                model.TypeId = (int)task.Type;

                var statuses = EnumHelpers.GetTaskStatuses();
                foreach (var st in statuses)
                    model.TaskStatuses.Add(new SelectListItem() { Text = st.DescriptionAttr(), Value = ((int)st).ToString() });

                var types = EnumHelpers.GetTaskTypes();
                foreach (var taskType in types)
                    model.TaskTypes.Add(new SelectListItem() { Text = taskType.DescriptionAttr(), Value = ((int)taskType).ToString() });

                return View(model);
            }
            return HttpNotFound();
        }

        [HttpPost]
        public JsonResult Update(UpdateTaskModel model)
        {
            bool succeed = false;
            if(model != null)
            {
                succeed = _taskManager.UpdateTask(model);
            }
            return Json(new { success = succeed });
        }

        public ActionResult ShowDetails(string taskId)
        {
            if (!String.IsNullOrEmpty(taskId))
            {
                TaskModel task = _taskManager.GetTaskById(int.Parse(taskId));
                return View(task);
            }
            return HttpNotFound();
        }

        [HttpPost]
        public JsonResult Remove(string taskId)
        {
            bool succeed = false;
            if(!String.IsNullOrEmpty(taskId))
            {
                succeed = _taskManager.RemoveTask(int.Parse(taskId));
            }
            return Json(new { success = succeed});
        }

        public ActionResult FindTasks(string searchCriteria)
        {
            var model = new TaskListModel();
            model.Tasks = _taskManager.FindTasks(searchCriteria, searchCriteria);
            return View("Index", model);
        }

        protected override void Dispose(bool disposing)
        {
            if (_taskManager != null)
                _taskManager.Dispose();

            base.Dispose(disposing);
        }
    }
}