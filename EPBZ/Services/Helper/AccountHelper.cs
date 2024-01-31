using DataAccess.Abstraction;
using Microsoft.Extensions.Options;
using Model;
using Services.Abstraction;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Services.Helper
{
    public class AccountHelper: IAccountHelper
    {
        private readonly IAccountRepository _accountRepository;
        private readonly AppSettings _appSettings;

        public AccountHelper(IAccountRepository accountRepository, IOptions<AppSettings> appSettings)
        {
            _accountRepository = accountRepository;
            _appSettings = appSettings.Value;
        }

        public bool CheckExistsEmail(string email)
        {
            var user = _accountRepository.GetUserByEmail(email);
            if (user == null)
            {
                return false;
            }
            return true;
        }

        public void Register(UserModel userModel)
        {
            userModel.Password = HashPassword.CreateHash(userModel.Password);
            _accountRepository.InsertUserDetails(userModel);
        }

        public UserModel Signin(LoginRequestModel loginModel)
        {
            loginModel.Password = HashPassword.CreateHash(loginModel.Password);
            var loginUser = _accountRepository.Login(loginModel.Email, loginModel.Password);
            if (loginUser == null)
            {
                return null;
            }

            return loginUser;
        }

        public string GenerateToken(UserModel validatedUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_appSettings.JWTTokenGenKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.PrimarySid, Convert.ToString(validatedUser.UserId.ToString())),
                    new Claim(ClaimTypes.Name, validatedUser.FirstName.ToString()),
                    new Claim(ClaimTypes.Email, validatedUser.Email.ToString()),
                    new Claim(ClaimTypes.MobilePhone, validatedUser.PhoneNumber.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public IEnumerable<UserModel> GetAllUser()
        {
            return _accountRepository.GetAll();
        }

        public void DeleteUser(int userId)
        {
            _accountRepository.Delete(userId);
        }

    }
}
