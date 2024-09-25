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
amdclient,, Failed,0.0%,176,12.0%,30 days ago,25/09/2024
analyticsclient,, Failed,27.8%,119,0.0%,8 days ago,25/09/2024
application-logging,, Not analyzed, Not available, Not available, Not available, Not analyzed,25/09/2024
authorization-service,, Failed,18.0%,30,4.6%,1 month ago,25/09/2024
benchmarks-service,, Passed,27.4%,54,4.4%,1 month ago,25/09/2024
benchmarks-sync-job,, Failed,1.5%,26,19.4%,1 month ago,25/09/2024
cash-flow-manager,, Passed,81.9%,162,4.6%,2 months ago,25/09/2024
cedar.common,, Not analyzed, Not available, Not available, Not available, Not analyzed,25/09/2024
containerisation,, Failed,60.6%,3,0.0%,1 month ago,25/09/2024
currency-management-service,, Passed,0.0%,5,0.0%,15 days ago,25/09/2024
data-transactions-sync-job,, Passed,86.5%,16,0.0%,1 month ago,25/09/2024
data-transactions-service,, Failed,50.1%,122,50.4%,5 hours ago,25/09/2024
data-projected-cash-transactions-sync-job,, Failed,72.9%,15,0.0%,1 day ago,25/09/2024
data-pricing-security-prices-sync-job,, Failed,45.9%,9,0.0%,21 days ago,25/09/2024
data-portfolio-strategies-sync-job,, Failed,26.6%,9,0.0%,1 month ago,25/09/2024
data-portfolio-gim-scd-mapping-sync-job,, Failed,37.6%,241,25.2%,2 months ago,25/09/2024
data-portfolio-contacts-sync-job,, Failed,34.5%,5,0.0%,2 months ago,25/09/2024
data-portfolio-bank-accounts-sync-job,, Passed,43.9%,9,0.0%,1 month ago,25/09/2024
data-issuers-sync-job,, Not analyzed, Not available, Not available, Not available, Not analyzed,25/09/2024
data-housekeeping-job,, Passed,0.0%,62,0.0%,1 day ago,25/09/2024
data-analytics-sync-job,, Failed,16.5%,198,10.5%,12 hours ago,25/09/2024
data-analytics-service,, Failed,15.8%,29,12.9%,28 days ago,25/09/2024
data-portfolio-sync-job,, Failed,37.6%,10,0.0%,1 month ago,25/09/2024
data-portfolio-service,, Failed,37.6%,241,25.2%,22 days ago,25/09/2024
dotnet,, Failed,60.6%,3,0.0%,1 month ago,25/09/2024
identity,, Failed,53.4%,95,0.0%,1 month ago,25/09/2024
kafkapublishingapi,, Failed,12.1%,27,0.0%,1 month ago,25/09/2024
kubernetes-admission-hooks,, Not analyzed, Not available, Not available, Not available, Not analyzed,25/09/2024
longview-position-notification-service,, Failed,37.1%,290,4.9%,7 days ago,25/09/2024
longview-sync-service,, Passed,0.0%, Not available,0.0%,2 months ago,25/09/2024
longview-sdk-invoker-function,, Failed,50.9%,8,0.0%,1 month ago,25/09/2024
longview-position-notification-service,, Failed,37.1%,290,4.9%,7 days ago,25/09/2024
longview-order-notification,, Failed,48.4%,54,5.3%,7 days ago,25/09/2024
messaging,, Passed,71.8%,36,0.0%,10 days ago,25/09/2024
optimizer-service,, Failed,63.3%,124,0.0%,2 months ago,25/09/2024
optimizer-engine,, Not analyzed, Not available, Not available, Not available, Not analyzed,25/09/2024
optimizer-engine,, Passed,76.7%,12,0.0%,2 months ago,25/09/2024
optimizer-data-consumer,, Failed,31.8%,35,0.0%,28 days ago,25/09/2024
optimizer-data-services,, Not analyzed,76.7%,12,0.0%, Not analyzed,25/09/2024
order-update-service,, Failed,48.7%,51,0.0%,3 months ago,25/09/2024
order-flow-manager,, Failed,20.4%,120,8.9%,1 month ago,25/09/2024
portal-service,, Failed,86.6%,16,2.6%,5 hours ago,25/09/2024
portfolio-calculator-sync-job,, Failed,0.0%,357,0.0%,1 day ago,25/09/2024
portfolio-calculator-service,, Failed,74.6%,124,0.0%,1 month ago,25/09/2024
portfolio-calculator-libraries,, Passed,34.1%,8,28.6%,1 month ago,25/09/2024
portfolio-construction-service,, Failed,14.8%,357,30.9%,1 day ago,25/09/2024
portfolio-construction-service,, Failed,14.8%,134,0.0%,9 days ago,25/09/2024
portfolio-viewer,, Failed,32.3%,216,30.9%,1 month ago,25/09/2024
position-keeping-service,, Failed,74.7%,216,0.0%,1 month ago,25/09/2024
position-keeping-service,, Failed,74.7%,155,0.0%,5 days ago,25/09/2024
pretrade-compliance-service,, Failed,22.2%,120,0.0%,1 day ago,25/09/2024
pricing-sync-job,, Failed,57.4%,29,5.2%,23 days ago,25/09/2024
securities-sync-job,, Failed,0.5%,3,0.0%,1 month ago,25/09/2024
security-service,, Passed,36.9%,280,0.8%,22 days ago,25/09/2024
strategies-service,, Failed,73.3%,338,0.7%,2 hours ago,25/09/2024
testing,, Failed,93.9%,134,0.0%,21 days ago,25/09/2024
transactions-service,, Failed,75.6%,27,0.0%,1 month ago,25/09/2024
longview-position-notification-service,, Failed,37.1%,290,4.9%,7 days ago,25/09/2024
position-keeping-service,, Passed,74.7%,216,0.0%,1 month ago,25/09/2024