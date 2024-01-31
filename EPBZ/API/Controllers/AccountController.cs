using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Services.Abstraction;

namespace API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountHelper _accountHelper;

        public AccountController(IAccountHelper accountHelper)
        {
            _accountHelper = accountHelper;
        }

        [HttpPost]
        [Route("api/signup")]
        public async Task<IActionResult> Signup(UserModel userModel)
        {
            try
            {
                var isExist = _accountHelper.CheckExistsEmail(userModel.Email);
                if (isExist)
                {
                    return Conflict("Email Already Exists");
                }
                _accountHelper.Register(userModel);
                return Ok("User Created");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error - " + ex.Message);
            }
        }

        [HttpPost]
        [Route("api/signin")]
        public async Task<IActionResult> Signin(LoginRequestModel loginModel)
        {
            try
            {
                var loginUser = _accountHelper.Signin(loginModel);
                if (loginUser == null)
                {
                    return Conflict("Invalid Credential.");
                }
                string token = _accountHelper.GenerateToken(loginUser);
                if (string.IsNullOrEmpty(token))
                    return BadRequest("Error while generating token");

                return Ok(token);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error - " + ex.Message);
            }

        }

    }
}
