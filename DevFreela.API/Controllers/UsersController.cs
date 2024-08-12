using DevFreela.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post(CreateUserInputModel model)
        {
            return Ok();
        }

        [HttpPost("{id}/skills")]
        public IActionResult PostSkill(UserSkillInputModel model)
        {
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
