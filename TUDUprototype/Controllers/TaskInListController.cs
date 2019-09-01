using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TUDUprototype.Models;
using TUDUprototype.Models.DTOs;

namespace TUDUprototype.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskInListController : ControllerBase
    {
        TUDUDbContext dbContext;

        public TaskInListController(TUDUDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet("{ListID}")]
        public async Task<IActionResult> GetTasksInList(int ListID)
        {
            try
            {
                var tils = await dbContext.GetTasksInListDTOAsync(ListID);
                return Ok(tils);

            }
            catch(Exception e)
            {
                return new ObjectResult(e)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }

        [HttpPut("ReplaceTasks/{ListID}")]
        public async Task<IActionResult> PutTasksInList(int ListID,[FromBody] IEnumerable<TaskInListDTO> tasks)
        {

            try
            {
                await dbContext.ReplaceTasksInList(ListID, tasks);
                return Ok();

            }
            catch (Exception e)
            {
                return new ObjectResult(e)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }
    }
}