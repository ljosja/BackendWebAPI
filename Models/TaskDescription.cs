using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendWebAPI.Models
{
    public class TaskDescriptionEntry
    {
        public int Id { get; set; } // TaskId
        public string LongDescription { get; set; }
    }
}
