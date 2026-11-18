using System.Text.Json;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

// builder.Services
//     .AddApplicationInsightsTelemetryWorkerService()
//     .ConfigureFunctionsApplicationInsights();

// FORCE JSON to respect the property names exactly as defined
builder.Services.Configure<JsonSerializerOptions>(options =>
{
    options.PropertyNamingPolicy = null; // This stops "type" from replacing "@odata.type"
});

builder.Build().Run();
