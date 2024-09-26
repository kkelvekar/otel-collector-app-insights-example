# TodoItems API with OpenTelemetry Integration - .NET 8

This is a sample ASP.NET Core Web API built on .NET 8, demonstrating how to send logs, metrics, and custom traces to an OpenTelemetry (OTel) collector using the OpenTelemetry protocol. The application is designed to showcase the key observability practices necessary for real-world application monitoring and debugging.

## Purpose

This example demonstrates:

- Application logs sent in OpenTelemetry format using Serilog, enabling structured logging for detailed insights.
- Native metrics automatically collected by OpenTelemetry, providing performance metrics such as request count and response time.
- Custom tracing for specific API requests and business processes.
- Integration with OpenTelemetry, sending telemetry data (logs, metrics, and traces) to an OpenTelemetry collector.

## Key Features

### 1. Application Logs in OpenTelemetry Format

The application uses Serilog to send structured logs in the OpenTelemetry format via the OTLP protocol. This setup enables the application to log important events, such as incoming requests, error occurrences, and custom business traces. The logs are shipped to the OpenTelemetry collector and can be visualized using monitoring tools like Grafana, Prometheus, or Azure Monitor.

**What logs are captured:**

- Info logs when API endpoints are called.
- Error logs for simulated error conditions in the API.
- Custom logs that accompany the custom traces, providing additional context for debugging and monitoring.

### 2. Native Metrics with OpenTelemetry

OpenTelemetry automatically captures native metrics from ASP.NET Core. This includes:

- Request count: The total number of requests to each API endpoint.
- Request duration: How long each request takes to complete.
- Exception counts: The number of failed requests or errors.

These metrics are automatically forwarded to the OpenTelemetry collector, where they can be used to monitor the overall health and performance of the API.

### 3. Custom Tracing

The application makes use of custom tracing for specific operations, helping monitor business processes and custom logic. Custom traces are captured using `ActivitySource` and include specific spans that help understand where time is spent and where potential issues might arise.

**Custom tracing is particularly useful in scenarios like:**

- Tracing long-running background processes.
- Tracing nested function calls within API operations.

## Controller Methods Explained

The `TodoItemsController` provides three endpoints, each demonstrating a different aspect of observability (logging, tracing, and metrics) using OpenTelemetry.

### 1. `GET /TodoItems`

This endpoint returns a shuffled list of todo items and captures custom traces to track the flow of the request. Each request will create a trace span for the `GetTodos` operation.

- **Custom Tracing**: The `ActivitySource` tracks when the todos are being fetched, how long the process takes, and when the response is sent back.
- **Application Logs**: Info logs are generated to capture successful retrieval of todos and the number of items fetched.

```csharp
[HttpGet(Name = "GetTodos")]
public async Task<IEnumerable<TodoItem>> GetTodos()
{
    using (var activity = activitySource.StartActivity("GetTodos"))
    {
        _logger.LogInformation("Starting to fetch todos");

        var todos = await FetchTodos();

        _logger.LogInformation($"Retrieved {todos.Count} todos successfully.");
        return todos;
    }
}
```
### 2. `GET /TodoItems/process`

This endpoint simulates a data processing operation with a 2-second delay. It is designed to demonstrate how processing time can be captured effectively via native metrics.

- **Metrics**: OpenTelemetry automatically captures the time taken to process the request and logs it as part of the request duration metric. This allows for monitoring of API performance and response times.
- **Application Logs**: Info logs are generated to track the start and completion of the data processing.

```csharp
[HttpGet("process", Name = "ProcessData")]
public async Task<IActionResult> ProcessData()
{
    _logger.LogInformation("Processing some data...");
    // Simulate some processing logic
    await Task.Delay(TimeSpan.FromSeconds(2));
    return Ok("Data processed successfully");
}
```
### 3. `GET /TodoItems/error`

This endpoint simulates an error by throwing an exception, capturing error logs and tracing failures.

- **Error Logs**: Serilog captures the exception and sends it in OpenTelemetry format to the OTel collector, making it visible in any observability tool connected to the collector.
- **Custom Tracing**: OpenTelemetry traces the request, and the trace is marked as failed when the exception is thrown.
- **Metrics**: OpenTelemetry captures the exception count for this endpoint, helping monitor the frequency of failures.

```csharp
[HttpGet("error", Name = "SimulateError")]
public IActionResult SimulateError()
{
    _logger.LogInformation("Simulating an error condition in the TodoItems API");
    try
    {
        throw new InvalidOperationException("This is a simulated exception for demonstration purposes.");
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "An error occurred in SimulateError");
        return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
    }
}
```
## How to Run the Application Locally

### Prerequisites

- .NET 8 SDK installed.
- Access to an OpenTelemetry collector (configured to receive OTLP logs, traces, and metrics).

### Steps to Run

1. Clone the Repository:

    ```bash
    git clone https://github.com/kkelvekar/otel-collector-app-insights-example.git
    cd <project-directory>
    ```
2. Run the Application:

    ```bash
    dotnet run
    ```

3. Access the API: Visit the Swagger UI to explore and test the API endpoints:

    ```bash
    https://localhost:7207;http://localhost:5285
    ```

## Deployment
The application is deployed on a DEV AKS cluster.
### Swagger UI
- **URL**: `"https://api.kkelvekar.com/otel/opentelemetry-otel-api/todoapi/swagger/index.html"`
- This interface provides a user-friendly way to interact with the API, allowing users to test various endpoints directly from their browser.

### OpenTelemetry Collector Configuration

#### Production Environment

- The OTel Collector is configured to be accessible only within the AKS cluster, using a Kubernetes ClusterIP service.
- The collector endpoint in production is: `"http://otel-collector-clusterip-service.otel.svc.cluster.local:4317"`

#### Local Development

- To view telemetry data locally, you need to deploy an OTel Collector on your local environment.
- Detailed instructions and the necessary configuration files can be found in the OTel Collector repository: [OTel Collector Repository](#)

## Observability Insights

### Application Logs in OpenTelemetry Format

- Application logs are structured using Serilog and sent to the OpenTelemetry collector via the OTLP protocol.
- These logs are critical for troubleshooting, providing details on API usage, errors, and business-specific events.

### Native Metrics

- Native ASP.NET Core metrics such as request counts, response durations, and exception counts are captured automatically by OpenTelemetry.
- These metrics help you monitor the health and performance of the application without additional manual instrumentation.

### Custom Tracing

- Custom tracing is implemented using `ActivitySource` to capture detailed spans around specific API calls and business processes.
- This allows you to monitor the execution flow of requests, identify bottlenecks, and debug performance issues effectively.

## Conclusion

This .NET 8 project demonstrates how to integrate OpenTelemetry into an ASP.NET Core API, achieving full observability through logs, metrics, and traces. It showcases the benefits of sending structured logs in OpenTelemetry format, capturing native metrics automatically, and creating custom spans for critical business logic.

This documentation can serve as a guide for developers looking to implement similar observability strategies in their own applications.
