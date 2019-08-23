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
    }
}