using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TUDUprototype.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskItemsController : ControllerBase
    {
        protected readonly Models.TUDUDbContext dbContext;

        public TaskItemsController(Models.TUDUDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet("TaskItems")]
        public IEnumerable<Models.TaskItem> TaskItems()
        {
            try
            {
                var r = dbContext.TaskItems.ToList();
                return r;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        [HttpPost("TaskItem")]
        public Models.TaskItem PostTaskItem([FromBody] Models.TaskItem taskItem)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    throw new ArgumentException();
                }

                dbContext.TaskItems.Add(taskItem);
                dbContext.SaveChanges();
                var result = dbContext.Entry(taskItem).Entity;
                return result;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}