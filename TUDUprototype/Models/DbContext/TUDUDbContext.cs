using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TUDUprototype.Models
{
    public class TUDUDbContext:DbContext
    {
        public TUDUDbContext(DbContextOptions<TUDUDbContext> options)
            :base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TaskItemsConfiguration());
            modelBuilder.ApplyConfiguration(new TaskListConfiguration());
            modelBuilder.ApplyConfiguration(new TaskInListConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<TaskItem> TaskItems { get; set; }

        public DbSet<TaskList> TaskLists { get; set; }

        public DbSet<TaskInList> TasksInLists{ get; set; }
    }
}
