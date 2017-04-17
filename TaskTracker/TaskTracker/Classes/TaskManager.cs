using System;
using System.Collections.Generic;
using System.Linq;
using TaskTracker.Classes.Db.Repository;
using TaskTracker.Classes.Db.Structure;
using TaskTracker.Models.Task;

namespace TaskTracker.Classes
{
    public class TaskManager : IDisposable
    {
        private TaskUnitRepository _repository { get; set; }

        public TaskManager(DbContext context)
        {
            if (context == null)
                throw new ArgumentNullException("DbContext");

            _repository = new TaskUnitRepository(context);
        }

        public List<TaskModel> GetAllTasks()
        {
            var taskList = new List<TaskModel>();

            var dbTasks = _repository.GetAll().ToList();
            foreach (var dbTask in dbTasks)
            {
                taskList.Add(new TaskModel()
                {
                    Id = dbTask.Id,
                    Title = dbTask.Title,
                    Details = dbTask.Details,
                    Status = dbTask.Status,
                    Type = dbTask.Type
                });
            }
            return taskList;
        }

        public bool RemoveTask(int taskId)
        {
            int deletedRecordsCount = _repository.DeleteById(taskId);
            bool success = deletedRecordsCount != 0;
            return success;
        }

        public bool UpdateTask(UpdateTaskModel taskModel)
        {
            if (taskModel == null)
                throw new ArgumentNullException("taskModel");

            int updatedRecordsCount = _repository.Update((int)taskModel.Id, taskModel.Title, taskModel.Details, (TaskStatus)taskModel.StatusId, (TaskType)taskModel.TypeId);
            return updatedRecordsCount != 0;
        }

        public bool CreateTask(CreateTaskModel taskModel)
        {
            if (taskModel == null)
                throw new ArgumentNullException("taskModel");

            var newTask = new TaskUnit()
            {
                Title = taskModel.Title,
                Details = taskModel.Details,
                Status = (TaskStatus)taskModel.StatusId,
                Type = (TaskType)taskModel.TypeId
            };
            var createdRecordsCount = _repository.Insert(newTask);
            bool succeed = createdRecordsCount != 0;
            return succeed;
        }

        public TaskModel GetTaskById(int taskId)
        {
            var dbTask = _repository.GetById(taskId);
            if (dbTask == null)
                throw new InvalidOperationException("The task with a given key was not found");

            var taskModel = new TaskModel();
            taskModel.Id = dbTask.Id;
            taskModel.Title = dbTask.Title;
            taskModel.Details = dbTask.Details;
            taskModel.Status = dbTask.Status;
            taskModel.Type = dbTask.Type;

            return taskModel;
        }

        public List<TaskModel> FindTasks(string title, string description)
        {
            var taskList = new List<TaskModel>();
            var searchedTasks = _repository.SearchFor(x => x.Title.Contains(title) || x.Details.Contains(description)).ToList();
            foreach (var dbTask in searchedTasks)
            {
                taskList.Add(new TaskModel()
                {
                    Id = dbTask.Id,
                    Title = dbTask.Title,
                    Details = dbTask.Details,
                    Status = dbTask.Status,
                    Type = dbTask.Type
                });
            }

            return taskList;
        }

        public void Dispose()
        {
            if (_repository != null)
                _repository.Dispose();
        }
    }
}