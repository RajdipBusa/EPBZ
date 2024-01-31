namespace Web.ApiHelper
{
    public static class CookieHelper
    {
        public static (string,string) GetLoginToken(HttpRequest request)
        {
            return (request.Cookies["IsLogin"], request.Cookies["LoginToken"]);
        }

        public static string GetToken(HttpRequest request)
        {
            return request.Cookies["LoginToken"];
        }
    }
}
