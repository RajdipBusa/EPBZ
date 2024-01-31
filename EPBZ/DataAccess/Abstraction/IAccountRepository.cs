using DataAccess.Repository;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstraction
{
    public interface IAccountRepository : IGenericRepository<UserModel>
    {
        UserModel GetUserByEmail(string email);
        void InsertUserDetails(UserModel user);
        UserModel Login(string email, string password);
    }
}
