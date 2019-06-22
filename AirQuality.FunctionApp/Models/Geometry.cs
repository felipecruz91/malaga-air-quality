using System.Collections.Generic;

namespace AirQuality.FunctionApp.Models
{
    public class Geometry
    {
        public string type { get; set; }
        public List<List<List<double>>> coordinates { get; set; }
    }
}