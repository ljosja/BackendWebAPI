using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace BackendWebAPI.Models
{
    public class EFProjectsDBContext : DbContext
    {
        public EFProjectsDBContext(DbContextOptions<EFProjectsDBContext> options) : base(options)
        { }
        public DbSet<ProjectEntry> Projects { get; set; }
        public DbSet<ProjectTaskEntry> Tasks { get; set; }
        public DbSet<TaskDescriptionEntry> TaskDescriptions { get; set; }
    }
}
