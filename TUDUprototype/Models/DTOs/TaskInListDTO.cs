using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TUDUprototype.Models.DTOs
{
    public class TaskInListDTO
    {
        public int? TaskID { get; set; }
        public string TaskName { get; set; }
        public int? OrderNo { get; set; }

        public TaskInListDTO(TaskItem task, TaskInList til)
        {
            this.TaskID = task.ID;
            this.TaskName = task.TaskName;
            this.OrderNo = til.OrderNo;
        }

        public TaskInListDTO()
        {

        }

        public TaskInList ToEntity(int ListID)
        {
            return new TaskInList()
            {
                ListID = ListID,
                OrderNo = OrderNo,
                TaskID = TaskID
            };
        }
    }
    public static class TaskInDTOContextExtensions
    {

        public static async Task<IEnumerable<TaskInListDTO>> GetTasksInListDTOAsync(this TUDUDbContext dbContext, int ListID)
        {
            var tils = await Task.Run(() => dbContext.TasksInLists.Where((x) => x.ListID == ListID));
            var taskstil = await Task.Run(() => dbContext.TaskItems.Where((task) => tils.Any((til) => til.TaskID == task.ID)).Select((x) => new TaskInListDTO()
            {
                TaskID = x.ID,
                TaskName = x.TaskName,
                OrderNo = tils.First((y) => y.TaskID == x.ID).OrderNo
            }));
            return await Task.Run(() => taskstil.OrderBy((x) => x.OrderNo).AsQueryable());
        }

        public static async Task ReplaceTasksInList(this TUDUDbContext dbContext, int ListID, IEnumerable<TaskInListDTO> allTasks)
        {
			var list = dbContext.TaskLists.Include((x) => x.TasksInLists).FirstOrDefault((l) => l.ListID == ListID);
			if (list == null)
				throw new ArgumentOutOfRangeException("listID not in table");
            await Task.Run(() => dbContext.TasksInLists.RemoveRange(list.TasksInLists));
            await dbContext.SaveChangesAsync();

            //add new
            var newTils = allTasks.Where((x) => x.TaskID == null);
            if (newTils.Any())
            {
                var newTasks = newTils.Select((x) => new TaskItem()
                {
                    ID = null,
                    OriginalProjectID = list.ProjectID,
                    TaskName = x.TaskName,
                    TasksInLists=new List<TaskInList>() { x.ToEntity(ListID) }
                });
                await dbContext.AddRangeAsync(newTasks);
                await dbContext.SaveChangesAsync();

            }


            //insert all tasks in list
            var newTilList = allTasks.Where((x)=>x.TaskID!=null).Select(t => t.ToEntity(ListID));
            await dbContext.TasksInLists.AddRangeAsync(newTilList);
            await dbContext.SaveChangesAsync();

			var nameTilList = allTasks.Where((x) => x.TaskID != null);
			foreach(var tl in nameTilList)
			{
				var t = await dbContext.TaskItems.SingleAsync((x)=>x.ID==tl.TaskID);
				t.TaskName = tl.TaskName;
			}
			await dbContext.SaveChangesAsync();
        }
    }
}
