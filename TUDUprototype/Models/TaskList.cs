using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TUDUprototype.Models
{
    public class TaskList
    {
        [Key]
        public int ListID { get; set; }
        [MaxLength(50)]
        public string ListName { get; set; }
        public int ProjectID { get; set; }
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
