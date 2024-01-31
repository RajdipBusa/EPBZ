using Model;

namespace Web.ApiHelper
{
    public interface IWebApiHelper
    {
        Task<ResponseModel<bool>> AddApplication(AddApplicationModel applicationModel);
        Task<ResponseModel<bool>> DeleteApplication(int id);
        Task<ResponseModel<List<Application>>> GetAllApplication();
        Task<ResponseModel<AddApplicationModel>> GetApplicationData(int id);
        Task<(bool, string)> Login(LoginRequestModel loginRequest);
    }
}
