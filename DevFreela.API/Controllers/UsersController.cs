using DevFreela.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DevFreela.Infrastructure.Persistence;
using DevFreela.Application.Models;
using DevFreela.Application.Services;

namespace DevFreela.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly IUsersService _usersService;

        public UsersController(DevFreelaDbContext dbContext, IUsersService usersService)
        {
            _dbContext = dbContext;
            _usersService = usersService;
        }

        [HttpGet]
        public IActionResult Get(string name, bool active = true, int page = 0, int rows = 3)
        {
            var result = _usersService.GetUsers(name, active, page, rows);

            if (!result.IsSucess)
                return NotFound(result.Message);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _usersService.GetById(id);

            if (!result.IsSucess)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post(CreateUserInputModel model)
        {
            var result = _usersService.Insert(model);

            if (!result.IsSucess)
                return BadRequest(result.Message);

            return CreatedAtAction(nameof(GetById), new { id = result.Data });
        }

        [HttpPut("{id}")]
        public IActionResult Put(UpdateUserInputModel model)
        {
            var result = _usersService.Update(model);

            if (!result.IsSucess)
                return BadRequest(result.Message);

            return NoContent();
        }

        [HttpPost("{id}/skills")]
        public IActionResult InsertSkill(int id, UserSkillInputModel model)
        {
            var userSkill = model.IdsSkill.Select(x => new UserSkill(id, x)).ToList();

            _dbContext.UserSkills.AddRange(userSkill);
            _dbContext.SaveChanges();

            return NoContent();
        }


        [HttpPut("{id}/profile-picture")]
        public IActionResult PostProfilePicture(int id, IFormFile file)
        {
            var description = $"File : {file.FileName}, Size: {file.Length}";

            // Processar a img (salvar no banco algo do tipo)

            return Ok(description);
        }
    }
}
