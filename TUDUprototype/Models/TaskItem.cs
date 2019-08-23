using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TUDUprototype.Models
{
    public class TaskItem
    {
        public TaskItem()
        {
        }

        public TaskItem(int iD, string taskName, int originalProjectID)
        {
            ID = iD;
            TaskName = taskName;
            OriginalProjectID = originalProjectID;
        }

        public int ID { get; set; }
        [MaxLength(50)]
        public string TaskName { get; set; }
        public int OriginalProjectID { get; set; }
    }

    public class TaskItemsConfiguration : IEntityTypeConfiguration<TaskItem>
    {
        public void Configure(EntityTypeBuilder<TaskItem> builder)
        {
            builder.ToTable("Tasks");
            //set key and autoincrement
            builder.HasKey((x) => x.ID);
            builder.Property((x) => x.ID)
                .HasColumnType("int")
                .IsRequired()
                .HasDefaultValueSql("next value for [Sequences].[ID]");

            //configuration for columns
            builder.Property((x) => x.TaskName).HasColumnType("nvarchar(50)");
            builder.Property((x) => x.OriginalProjectID).HasColumnType("int").IsRequired();

        }
    }
}

