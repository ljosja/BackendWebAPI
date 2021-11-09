using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendWebAPI.Models
{
    public class EFProjectsRepository : IProjectsRepository
    {
        private EFProjectsDBContext Context;

        public ProjectEntry GetProjectEntry(int Id)
        {
            return Context.Projects.Find(Id);
        }
        public Project GetProject(int Id)
        {
            ProjectEntry projectEntry = Context.Projects.Find(Id);
            Project Project = new Project();
            if (projectEntry != null)
            {
                Project.Id = projectEntry.Id;
                Project.ProjectLead = projectEntry.ProjectLead;
                Project.IsComplete = projectEntry.IsComplete;
                Project.ProjectTasks = new List<ProjectTask>();
                foreach (ProjectTaskEntry TaskEntry in Context.Tasks)
                {
                    if (TaskEntry.ProjectId == Project.Id)
                    {
                        ProjectTask TaskItem = new ProjectTask();
                        TaskItem.Id = TaskEntry.Id;
                        TaskItem.ProjectId = TaskEntry.ProjectId;
                        TaskItem.ShortDescription = TaskEntry.ShortDescription;
                        TaskDescriptionEntry TaskDescription = Context.TaskDescriptions.Find(TaskItem.Id);
                        if (TaskDescription != null)
                        {
                            TaskItem.TaskDescription = TaskDescription;
                        }
                        Project.ProjectTasks.Add(TaskItem);
                    }
                }
            }
            return Project;
        }
        public IEnumerable<Project> GetProject()
        {
            List<Project> Projects = new List<Project>();
            foreach (ProjectEntry ProjectEntry in Context.Projects)
            {
                int Id = ProjectEntry.Id;
                Projects.Add(GetProject(Id));
            }
            return Projects;
        }
        /*public IEnumerable<ProjectTask> GetTask()
        {
            List<ProjectTask> Tasks = new List<ProjectTask>();
        }*/

        public EFProjectsRepository(EFProjectsDBContext context)
        {
            Context = context;
        }
        public void CreateProject(ProjectEntry projectItem)
        {
            Context.Projects.Add(projectItem);
            Context.SaveChanges();
        }
        public void UpdateProject(ProjectEntry UpdatedProject)
        {
            ProjectEntry CurrentProject = GetProjectEntry(UpdatedProject.Id);
            CurrentProject.IsComplete = UpdatedProject.IsComplete;
            CurrentProject.ProjectLead = UpdatedProject.ProjectLead;

            Context.Projects.Update(CurrentProject);
            Context.SaveChanges();
        }
        public Project DeleteProject(int Id)
        {
            ProjectEntry ProjectItem = GetProjectEntry(Id);

            if (ProjectItem != null)
            {
                Context.Projects.Remove(ProjectItem);
                Context.SaveChanges();

                List<int> TaskIdsToDelete = new List<int>();
                foreach (ProjectTaskEntry TaskEntry in Context.Tasks)
                {
                    if (TaskEntry.ProjectId == ProjectItem.Id)
                    {
                        TaskIdsToDelete.Add(TaskEntry.Id);
                    }
                }
                foreach (int IdDelete in TaskIdsToDelete)
                {
                    DeleteTaskEntry(IdDelete);
                    DeleteTaskDescriptionEntry(IdDelete);
                }
            }

            return GetProject(ProjectItem.Id);
        }
        public ProjectTaskEntry GetTaskEntry(int Id)
        {
            return Context.Tasks.Find(Id);
        }
        public void CreateTask(ProjectTaskEntry taskItem)
        {
            Context.Tasks.Add(taskItem);
            Context.SaveChanges();
            TaskDescriptionEntry TaskDescription = new TaskDescriptionEntry();
            Context.TaskDescriptions.Add(TaskDescription);
            Context.SaveChanges();
        }
        public void UpdateTask(ProjectTaskEntry UpdatedTask)
        {
            ProjectTaskEntry CurrentTask = GetTaskEntry(UpdatedTask.Id);
            CurrentTask.ProjectId = UpdatedTask.ProjectId;
            CurrentTask.ShortDescription = UpdatedTask.ShortDescription;

            Context.Tasks.Update(CurrentTask);
            Context.SaveChanges();
        }
        public void DeleteTaskEntry(int Id)
        {
            ProjectTaskEntry TaskItem = GetTaskEntry(Id);
            Context.Tasks.Remove(TaskItem);
            Context.SaveChanges();
        }
        public void DeleteTaskDescriptionEntry(int Id)
        {
            TaskDescriptionEntry TaskDescripItem = GetTaskDescripEntry(Id);
            Context.TaskDescriptions.Remove(TaskDescripItem);
            Context.SaveChanges();
        }
        public ProjectTaskEntry DeleteTask(int Id)
        {
            ProjectTaskEntry TaskItem = GetTaskEntry(Id);
            if (TaskItem != null)
            {
                DeleteTaskEntry(Id);
                DeleteTaskDescriptionEntry(Id);
            }
            return TaskItem;
        }
        public TaskDescriptionEntry GetTaskDescripEntry(int Id)
        {
            return Context.TaskDescriptions.Find(Id);
        }
        public void UpdateTaskDescription(TaskDescriptionEntry UpdatedTaskDescrip)
        {
            TaskDescriptionEntry CurrentTaskDescrip = GetTaskDescripEntry(UpdatedTaskDescrip.Id);
            CurrentTaskDescrip.LongDescription = UpdatedTaskDescrip.LongDescription;

            Context.TaskDescriptions.Update(CurrentTaskDescrip);
            Context.SaveChanges();
        }
    }
}
