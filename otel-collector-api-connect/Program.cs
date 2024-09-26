using OpenTelemetry;
using OpenTelemetry.Trace;
using Serilog;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;
var Environment = builder.Environment;

var otelEndpoint = $"{Configuration.GetValue<string>("Otlp:Endpoint")}/v1/logs";
var serviceName = Configuration.GetValue<string>("Otlp:ServiceName");

// Configure Serilog for logging
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

// Configure OpenTelemetry
var otel = builder.Services.AddOpenTelemetry();

// Add Metrics and Tracing for ASP.NET Core
otel.WithTracing(tracing =>
{
    tracing.AddAspNetCoreInstrumentation();
    tracing.AddHttpClientInstrumentation();
});

// Use OTLP exporter
otel.UseOtlpExporter(OpenTelemetry.Exporter.OtlpExportProtocol.Grpc, new Uri(otelEndpoint));

builder.Services.AddControllers();
builder.Services.AddHttpClient();
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
