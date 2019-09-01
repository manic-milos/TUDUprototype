using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace TUDUprototype.Tests
{
	public class EFRelatedPropertyTest
	{
		[Fact]
		public void TaskPropertyTestInTIL()
		{
			//Arrange
			var db = new DbContextMock.DbContextMocker().GetContext();

			//Act
			var tils = db.TasksInLists;

			//Assert
			foreach (var til in tils)
			{
				Assert.NotNull(til.Task);
				Assert.NotNull(til.List);
			}
			db.Dispose();
		}
		[Fact]
		public void TaskPropertyTestListInTask()
		{
			//Arrange
			var db = new DbContextMock.DbContextMocker().GetContext();

			//Act
			var tasks = db.TaskItems;

			//Assert
			foreach (var t in tasks)
			{
				var tils = db.TasksInLists.Where((x) => x.TaskID == t.ID).Count();
				//if (tils == 0)
				//    Assert.Null(t.TasksInLists);
				//else
				Assert.Equal(tils, t.TasksInLists.Count);
				//Assert.NotNull(t);
			}
			db.Dispose();
		}
		[Fact]
		public void ListPropertyTestListInTask()
		{
			//Arrange
			var db = new DbContextMock.DbContextMocker().GetContext();

			//Act
			var lists = db.TaskLists;

			//Assert
			foreach (var l in lists)
			{
				Assert.NotNull(l.TasksInLists);
				//Assert.NotNull(t);
			}
			db.Dispose();
		}

		[Fact]
		public void DBEmptyContextMockerIsEmpty()
		{
			//Arrange
			var db = new DbContextMock.DbEmptyContextMocker().GetContext();

			//Assert
			Assert.Empty(db.TaskLists);
			Assert.Empty(db.TaskItems);
			Assert.Empty(db.TaskLists);
			db.Dispose();
		}

		[Fact]
		public void AddingTaskAndTaskInList()
		{
			//Arrange
			var db = new DbContextMock.DbEmptyContextMocker().GetContext();
			db.TaskLists.Add(new Models.TaskList()
			{
				ListID = 1,
				ListName = "test list",
				ProjectID = 5
			});
			db.SaveChanges();

			//Act
			var list = db.TaskLists.Find(1);
			var task = new Models.TaskItem()
			{
				ID = null,
				TaskName = "test task",
				OriginalProjectID = 5
			};
			var taskinlist = new Models.TaskInList()
			{
				List = list,
				Task = task,
				OrderNo = 1
			};

			db.TaskItems.Add(task);
			db.TasksInLists.Add(taskinlist);
			db.SaveChanges();

			//Assert

			//task and taskinlist added
			Assert.NotEmpty(db.TaskItems);
			Assert.NotEmpty(db.TasksInLists);

			var taskinlistindb = db.TasksInLists.First();
			var taskindb = db.TaskItems.First();
			Assert.Equal(taskindb.ID, taskinlistindb.TaskID);

			db.Dispose();

		}

		[Fact]
		public void AddingTaskInListAndTask()
		{
			//Arrange
			var db = new DbContextMock.DbEmptyContextMocker().GetContext();
			db.TaskLists.Add(new Models.TaskList()
			{
				ListID = 1,
				ListName = "test list",
				ProjectID = 5
			});
			db.SaveChanges();

			//Act
			var list = db.TaskLists.Find(1);
			var task = new Models.TaskItem()
			{
				ID = 1,
				TaskName = "test task",
				OriginalProjectID = 5,
				TasksInLists = new List<Models.TaskInList>()
				{
					new Models.TaskInList()
					{
						List = list,
						OrderNo = 1
					}
				}
			};

			db.Add(task);
			db.SaveChanges();

			//Assert

			//task and taskinlist added
			Assert.NotEmpty(db.TaskItems);
			Assert.NotEmpty(db.TasksInLists);

			var taskinlistindb = db.TasksInLists.First();
			var taskindb = db.TaskItems.First();
			Assert.Equal(taskindb.ID, taskinlistindb.TaskID);

			db.Dispose();

		}
	}
}
