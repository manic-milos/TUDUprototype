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

}

