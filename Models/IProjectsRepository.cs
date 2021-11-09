using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendWebAPI.Models
{
    public interface IProjectsRepository
    {
        // add/update/delete project
        IEnumerable<Project> GetProject(); // get all projects
        Project GetProject(int id); // get a project by id
        ProjectEntry GetProjectEntry(int id); // get an entry from Table Projects 
        void CreateProject(ProjectEntry item);
        void UpdateProject(ProjectEntry item);
        Project DeleteProject(int id);

        // add/update/delete task
        //IEnumerable<ProjectTask> GetTask(); // get all tasks
        void CreateTask(ProjectTaskEntry item);
        ProjectTaskEntry GetTaskEntry(int id); // get an entry from Table Tasks
        void UpdateTask(ProjectTaskEntry item);
        ProjectTaskEntry DeleteTask(int id);

        // update task description
        void UpdateTaskDescription(TaskDescriptionEntry UpdatedTaskDescrip);
    }
}
