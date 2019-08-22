using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TUDUprototype.Models
{
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
