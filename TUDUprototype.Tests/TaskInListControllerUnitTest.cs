using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUDUprototype.Controllers;
using TUDUprototype.Models.DTOs;
using Xunit;

namespace TUDUprototype.Tests
{
    public class TaskInListControllerUnitTest
    {
        [Fact]
        public async Task TestPutTasksInListClearAsync()
        {
            //Arrange
            var dbContext = new DbContextMock.DbContextMocker().GetContext();
            var controller = new TaskInListController(dbContext);

            //Act
            int listID = 1;
            var response = await controller.PutTasksInList(listID, new List<TaskInListDTO>()) as ObjectResult;


            //Assert
            Assert.Empty(dbContext.TasksInLists.Where((x) => x.ListID == listID));
			dbContext.Dispose();

		}
        [Fact]
        public async Task TestPutTasksInList_CleanAndPutAddExisting1Async()
        {
            //Arrange
            var dbContext = new DbContextMock.DbContextMocker().GetContext();
            var controller = new TaskInListController(dbContext);

            //Act
            int listID = 1;
            var task = dbContext.TaskItems.First();
            var response = await controller.PutTasksInList(listID, new List<TaskInListDTO>() {
                new TaskInListDTO()
                {
                    OrderNo=1,
                    TaskID=task.ID,
                    TaskName=task.TaskName
                }
            }) as ObjectResult;


            //Assert
            Assert.Equal(1,dbContext.TasksInLists.Count((x) => x.ListID == listID));
            Assert.Equal(task.ID, dbContext.TasksInLists.First((x) => x.ListID == listID).TaskID);
			dbContext.Dispose();
		}

		[Fact]
		public async Task TestPutTasksInList_CleanAndPutAdd1Async()
		{
			//Arrange
			var dbContext = new DbContextMock.DbContextMocker().GetContext();
			var controller = new TaskInListController(dbContext);

			//Act
			int listID = 1;
			var response = await controller.PutTasksInList(listID, new List<TaskInListDTO>() {
				new TaskInListDTO()
				{
					OrderNo=1,
					TaskID=null,
					TaskName="test add task"
				}
			}) as ObjectResult;


			//Assert
			Assert.Equal(1, dbContext.TasksInLists.Count((x) => x.ListID == listID));
			//Assert.Equal(task.ID, dbContext.TasksInLists.First((x) => x.ListID == listID).TaskID);
			dbContext.Dispose();
		}

	}
}
