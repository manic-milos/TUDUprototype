using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TUDUprototype.Models
{
	public class TaskList
	{
		public TaskList()
		{
			TasksInLists = new List<TaskInList>();
		}
		[Key]
		public int ListID { get; set; }
		[MaxLength(50)]
		public string ListName { get; set; }
		public int ProjectID { get; set; }
		[InverseProperty("List")]
		public virtual ICollection<TaskInList> TasksInLists
		{
			get;
			set;
		}
	}
	public class TaskListConfiguration : IEntityTypeConfiguration<TaskList>
	{
		public void Configure(EntityTypeBuilder<TaskList> builder)
		{
			builder.ToTable("Lists");

			builder.HasKey((x) => x.ListID);
			builder.Property((x) => x.ListID)
				.HasColumnType("int")
				.IsRequired()
				.HasDefaultValueSql("next value for [Sequences].[ListID]");

			builder.Property((x) => x.ListName)
				.HasColumnType("nvarchar(50)");

			builder.Property((x) => x.ProjectID)
				.HasColumnType("int");



		}
	}
}
