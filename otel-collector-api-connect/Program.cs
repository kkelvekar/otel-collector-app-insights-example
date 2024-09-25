using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using System.Diagnostics.Metrics;
using System.Diagnostics;
using Serilog;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;
var Environment = builder.Environment;

// Custom metrics for the application
var greeterMeter = new Meter("OTel.Example", "1.0.0");

// Custom ActivitySource for the application
var greeterActivitySource = new ActivitySource("OTel.Example");

var otelEndpoint = $"{Configuration.GetValue<string>("Otlp:Endpoint")}/v1/logs";
var serviceName = Configuration.GetValue<string>("Otlp:ServiceName");

// Add services to the container.

builder.Host.UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
            .ReadFrom.Configuration(hostingContext.Configuration)
            .WriteTo.OpenTelemetry(options =>
            {
                options.Endpoint = otelEndpoint;
                options.Protocol = Serilog.Sinks.OpenTelemetry.OtlpProtocol.Grpc;
                options.ResourceAttributes = new Dictionary<string, object>
                {
                    ["service.name"] = serviceName
                };
            }));

// Setup logging to be exported via OpenTelemetry
//builder.Logging.AddOpenTelemetry(logging =>
//{
//    logging.IncludeFormattedMessage = true;
//    logging.IncludeScopes = true;
//});

var otel = builder.Services.AddOpenTelemetry();

// Add Metrics for ASP.NET Core and our custom metrics and export via OTLP
otel.WithMetrics(metrics =>
{
    // Metrics provider from OpenTelemetry
    metrics.AddAspNetCoreInstrumentation();
    //Our custom metrics
    metrics.AddMeter(greeterMeter.Name);
    // Metrics provides by ASP.NET Core in .NET 8
    metrics.AddMeter("Microsoft.AspNetCore.Hosting");
    metrics.AddMeter("Microsoft.AspNetCore.Server.Kestrel");
});

// Add Tracing for ASP.NET Core and our custom ActivitySource and export via OTLP
otel.WithTracing(tracing =>
{
    tracing.AddAspNetCoreInstrumentation();
    tracing.AddHttpClientInstrumentation();
    tracing.AddSource(greeterActivitySource.Name);
});

otel.UseOtlpExporter(OpenTelemetry.Exporter.OtlpExportProtocol.Grpc, new Uri(otelEndpoint));

// Export OpenTelemetry data via OTLP, using env vars for the configuration
//var OtlpEndpoint = builder.Configuration["OTEL_EXPORTER_OTLP_ENDPOINT"];
//if (OtlpEndpoint != null)
//{
//    otel.UseOtlpExporter();
//}



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var serverUrl = Configuration["SwaggerOptions:ServerUrl"];
    var serverDescription = Configuration["SwaggerOptions:Description"];
    c.AddServer(new OpenApiServer() { Url = serverUrl, Description = serverDescription });
});


var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
