using DevFreela.API.Entities;
using DevFreela.API.Models;
using DevFreela.API.Persistence;
using DevFreela.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DevFreela.API.Controllers
{
    [ApiController]
    [Route("api/projects")]
    public class ProjectsController : ControllerBase
    {
        private readonly DevFreelaDbContext _dbContext;

        public ProjectsController(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Get(string search = "")
        {
            var projects = _dbContext.Projects
                .Include(x => x.Client)
                .Include(x => x.Freelancer)
                .Where(x => !x.IsDeleted).ToList();

            var model = projects.Select(ProjectItemViewModel.FromProject).ToList();

            return Ok(model);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var project = _dbContext.Projects
                .Include(x => x.Client)
                .Include(x => x.Freelancer)
                .Include(x => x.Comments)
                .SingleOrDefault(x => x.Id == id);

            if (project == null)
                    return NotFound();

            var model = ProjectViewModel.FromProject(project);

            return Ok();
        }

        [HttpPost]
        public IActionResult Post(CreateProjectInputModel model)
        {
            var project = model.ToProject();

            _dbContext.Projects.Add(project);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = project.Id }, model);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, UpdateProjectInputModel model)
        {
            var project = _dbContext.Projects.SingleOrDefault(x => x.Id == id);

            if (project is null)
                return NotFound();

            project.Update(model.Title, model.Description, model.TotalCost);

            _dbContext.Projects.Update(project);
            _dbContext.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(x => x.Id == id);

            if (project is null)
                return NotFound();

            project.Cancel();

            _dbContext.Projects.Update(project);
            _dbContext.SaveChanges();

            return NoContent();
        }

        [HttpPut("{id}/start")]
        public IActionResult Start(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(x => x.Id == id);

            if (project is null)
                return NotFound();

            project.Start();

            _dbContext.Projects.Update(project);
            _dbContext.SaveChanges();

            return NoContent();
        }

        [HttpPut("{id}/complete")]
        public IActionResult Complete(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(x => x.Id == id);

            if (project is null)
                return NotFound();

            project.Complete();

            _dbContext.Projects.Update(project);
            _dbContext.SaveChanges();

            return NoContent();
        }

        [HttpPost("{id}/comments")]
        public IActionResult PostComment(int id, CreateProjectCommentInputModel model)
        {
            var project = _dbContext.Projects.SingleOrDefault(x => x.Id == id);

            if (project is null)
                return NotFound();

            var comment = new ProjectComment(model.Content, model.IdProject, model.IdUser);

            _dbContext.ProjectComments.Add(comment);
            _dbContext.SaveChanges();

            return Ok();
        }
    }
}
