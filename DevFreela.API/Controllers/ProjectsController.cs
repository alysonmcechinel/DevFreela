using DevFreela.API.Models;
using DevFreela.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DevFreela.API.Controllers
{
    [ApiController]
    [Route("api/projects")]
    public class ProjectsController : ControllerBase
    {
        public ProjectsController()
        {
        }

        [HttpGet]
        public IActionResult Get(string search)
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult Post(CreateProjectInputModel model)
        {
            return CreatedAtAction(nameof(GetById), new { id = 0 }, model);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, UpdateProjectInputModel model)
        {
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return NoContent();
        }

        [HttpPut("{id}/start")]
        public IActionResult Start(int id)
        {
            return NoContent();
        }

        [HttpPut("{id}/complete")]
        public IActionResult Complete(int id)
        {
            return NoContent();
        }

        [HttpPost("{id}/comments")]
        public IActionResult PostComment(int id, CreateProjectCommentInputModel model)
        {
            return Ok();
        }
    }
}
