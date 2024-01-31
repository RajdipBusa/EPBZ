using Microsoft.AspNetCore.Mvc;
using Model;
using Web.ApiHelper;

namespace Web.Controllers
{
    public class ApplicationFormController : Controller
    {
        private readonly IWebApiHelper _webApiHelper;
        public ApplicationFormController(IWebApiHelper webApiHelper)
        {
            _webApiHelper = webApiHelper;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestModel loginRequest)
        {
            if (ModelState.IsValid)
            {
                (bool, string) data = await _webApiHelper.Login(loginRequest);
                if (data.Item1)
                {
                    CookieOptions options = new()
                    {
                        Expires = DateTime.Now.AddDays(7)
                    };
                    Response.Cookies.Append("IsLogin", "true", options);
                    Response.Cookies.Append("LoginToken", data.Item2, options);

                    return RedirectToAction("Index");
                }
                ViewBag.LoginError = data.Item2;

            }
            return View(loginRequest);
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete("IsLogin");
            Response.Cookies.Delete("LoginToken");
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> EditApplication(int appId)
        {
            if (!CheckLogin())
            {
                return RedirectToAction("Login");
            }

            ResponseModel<AddApplicationModel> data = await _webApiHelper.GetApplicationData(appId);
            AddApplicationModel addApplication = new();
            if (data.IsSuccess)
            {
                addApplication = data.Data;
            }
            return View("Index", addApplication);
        }

        [HttpPost]
        public async Task<IActionResult> SaveApplication(AddApplicationModel applicationModel)
        {
            try
            {
                if (applicationModel == null || applicationModel.educations == null)
                {
                    return BadRequest("Invalid application data!");
                }
                ResponseModel<bool> data = await _webApiHelper.AddApplication(applicationModel);
                if (data.IsSuccess)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(data.Message);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        public async Task<IActionResult> ViewApplication()
        {
            if(!CheckLogin())
            {
                return RedirectToAction("Login");
            }
            ResponseModel<List<Application>> data = await _webApiHelper.GetAllApplication();

            return View(data.Data);
        }

        public async Task<IActionResult> DeleteApplication(int appId)
        {
            if (!CheckLogin())
            {
                return RedirectToAction("Login");
            }

            try
            {
                ResponseModel<bool> data = await _webApiHelper.DeleteApplication(appId);
                if (data.IsSuccess)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(data.Message);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private bool CheckLogin()
        {
            (string, string) loginData = CookieHelper.GetLoginToken(Request);
            if (loginData.Item1 == "true" && !string.IsNullOrWhiteSpace(loginData.Item2))
            {
                return true;
            }
            return false;   
        }


    }
}
