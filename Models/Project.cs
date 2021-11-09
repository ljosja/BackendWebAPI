using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendWebAPI.Models
{
    public class ProjectEntry
    {
        public int Id { get; set; }
        public string ProjectLead { get; set; }
        public bool IsComplete { get; set; }
    }
    public class Project
    {
        public int Id { get; set; }
        public string ProjectLead { get; set; }
        public bool IsComplete { get; set; }
        public List<ProjectTask> ProjectTasks { get; set; }
    }
}
