
using System.Net;

namespace AlphaHemClient.Model.DTO
{
    // Author: Conny

    // Use this if you want to return an object along with the response
    public class Response<T> : Response
    {
        public T? Data { get; set; }
    }

    // Use this if you want to return a response without an object
    public class Response
    {
        public string? Message { get; set; }
        public List<string> Errors { get; set; } = [];
        public HttpStatusCode StatusCode { get; set; }
    }
}
