using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendWebAPI.Models
{
    public class ProjectTaskEntry
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string ShortDescription { get; set; }
    }
    public class ProjectTask
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string ShortDescription { get; set; }
        public TaskDescriptionEntry TaskDescription { get; set; }
    }
}
