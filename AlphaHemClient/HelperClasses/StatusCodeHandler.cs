using System.Net;

namespace AlphaHemClient.HelperClasses
{
    public static class StatusCodeHandler
    {
        public static string? Handler(HttpStatusCode statusCode)
        {
           switch (statusCode)
           {
                case HttpStatusCode.OK:
                    return null;
                case HttpStatusCode.NotFound:
                    return "/404-NotFound";
           }
        }
    }
}
