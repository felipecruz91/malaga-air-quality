using System.Collections.Generic;

namespace AirQuality.FunctionApp.Models
{
    public class AirQualityApiResponse
    {
        public int totalFeatures { get; set; }
        public string type { get; set; }
        public List<Feature> features { get; set; }
    }
}