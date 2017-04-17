using System;
using System.Linq;
using System.Linq.Expressions;
using TaskTracker.Classes.Db.Structure;

namespace TaskTracker.Classes.Db.Repository
{
    public class TaskUnitRepository : IRepository<TaskUnit>
    {
        private DbContext _context = null;

        public TaskUnitRepository(DbContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            _context = context;
        }

        public void Delete(TaskUnit taskUnit)
        {
            if (taskUnit == null)
                throw new ArgumentNullException("taskUnit");

            _context.Tasks.Remove(taskUnit);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            if(_context != null)
                _context.Dispose();
        }

        public IQueryable<TaskUnit> GetAll()
        {
            var allTasks = _context.Tasks;
            return allTasks;
        }

        public TaskUnit GetById(object id)
        {
            if (id.GetType() != typeof(int))
                throw new ArgumentException("The id parameter sended to the GetById method should have a non negative integer value");

            var dbTask = _context.Tasks.SingleOrDefault(x => x.Id == (int)id);
            return dbTask;
        }

        public int DeleteById(int taskId)
        {
            int deletedRecordsCount = 0;
            var deletedTask = _context.Tasks.SingleOrDefault(x => x.Id == taskId);
            if(deletedTask != null)
            {
                _context.Tasks.Remove(deletedTask);
                deletedRecordsCount = _context.SaveChanges();
            }

            return deletedRecordsCount;
        }

        public int Insert(TaskUnit taskUnit)
        {
            if (taskUnit == null)
                throw new ArgumentNullException("taskUnit");

            _context.Tasks.Add(taskUnit);
            return _context.SaveChanges();
        }

        public IQueryable<TaskUnit> SearchFor(Expression<Func<TaskUnit, bool>> predicate)
        {
            var searchedTasks = _context.Tasks.Where(predicate);
            return searchedTasks;
        }

        public int Update(int updateTaskId, string title, string details, TaskStatus status, TaskType type)
        {
            int updatedRecordsCount = 0;
            var dbTask = _context.Tasks.SingleOrDefault(x => x.Id == updateTaskId);
            if(dbTask != null)
            {
                dbTask.Title = title;
                dbTask.Details = details;
                dbTask.Status = status;
                dbTask.Type = type;
                updatedRecordsCount = _context.SaveChanges();
            }

            return updatedRecordsCount;
        }
    }
}