using System.Text.Json.Serialization;

namespace AlphaHemClient.Model
{
    public class UserData
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("token")]
        public string Token { get; set; }

        [JsonPropertyName("userId")]
        public string UserId { get; set; }
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }
        [JsonPropertyName("lastName")]
        public string LastName { get; set; }
    }
}
