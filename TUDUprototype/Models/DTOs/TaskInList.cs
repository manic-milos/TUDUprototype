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
        public int OrderNo { get; set; }

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
                TaskID = TaskID.Value
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
            var tils = await Task.Run(() => dbContext.TasksInLists.Where((x) => x.ListID == ListID));
            dbContext.AttachRange(tils);
            await Task.Run(() => dbContext.TasksInLists.RemoveRange(tils));
            var newTils = from t in allTasks
                          select t.ToEntity(ListID);
            await dbContext.TasksInLists.AddRangeAsync(newTils);
            await dbContext.SaveChangesAsync();
        }
    }
}
