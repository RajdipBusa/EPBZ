using Model;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Web.ApiHelper
{
    public class WebApiHelper : IWebApiHelper
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public WebApiHelper(IConfiguration configuration, IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpClient = httpClientFactory.CreateClient();
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResponseModel<List<Application>>> GetAllApplication()
        {
            // Make a GET request
            HttpRequestMessage request = new(HttpMethod.Get, _configuration.GetValue<string>("ApiUrl") + ApiUrls.GetAllApplication);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", CookieHelper.GetToken(_httpContextAccessor.HttpContext.Request));

            HttpResponseMessage response = await _httpClient.SendAsync(request);

            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var data = JsonConvert.DeserializeObject<ResponseModel<List<Application>>>(content);
                return data;
            }
            else
            {
                return new()
                {
                    IsSuccess = false,
                    Code = (int)response.StatusCode,
                    Message = await response.Content.ReadAsStringAsync()
                };
            }

        }

        public async Task<ResponseModel<bool>> DeleteApplication(int id)
        {

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Delete, _configuration.GetValue<string>("ApiUrl") + ApiUrls.DeleteApplication + $"?id={id}");
            request.Headers.Add("accept", "text/plain");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", CookieHelper.GetToken(_httpContextAccessor.HttpContext.Request));
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                ResponseModel<bool>? data = JsonConvert.DeserializeObject<ResponseModel<bool>>(await response.Content.ReadAsStringAsync());
                return data;
            }
            else
            {
                return new()
                {
                    IsSuccess = false,
                    Code = (int)response.StatusCode,
                    Message = await response.Content.ReadAsStringAsync()
                };
            }
        }

        public async Task<ResponseModel<bool>> AddApplication(AddApplicationModel applicationModel)
        {

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, _configuration.GetValue<string>("ApiUrl") + ApiUrls.AddApplication);
            request.Headers.Add("accept", "text/plain");
            request.Content = new StringContent(JsonConvert.SerializeObject(applicationModel), null, "application/json");
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {

                ResponseModel<bool>? data = JsonConvert.DeserializeObject<ResponseModel<bool>>(await response.Content.ReadAsStringAsync());
                return data;
            }
            else
            {
                return new()
                {
                    IsSuccess = false,
                    Code = (int)response.StatusCode,
                    Message = await response.Content.ReadAsStringAsync()
                };
            }

        }

        public async Task<(bool, string)> Login(LoginRequestModel loginRequest)
        {

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, _configuration.GetValue<string>("ApiUrl") + ApiUrls.UserLogin);
            request.Headers.Add("accept", "text/plain");
            request.Content = new StringContent(JsonConvert.SerializeObject(loginRequest), null, "application/json");
            var response = await client.SendAsync(request);

            string responseData = await response.Content.ReadAsStringAsync();
            return (response.IsSuccessStatusCode, responseData);
        }

        public async Task<ResponseModel<AddApplicationModel>> GetApplicationData(int id)
        {
            // Make a GET request
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, _configuration.GetValue<string>("ApiUrl") + ApiUrls.GetApplicationData + $"?id={id}");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", CookieHelper.GetToken(_httpContextAccessor.HttpContext.Request));

            // Perform the GET request using HttpClient
            HttpResponseMessage response = await _httpClient.SendAsync(request);

            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var data = JsonConvert.DeserializeObject<ResponseModel<AddApplicationModel>>(content);
                return data;
            }
            else
            {
                return new()
                {
                    IsSuccess = false,
                    Code = (int)response.StatusCode,
                    Message = await response.Content.ReadAsStringAsync()
                };
            }
        }
    }
}
