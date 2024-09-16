<code> # Sending Logs, Traces, and Metrics to OpenTelemetry Collector
Introduction
This README provides a step-by-step guide on how to configure an ASP.NET Core application to send logs, traces, and metrics to an OpenTelemetry Collector. The main goal is to help you instrument your application using OpenTelemetry and understand how to export telemetry data to a collector within an AKS (Azure Kubernetes Service) cluster.

Note: The OpenTelemetry Collector is accessible only within the AKS cluster. It is not accessible from the Developer Cloud Desktop. You can obtain the collector's address from the service's IP address within the cluster.

Table of Contents
Prerequisites
Setting Up OpenTelemetry in ASP.NET Core
Configure Logging
Add Metrics and Tracing
Configure the OpenTelemetry Collector Endpoint
Implementing Tracing and Metrics in the Controller
Accessing the OpenTelemetry Collector
Example Repository
Code Explanation
Conclusion
Prerequisites
ASP.NET Core Application: An existing ASP.NET Core project.
OpenTelemetry NuGet Packages:
OpenTelemetry
OpenTelemetry.Extensions.Hosting
OpenTelemetry.Exporter.OpenTelemetryProtocol
OpenTelemetry.Instrumentation.AspNetCore
OpenTelemetry.Instrumentation.Http
OpenTelemetry.Instrumentation.Runtime
OpenTelemetry.Instrumentation.Process
AKS Cluster: Access to an AKS cluster where the OpenTelemetry Collector is deployed.
Knowledge of C# and .NET Core.
Setting Up OpenTelemetry in ASP.NET Core
Configure Logging
In your Program.cs, set up logging to be exported via OpenTelemetry:

csharp
Copy code
builder.Logging.AddOpenTelemetry(logging =>
{
    logging.IncludeFormattedMessage = true;
    logging.IncludeScopes = true;
});
Add Metrics and Tracing
Add OpenTelemetry services to collect metrics and traces:

csharp
Copy code
var otel = builder.Services.AddOpenTelemetry();

// Configure Metrics
otel.WithMetrics(metrics =>
{
    metrics.AddAspNetCoreInstrumentation();

    // Register custom Meters
    metrics.AddMeter(nameof(YourNamespace.Controllers.YourController));

    // Add built-in meters (if using .NET 8 or higher)
    metrics.AddMeter("Microsoft.AspNetCore.Hosting");
    metrics.AddMeter("Microsoft.AspNetCore.Server.Kestrel");
});

// Configure Tracing
otel.WithTracing(tracing =>
{
    tracing.AddAspNetCoreInstrumentation();
    tracing.AddHttpClientInstrumentation();

    // Register custom ActivitySources
    tracing.AddSource(nameof(YourNamespace.Controllers.YourController));
});
Configure the OpenTelemetry Collector Endpoint
Set the endpoint for the OpenTelemetry Collector. This can be configured via environment variables or app settings:

csharp
Copy code
var OtlpEndpoint = builder.Configuration["OTEL_EXPORTER_OTLP_ENDPOINT"];
if (OtlpEndpoint != null)
{
    otel.UseOtlpExporter(options =>
    {
        options.Endpoint = new Uri(OtlpEndpoint);
        // Additional exporter options can be set here
    });
}
In your appsettings.json:

json
Copy code
{
  "OTEL_EXPORTER_OTLP_ENDPOINT": "http://<collector-service-ip>:4317",
  "OTEL_SERVICE_NAME": "YourServiceName",
  "AllowedHosts": "*"
}
Replace <collector-service-ip> with the IP address of your OpenTelemetry Collector service in the AKS cluster.

Implementing Tracing and Metrics in the Controller
In your controller, set up an ActivitySource and Meter, and instrument your actions:

csharp
Copy code
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class YourController : ControllerBase
    {
        // Define ActivitySource and Meter
        private static readonly ActivitySource ActivitySource = new ActivitySource(nameof(YourController));
        private static readonly Meter Meter = new Meter(nameof(YourController));

        // Define a custom counter metric
        private readonly Counter<long> _requestCounter;

        private readonly ILogger<YourController> _logger;

        public YourController(ILogger<YourController> logger)
        {
            _logger = logger;
            _requestCounter = Meter.CreateCounter<long>("your_action_requests", description: "Number of requests for your action");
        }

        [HttpGet]
        public IActionResult Get()
        {
            // Increment the custom counter metric
            _requestCounter.Add(1);

            // Start a new activity for tracing
            using (var activity = ActivitySource.StartActivity("GetYourData"))
            {
                _logger.LogInformation("Processing Get request");

                // Your action logic here

                _logger.LogInformation("Get request processed successfully");

                return Ok(/* your data */);
            }
        }
    }
}
Accessing the OpenTelemetry Collector
Since the OpenTelemetry Collector is accessible only within the AKS cluster, you need to obtain its service IP address:

Use kubectl to get the service details:

bash
Copy code
kubectl get services
Locate the OpenTelemetry Collector service and note its ClusterIP.

Use this IP address in your OTEL_EXPORTER_OTLP_ENDPOINT setting:

json
Copy code
"OTEL_EXPORTER_OTLP_ENDPOINT": "http://<collector-service-ip>:4317"
Note: Ensure your application is deployed within the same AKS cluster or has network access to the cluster.

Example Repository
You can find a working example of this setup in the following repository:

Example Repository Link

Please replace the above link with the actual repository link.

Code Explanation
Program.cs
Logging Configuration: Sets up logging to export logs via OpenTelemetry, including formatted messages and scopes.

OpenTelemetry Services: Adds OpenTelemetry services for metrics and tracing, registering custom meters and activity sources.

OTLP Exporter Configuration: Configures the application to export telemetry data to the OpenTelemetry Collector using the OTLP exporter.

Dependency Injection: Adds controllers and Swagger for API documentation.

Controller
ActivitySource and Meter: Creates static instances of ActivitySource and Meter to instrument tracing and metrics within the controller.

Custom Counter Metric: Defines a Counter<long> metric to count the number of times the action method is called.

Tracing in Action Method: Starts a new activity using the ActivitySource to trace the execution of the action method.

Logging: Uses ILogger to log information and errors, which are also exported via OpenTelemetry.

appsettings.json
OTLP Endpoint Configuration: Sets the OTEL_EXPORTER_OTLP_ENDPOINT to the OpenTelemetry Collector's service IP address within the AKS cluster.

Service Name: Defines the OTEL_SERVICE_NAME for identifying the service in telemetry data.

Conclusion
By following this guide, you should be able to instrument your ASP.NET Core application with OpenTelemetry and export logs, traces, and metrics to an OpenTelemetry Collector within an AKS cluster. This setup allows you to monitor and observe your application's behavior in a distributed environment effectively.

For any questions or further assistance, please refer to the OpenTelemetry .NET documentation or reach out to your DevOps team.

Document prepared on [Date]. </code>