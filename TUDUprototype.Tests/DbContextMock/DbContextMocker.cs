using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TUDUprototype.Models;

namespace TUDUprototype.Tests.DbContextMock
{
    public class DbContextMocker
    {
        public TUDUDbContext GetContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<TUDUDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            var dbContext = new TUDUDbContext(options);
            Seed(dbContext);
            return dbContext;
            
        }

        public virtual void Seed(TUDUDbContext dbContext)
        {
            dbContext.TaskLists.Add(new TaskList()
            {
                ListID = 1,
                ListName = "first",
                ProjectID = 5
            });
            dbContext.TaskLists.Add(new TaskList()
            {
                ListID = 2,
                ListName = "second",
                ProjectID = 5
            });

            dbContext.TaskItems.Add(new TaskItem()
            {
                ID = 1,
                TaskName = "first task",
                OriginalProjectID = 5
            });
            dbContext.TaskItems.Add(new TaskItem()
            {
                ID = 2,
                TaskName = "second task",
                OriginalProjectID = 5
            });
            dbContext.TaskItems.Add(new TaskItem()
            {
                ID = 3,
                TaskName = "third and second task",
                OriginalProjectID = 5
            });
            dbContext.TaskItems.Add(new TaskItem()
            {
                ID = 4,
                TaskName = "first task in second list",
                OriginalProjectID = 5
            });
            //Tasks in lists
            dbContext.TasksInLists.Add(new TaskInList()
            {
                ListID=1,
                OrderNo=1,
                TaskID=1
            });
            dbContext.TasksInLists.Add(new TaskInList()
            {
                ListID = 1,
                OrderNo = 2,
                TaskID = 2
            });
            dbContext.TasksInLists.Add(new TaskInList()
            {
                ListID = 1,
                OrderNo = 3,
                TaskID = 3
            });
            dbContext.TasksInLists.Add(new TaskInList()
            {
                ListID = 2,
                OrderNo = 1,
                TaskID = 4
            });
            dbContext.TasksInLists.Add(new TaskInList()
            {
                ListID = 2 ,
                OrderNo = 2,
                TaskID = 3
            });
            dbContext.SaveChanges();
        }
    }
}
