using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using BackendWebAPI.Models;

namespace BackendWebAPI.Controllers
{
    [Route("api/[controller]")]
    public class ProjectsController : Controller
    {
        IProjectsRepository ProjectsRepository;

        public ProjectsController(IProjectsRepository projectRepository)
        {
            ProjectsRepository = projectRepository;
        }

        [HttpGet(Name = "GetAllProjects")]
        public IEnumerable<Project> GetProject()
        {
            return ProjectsRepository.GetProject();
        }

        /*[HttpGet("GetAllTasks")]
        public IEnumerable<Project> GetTask()
        {
            return ProjectsRepository.GetTask();
        }*/

        [HttpGet("id", Name = "GetProjectItem")]
        public IActionResult GetProjectById(int Id)
        {
            Project projectItem = ProjectsRepository.GetProject(Id);

            if (projectItem == null)
            {
                return NotFound();
            }

            return new ObjectResult(projectItem);
        }

        [HttpPost("AddProject")]
        public IActionResult AddProject([FromBody] ProjectEntry projectItem)
        {
            if (projectItem == null)
            {
                return BadRequest();
            }
            ProjectsRepository.CreateProject(projectItem);
            return CreatedAtRoute("GetProjectItem", new { id = projectItem.Id }, projectItem);
        }

        [HttpPost("AddTask")]
        public IActionResult AddTask([FromBody] ProjectTaskEntry taskItem)
        {
            if (taskItem == null)
            {
                return BadRequest();
            }
            ProjectsRepository.CreateTask(taskItem);
            return CreatedAtRoute("GetAllProjects", new { id = taskItem.Id }, taskItem);
        }

        [HttpPut("UpdateProject")]
        public IActionResult UpdateProject(int Id, [FromBody] ProjectEntry UpdatedProjectItem)
        {
            if (UpdatedProjectItem == null || UpdatedProjectItem.Id != Id) 
            {
                return BadRequest();
            }

            var projectItem = ProjectsRepository.GetProjectEntry(Id);
            if (projectItem == null)
            {
                return NotFound();
            }

            ProjectsRepository.UpdateProject(UpdatedProjectItem);
            return GetProjectById(UpdatedProjectItem.Id); // RedirectToRoute("GetAllProjects");
        }
        [HttpPut("UpdateTask")]
        public IActionResult UpdateTask(int Id, [FromBody] ProjectTaskEntry UpdatedTaskItem)
        {
            if (UpdatedTaskItem == null || UpdatedTaskItem.Id != Id)
            {
                return BadRequest();
            }

            var taskItem = ProjectsRepository.GetTaskEntry(Id);
            if (taskItem == null)
            {
                return NotFound();
            }

            ProjectsRepository.UpdateTask(UpdatedTaskItem);
            return GetProjectById(UpdatedTaskItem.ProjectId);
        }
        [HttpPut("AddOrUpdateTaskDescription")]
        public IActionResult AddOrUpdateTaskDescription(int Id, [FromBody] TaskDescriptionEntry UpdatedTaskDescripItem)
        {
            if (UpdatedTaskDescripItem == null || UpdatedTaskDescripItem.Id != Id)
            {
                return BadRequest();
            }

            var taskItem = ProjectsRepository.GetTaskEntry(Id);
            if (taskItem == null)
            {
                return NotFound();
            }

            ProjectsRepository.UpdateTaskDescription(UpdatedTaskDescripItem);
            return GetProjectById(taskItem.ProjectId);
        }

        [HttpDelete("DeleteProject")]
        public IActionResult DeleteProject(int Id)
        {
            var deletedProjectItem = ProjectsRepository.DeleteProject(Id);

            if (deletedProjectItem == null)
            {
                return BadRequest();
            }

            return new ObjectResult(deletedProjectItem);
        }

        [HttpDelete("DeleteTask")]
        public IActionResult DeleteTask(int Id)
        {
            var deletedTaskItem = ProjectsRepository.DeleteTask(Id);

            if (deletedTaskItem == null)
            {
                return BadRequest();
            }

            return new ObjectResult(deletedTaskItem);
        }
    }
}
