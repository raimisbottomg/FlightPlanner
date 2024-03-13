using System.Text.Json.Serialization;

namespace FlightPlanner.Modules
{
    public class Airport
    {
        [JsonIgnore]
        public int Id {  get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        [JsonPropertyName("airport")]
        public string AirportCode { get; set; }
    }
}