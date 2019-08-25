using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TUDUprototype.Models
{
    public class TaskInList
    {
        public int TaskID { get; set; }
        public int ListID { get; set; }
        public int OrderNo { get; set; }
    }

    public class TaskInListConfiguration : IEntityTypeConfiguration<TaskInList>
    {
        public void Configure(EntityTypeBuilder<TaskInList> builder)
        {
            builder.ToTable("TaskInList");

            builder.HasKey("TaskID", "ListID", "OrderNo");

            builder.Property((x) => x.TaskID).IsRequired().HasColumnType("int");
            builder.Property((x) => x.ListID).IsRequired().HasColumnType("int");
            builder.Property((x) => x.OrderNo).IsRequired().HasColumnType("int");


        }
    }
}
