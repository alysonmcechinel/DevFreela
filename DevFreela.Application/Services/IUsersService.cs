using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevFreela.Application.Models;

namespace DevFreela.Application.Services
{
    public interface IUsersService
    {
        ResultViewModel<List<UserViewModel>> GetUsers(string name, bool active = true, int page = 0, int rows = 3);
        ResultViewModel<UserViewModel> GetById(int id);
        ResultViewModel<int> Insert(CreateUserInputModel model);
        ResultViewModel Update(UpdateUserInputModel model);
        ResultViewModel Delete(int id);
        ResultViewModel ToggleUserStatus(int id);
    }
}
