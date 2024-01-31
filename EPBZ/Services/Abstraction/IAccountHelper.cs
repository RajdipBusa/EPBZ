using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface IAccountHelper
    {
        bool CheckExistsEmail(string email);
        void DeleteUser(int userId);
        string GenerateToken(UserModel validatedUser);
        IEnumerable<UserModel> GetAllUser();
        void Register(UserModel userModel);
        UserModel Signin(LoginRequestModel loginModel);
    }
}
