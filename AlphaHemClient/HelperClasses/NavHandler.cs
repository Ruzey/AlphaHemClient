using System.Net;

namespace AlphaHemClient.HelperClasses
{
    public static class NavHandler
    {
        public static string? Handler(HttpStatusCode statusCode)
        {
            if ((int)statusCode <= 299 && (int)statusCode >= 200)
                return null;
            switch (statusCode)
            {
                case HttpStatusCode.BadRequest:
                    return "/400-BadRequest";
                case HttpStatusCode.Unauthorized:
                    return "/401-Unauthorized";
                case HttpStatusCode.Forbidden:
                    return "/403-Forbidden";
                case HttpStatusCode.NotFound:
                    return "/404-NotFound";
                case HttpStatusCode.InternalServerError:
                    return "/500-InternalServerError";
                case HttpStatusCode.ServiceUnavailable:
                    return "/503-ServiceUnavailable";
                default:
                    return "/";
            }
        }
    }
}
