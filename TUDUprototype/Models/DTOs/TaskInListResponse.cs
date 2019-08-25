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
    }
    public static class TaskInDTOContextExtensions
    {

        public static async Task<IEnumerable<TaskInListDTO>> GetTasksInListDTO(this TUDUDbContext dbContext,int ListID)
        {
            var tils = dbContext.TasksInLists.Where((x) => x.ListID == ListID);
            var taskstil = dbContext.TaskItems.Where((task) => tils.Any((til) => til.TaskID == task.ID)).Select((x) => new TaskInListDTO()
            {
                TaskID = x.ID,
                TaskName = x.TaskName,
                OrderNo = tils.First((y) => y.TaskID == x.ID).OrderNo
            });
            return taskstil.OrderBy((x)=>x.OrderNo).AsQueryable();
        }
    }
}
