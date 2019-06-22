using Newtonsoft.Json;

namespace AirQuality.FunctionApp.Models
{
    public class Message
    {
        [JsonProperty("sender")] public string Sender { get; set; }
        [JsonProperty("text")] public string Text { get; set; }
    }
}