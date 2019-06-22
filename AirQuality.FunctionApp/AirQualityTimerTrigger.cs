using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AirQuality.FunctionApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AirQuality.FunctionApp
{
    public static class AirQualityTimerTrigger
    {
        private static readonly HttpClient HttpClient = new HttpClient();

        /// <summary>
        ///     This function will be called by our client to retrieve the SignalR Service endpoint.
        /// </summary>
        /// <param name="req"></param>
        /// <param name="connectionInfo"></param>
        /// <returns></returns>
        [FunctionName("negotiate")]
        public static SignalRConnectionInfo GetSignalRInfo(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")]
            HttpRequest req,
            [SignalRConnectionInfo(HubName = "airQualityHub")]
            SignalRConnectionInfo connectionInfo)
        {
            return connectionInfo;
        }


        [FunctionName("AirQualityTimerTrigger")]
        public static Task Run([TimerTrigger("0 */1 * * * *")] TimerInfo myTimer,
            [SignalR(HubName = "airQualityHub")] IAsyncCollector<SignalRMessage> signalRMessages, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            var response = HttpClient
                .GetAsync("https://datosabiertos.malaga.eu/recursos/ambiente/calidadaire/calidadaire.json").Result;
            response.EnsureSuccessStatusCode();
            var result = response.Content.ReadAsStringAsync().Result;

            var model = JsonConvert.DeserializeObject<AirQualityApiResponse>(result);

            var unhealthyLowAreas = model.features.Where(x => x.properties.iucalevel_APP_global == "unhealthy-low");
            var unhealthyLowAreasCount = unhealthyLowAreas.Count();

            var unhealthyAreas = model.features.Where(x => x.properties.iucalevel_APP_global == "unhealthy");
            var unhealthyAreasCount = unhealthyAreas.Count();

            var unhealthyHighAreas = model.features.Where(x => x.properties.iucalevel_APP_global == "unhealthy-high");
            var unhealthyHighAreasCount = unhealthyHighAreas.Count();

            var maxO3Level = model.features.Max(x => x.properties.o3);
            var coordsMaxO3Level = model.features.OrderByDescending(x => x.properties.o3).First().geometry.coordinates;

            var maxNo2Level = model.features.Max(x => x.properties.no2);
            var coordsMaxNo2Level =
                model.features.OrderByDescending(x => x.properties.no2).First().geometry.coordinates;

            var maxCoLevel = model.features.Max(x => x.properties.co);
            var coordsMaxCoLevel = model.features.OrderByDescending(x => x.properties.co).First().geometry.coordinates;

            var fullText = $"Max O3: {maxO3Level} @ {JsonConvert.SerializeObject(coordsMaxO3Level)} \n" +
                           $"/ Max NO2: {maxNo2Level} @ {JsonConvert.SerializeObject(coordsMaxNo2Level)} \n" +
                           $"/ Max CO: {maxCoLevel} @ {JsonConvert.SerializeObject(coordsMaxCoLevel)}";

            Console.WriteLine(fullText);

            return signalRMessages.AddAsync(
                new SignalRMessage
                {
                    Target = "newMessage",
                    Arguments = new[]
                    {
                        new Message
                        {
                            Text = fullText
                        }
                    }
                });
        }
    }
}