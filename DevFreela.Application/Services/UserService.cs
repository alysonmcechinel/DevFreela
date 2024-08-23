using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure;
using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Services
{
    public class UserService : IUserService
    {
        private readonly DevFreelaDbContext _dbContext;

        public UserService(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ResultViewModel<List<UserViewModel>> GetUsers(string name, bool active = true, int page = 0, int rows = 3)
        {
            var users = _dbContext.Users
                .Include(i => i.Skills)
                .Where(x => !x.IsDeleted 
                            && x.Active == active
                            && (name == "" || x.FullName.Contains(name) || x.Email.Contains(name)))
                .Skip(page * rows)
                .Take(rows)
                .ToList();

            if (!users.Any())
                return ResultViewModel<List<UserViewModel>>.Error("Nenhum usuario encontrado");

            var model = users.Select(UserViewModel.FromUser).ToList();

            return ResultViewModel<List<UserViewModel>>.Sucess(model);
        }

        public ResultViewModel<UserViewModel> GetById(int id)
        {
            var user = _dbContext.Users
                .Include(u => u.Skills)
                .SingleOrDefault(x => x.Id == id);

            if (user == null)
                return ResultViewModel<UserViewModel>.Error("Usuario não existe!!");

            var model = UserViewModel.FromUser(user);

            return ResultViewModel<UserViewModel>.Sucess(model);
        }

        public ResultViewModel<int> Insert(CreateUserInputModel model)
        {
            var user = new User(model.FullName, model.Email, model.BirthDate);

            if (DoesFullNameExist(model.FullName))
                return ResultViewModel<int>.Error("Já existe um usuario com esté nome");

            if (DoesEmailExist(model.Email))
                return ResultViewModel<int>.Error("Já existe um usuario com este e-mail");

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            return ResultViewModel<int>.Sucess(user.Id);
        }

        public ResultViewModel Update(UpdateUserInputModel model)
        {
            var user = _dbContext.Users.SingleOrDefault(x => x.Id == model.IdUser);

            if (user is null)
                return ResultViewModel.Error("Usuario não existe");

            if (DoesFullNameExist(model.FullName))
                return ResultViewModel<int>.Error("Já existe um usuario com esté nome");

            if (DoesEmailExist(model.Email))
                return ResultViewModel<int>.Error("Já existe um usuario com este e-mail");

            _dbContext.Users.Update(user);
            _dbContext.SaveChanges();

            return ResultViewModel.Sucess();
        }

        public ResultViewModel InsertSkill(int id, UserSkillInputModel model)
        {
            var userSkill = model.IdsSkill.Select(x => new UserSkill(id, x)).ToList();

            _dbContext.UserSkills.AddRange(userSkill);
            _dbContext.SaveChanges();

            return ResultViewModel.Sucess();
        }

        public ResultViewModel Delete(int id)
        {
            var user = _dbContext.Users.SingleOrDefault(x => x.Id == id);

            if (user is null)
                return ResultViewModel.Error("Usuario não existe!!");

            user.IsDeleted = false;

            _dbContext.Users.Update(user);
            _dbContext.SaveChanges();

            return ResultViewModel.Sucess();
        }

        private bool DoesFullNameExist(string verify) =>
            _dbContext.Users.ToList().Any(x => x.FullName.Equals(verify, StringComparison.OrdinalIgnoreCase));

        private bool DoesEmailExist(string verify) =>
            _dbContext.Users.ToList().Any(x => x.Email.Equals(verify, StringComparison.OrdinalIgnoreCase));
    }
}
