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
        private readonly FreelanceTotalCostConfig _config;
        private readonly IConfigService _configService;

        public ProjectsController(IOptions<FreelanceTotalCostConfig> options, IConfigService configService)
        {
            _config = options.Value;
            _configService = configService;
        }

        [HttpGet]
        public IActionResult Get(string search)
        {
            return Ok(_configService.GetIncrement());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            throw new Exception();
            return Ok();
        }

        [HttpPost]
        public IActionResult Post(CreateProjectViewModel model)
        {
            if(_config.Minimum > model.TotalCost || model.TotalCost > _config.Maximum)
                return BadRequest("Total cost is out of range.");

            return CreatedAtAction(nameof(GetById), new { id = 0 }, model);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, UpdateProjectViewModel model)
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
        public IActionResult PostComment(int id, CreateProjectCommentViewModel model)
        {
            return Ok();
        }
    }
}
