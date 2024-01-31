using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using Services.Abstraction;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationHelper _applicationHelper;
        public ApplicationController(IApplicationHelper applicationHelper)
        {
            _applicationHelper = applicationHelper;
        }

        [HttpPost]
        [Route("add")]
        public ActionResult<ResponseModel<bool>> Add([FromBody]AddApplicationModel addApplication)
        {
            ResponseModel<bool> responseModel = new();
            try
            {
                responseModel.Data = _applicationHelper.AddApplication(addApplication);
                responseModel.IsSuccess = true;
                responseModel.Code = StatusCodes.Status201Created;
                return responseModel;
            }
            catch (Exception ex)
            {
                responseModel.IsSuccess = false;
                responseModel.Message = ex.Message;
                responseModel.Code = StatusCodes.Status500InternalServerError;
                return responseModel;
            }
        }

        [HttpGet]
        [Authorize]
        [Route("getall")]
        public ActionResult<ResponseModel<List<Application>>> GetAll()
        {
            ResponseModel<List<Application>> responseModel = new();
            try
            {
                responseModel.Data = _applicationHelper.GetAll().ToList();
                responseModel.IsSuccess = true;
                responseModel.Code = StatusCodes.Status201Created;
                return responseModel;
            }
            catch (Exception ex)
            {
                responseModel.IsSuccess = false;
                responseModel.Message = ex.Message;
                responseModel.Code = StatusCodes.Status500InternalServerError;
                return responseModel;
            }
        }

        [HttpDelete]
        [Authorize] 
        [Route("delete")]
        public ActionResult<ResponseModel<bool>> Delete(int id)
        {
            ResponseModel<bool> responseModel = new();
            try
            {
                responseModel.Data = _applicationHelper.Delete(id);
                responseModel.IsSuccess = true;
                responseModel.Code = StatusCodes.Status200OK;
                return responseModel;
            }
            catch (Exception ex)
            {
                responseModel.IsSuccess = false;
                responseModel.Message = ex.Message;
                responseModel.Code = StatusCodes.Status500InternalServerError;
                return responseModel;
            }
        }

        [HttpGet]
        [Authorize]
        [Route("getapplicationdata")]
        public ActionResult<ResponseModel<AddApplicationModel>> GetApplicationData(int id)
        {
            ResponseModel<AddApplicationModel> responseModel = new();
            try
            {
                responseModel.Data = _applicationHelper.GetApplicationData(id);
                responseModel.IsSuccess = true;
                responseModel.Code = StatusCodes.Status200OK;
                return responseModel;
            }
            catch (Exception ex)
            {
                responseModel.IsSuccess = false;
                responseModel.Message = ex.Message;
                responseModel.Code = StatusCodes.Status500InternalServerError;
                return responseModel;
            }
        }
    }
}
