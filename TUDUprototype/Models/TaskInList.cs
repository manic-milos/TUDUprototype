using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TUDUprototype.Models
{
    public class TaskInList
    {
        [Required]
        public int? TaskID { get; set; }
        [Required]
        public int? ListID { get; set; }
        [Required]
        public int? OrderNo { get; set; }
        [ForeignKey("TaskID")]
        public TaskItem Task { get; set; }
        [ForeignKey("ListID")]
        public TaskList List{ get; set; }
    }

    public class TaskInListConfiguration : IEntityTypeConfiguration<TaskInList>
    {
        public void Configure(EntityTypeBuilder<TaskInList> builder)
        {
            builder.ToTable("TaskInList");

            builder.HasKey((x)=>new { x.TaskID, x.ListID, x.OrderNo });

            builder.Property((x) => x.TaskID).IsRequired().HasColumnType("int");
            builder.Property((x) => x.ListID).IsRequired().HasColumnType("int");
            builder.Property((x) => x.OrderNo).IsRequired().HasColumnType("int");


        }
    }
}
