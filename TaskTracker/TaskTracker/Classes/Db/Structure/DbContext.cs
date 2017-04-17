using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using TaskTracker.Models;

namespace TaskTracker.Classes.Db.Structure
{
    public class DbContext : IdentityDbContext<ApplicationUser>
    {
        public DbContext()
            : base("TaskManagerDb", throwIfV1Schema: false)
        {
        }

        public static DbContext Create()
        {
            return new DbContext();
        }

        public DbSet<TaskUnit> Tasks { get; set; }
    }
}