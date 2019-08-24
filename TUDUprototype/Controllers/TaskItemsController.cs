using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        public async Task<IActionResult> TaskItems()
        {
            try
            {
                var r = dbContext.TaskItems;
                return Ok(r);
            }
            catch(Exception e)
            {
                return new ObjectResult(e)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }

        [HttpPost("TaskItem")]
        public async Task<IActionResult> PostTaskItem([FromBody] Models.TaskItem taskItem)
        {
            try
            {
                if(taskItem.ID!=null)
                    return new BadRequestObjectResult(ModelState);

                if(!ModelState.IsValid)
                {
                    return new BadRequestObjectResult(ModelState);
                }
                await dbContext.TaskItems.AddAsync(taskItem);
                await dbContext.SaveChangesAsync();
                var result = dbContext.Entry(taskItem).Entity;
                return Ok(result);
            }
            catch(Exception e)
            {
                return new ObjectResult(e)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }
    }
}