using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TUDUprototype.Models;

namespace TUDUprototype.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskListsController : ControllerBase
    {
        TUDUDbContext dbContext;

        public TaskListsController(TUDUDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet("TaskLists")]
        public IActionResult GetLists()
        {
            var result = dbContext.TaskLists.ToList();
            return Ok(result);
        }

    }
}