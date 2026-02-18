using System.Text.Json.Serialization;

namespace Client.Models
{
    /// <summary>
    /// Modelo para deserializar respuestas de la API.
    /// </summary>
    public class ApiResponse<T>
    {
        [JsonPropertyName("data")]
        public T? Data { get; set; }

        [JsonPropertyName("statusCode")]
        public int StatusCode { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
