using DevFreela.API.Entities;
using DevFreela.API.Models;
using DevFreela.API.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly DevFreelaDbContext _dbContext;
        public UsersController(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _dbContext.Users
                .Include(u => u.Skills)
                .ThenInclude(x => x.Skill)
                .SingleOrDefault(x => x.Id == id);

            if (user == null)
                return NotFound();

            var model = UserViewModel.FromUser(user);

            return Ok(model);
        }

        [HttpPost]
        public IActionResult Post(CreateUserInputModel model)
        {
            var user = new User(model.FullName, model.Email, model.BirthDate);

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            return NoContent();
        }

        [HttpPost("{id}/skills")]
        public IActionResult PostSkill(int id, UserSkillInputModel model)
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
