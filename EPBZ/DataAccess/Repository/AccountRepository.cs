using Dapper;
using DataAccess.Abstraction;
using Model;

namespace DataAccess.Repository
{
    public class AccountRepository : SqlDbRepository<UserModel>, IAccountRepository
    {
        public AccountRepository(string connectionString) : base(connectionString) { }

        public override void Delete(int userId)
        {
            using var connection = GetOpenConnection();
            var sql = "DELETE FROM Users WHERE UserId = @UserId";
            connection.Execute(sql, new { UserId = userId });
        }

        public override IEnumerable<UserModel> GetAll()
        {
            using var connection = GetOpenConnection();
            var sql = "SELECT * FROM Users";
            return connection.Query<UserModel>(sql);
        }

        public override IEnumerable<UserModel> GetAllById(int userId)
        {
            using var connection = GetOpenConnection();
            var sql = "SELECT * FROM Users WHERE UserId = @UserId";
            return connection.Query<UserModel>(sql, new { UserId = userId });
        }

        public override UserModel GetSingle(int userId)
        {
            using var connection = GetOpenConnection();
            var sql = "SELECT * FROM Users WHERE UserId = @UserId";
            return connection.QueryFirstOrDefault<UserModel>(sql, new { UserId = userId });
        }

        public void InsertUserDetails(UserModel user)
        {
            using var connection = GetOpenConnection();
            var sql = "INSERT INTO Users (FirstName, LastName, UserName, Email, Password, PhoneNumber) VALUES (@FirstName, @LastName, @UserName, @Email, @Password, @PhoneNumber)";
            connection.Execute(sql, user);
        }

        public override void Update(UserModel user)
        {
            using var connection = GetOpenConnection();
            var sql = "UPDATE Users SET FirstName = @FirstName, LastName = @LastName, UserName = @UserName, Email = @Email, Password = @Password, PhoneNumber = @PhoneNumber WHERE UserId = @UserId";
            connection.Execute(sql, user);
        }

        public UserModel Login(string email, string password)
        {
            using var connection = GetOpenConnection();
            var sql = "SELECT * FROM Users WHERE Email = @Email AND Password = @Password";
            return connection.QueryFirstOrDefault<UserModel>(sql, new { Email = email, Password = password });
        }


        public UserModel GetUserByEmail(string email)
        {
            using var connection = GetOpenConnection();
            var sql = "SELECT * FROM Users WHERE Email = @Email";
            return connection.QueryFirstOrDefault<UserModel>(sql, new { Email = email });
        }

        public override void Insert(UserModel user)
        {
            using var connection = GetOpenConnection();
            var sql = "INSERT INTO Users (FirstName, LastName, UserName, Email, Password, PhoneNumber) VALUES (@FirstName, @LastName, @UserName, @Email, @Password, @PhoneNumber)";
            connection.Execute(sql, user);
        }

    }
}
