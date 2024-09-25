using Microsoft.AspNetCore.Mvc;

namespace otel_collector_api_connect.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };


        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            _logger.LogInformation("Getting weather forecast");
            var data =  Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
            _logger.LogError("Weather forecast retrieved");
            return data;
        }
    }
}

Project Name, POD Team, Quality Gate Status, Code Coverage, Code Smells, Duplication, Last Analyze Date, Data Capture Date
analyticsclient,, Failed,27.80%,119,0.00%,8 days ago,25-09-2024
authorization-service,, Failed,18.00%,30,4.60%,1 month ago,25-09-2024
benchmarks-service,, Passed,27.40%,54,4.40%,1 month ago,25-09-2024
cash-flow-manager,, Passed,81.90%,162,4.60%,2 months ago,25-09-2024
containerisation,, Failed,60.60%,3,0.00%,1 month ago,25-09-2024
currency-management-service,, Passed,0.00%,5,0.00%,15 days ago,25-09-2024
data-analytics-service,, Failed,15.80%,29,12.90%,28 days ago,25-09-2024
data-analytics-sync-job,, Failed,16.50%,198,10.50%,9 hours ago,25-09-2024
data-portfolio-bank-accounts-sync-job,, Passed,43.90%,9,0.00%,1 month ago,25-09-2024
data-portfolio-contacts-sync-job,, Failed,34.50%,5,0.00%,2 months ago,25-09-2024
data-portfolio-gim-scd-mapping-sync-job,, Failed,39.40%,241,25.20%,2 months ago,25-09-2024
data-portfolio-service,, Failed,37.60%,241,25.20%,22 days ago,25-09-2024
data-portfolio-strategies-sync-job,, Failed,26.60%,9,0.00%,2 months ago,25-09-2024
data-portfolio-sync-job,, Failed,77.80%,10,0.00%,1 month ago,25-09-2024
data-pricing-security-prices-sync-job,, Failed,45.90%,9,0.00%,21 days ago,25-09-2024
data-projected-cash-transactions-sync-job,, Failed,72.90%,15,0.00%,1 day ago,25-09-2024
data-transactions-service,, Failed,50.10%,122,50.40%,2 hours ago,25-09-2024
data-transactions-sync-job,, Passed,86.50%,16,0.00%,1 month ago,25-09-2024
dotnet,, Failed,60.60%,3,0.00%,1 month ago,25-09-2024
identity,, Failed,0.00%,3,0.00%,1 month ago,25-09-2024
kafkapublishingapi,, Failed,12.10%,27,0.00%,1 month ago,25-09-2024
longview-order-notification,, Failed,48.40%,54,5.30%,7 days ago,25-09-2024
longview-position-notification-service,, Failed,37.10%,290,4.90%,5 days ago,25-09-2024
longview-sdk-invoker-function,, Failed,50.90%,291,0.00%,1 month ago,25-09-2024
longview-sync-service,, Passed,37.60%,291,4.90%,1 month ago,25-09-2024
order-flow-manager,, Failed,63.30%,124,0.00%,2 months ago,25-09-2024
portal-service,, Failed,48.70%,51,0.00%,3 days ago,25-09-2024
portfolio-calculator-libraries,, Passed,86.60%,16,2.60%,2 hours ago,25-09-2024
portfolio-calculator-service,, Failed,34.10%,124,28.60%,1 month ago,25-09-2024
portfolio-calculator-sync-job,, Failed,74.60%,35,0.00%,13 days ago,25-09-2024
portfolio-construction-service,, Failed,0.00%,124,1.40%,1 month ago,25-09-2024
position-keeping-service,, Passed,75.70%,155,0.00%,4 days ago,25-09-2024
pretrade-compliance-service,, Failed,22.20%,120,5.20%,1 day ago,25-09-2024
pricing-sync-job,, Failed,57.40%,29,0.00%,23 days ago,25-09-2024
securities-sync-job,, Failed,0.50%,3,0.00%,1 month ago,25-09-2024
security-service,, Failed,36.90%,280,0.80%,22 days ago,25-09-2024
testing,, Passed,0.00%,0,0.00%,22 days ago,25-09-2024
transactions-service,, Failed,75.60%,27,0.00%,1 month ago,25-09-2024
ubs/am/managing-investments/traditional/invcedar/common-event-driven-service,, Failed,22.20%,290,5.20%,1 month ago,25-09-2024
amdClient,, Failed,0.00%,176,12.00%,30 days ago,25-09-2024
benchmark-sync-job,, Failed,1.50%,26,19.40%,1 month ago,25-09-2024
data-housekeeping-job,, Passed,0.00%,62,0.00%,1 day ago,25-09-2024
messaging,, Passed,71.80%,36,0.00%,16 days ago,25-09-2024
optimizer-data-consumer,, Failed,31.80%,35,0.00%,28 days ago,25-09-2024




