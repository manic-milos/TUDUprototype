using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TUDUprototype.Models.DTOs;

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
                var r = dbContext.TaskItems.AsQueryable();
                return Ok(r.ToList());
            }
            catch(Exception e)
            {
                return new ObjectResult(e)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }

        [HttpGet("{ListID}")]
        public async Task<IActionResult> TaskItems(int ListID)
        {
            try
            {
                var r = await dbContext.GetTasksInListDTO(ListID);
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
        public async Task<IActionResult> PostTaskItem([FromBody] Models.TaskItem taskItem, int? ListID = null)
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

                //TODO taskinlist update
                if(ListID.HasValue)
                {
                    var tilresult = dbContext.TasksInLists.Count((x) => x.ListID == ListID);
                    var til = new Models.TaskInList()
                    {
                        ListID = ListID.Value,
                        TaskID = result.ID.Value,
                        OrderNo = tilresult+1
                    };
                    await dbContext.TasksInLists.AddAsync(til);
                    await dbContext.SaveChangesAsync();
                }

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