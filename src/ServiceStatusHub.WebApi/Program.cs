using FluentValidation;
using HealthChecks.UI.Client;
using HealthChecks.UI.Configuration;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using MongoDB.Driver;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using Serilog.Sinks.Tcp;
using ServiceStatusHub.Application.Commands.Incident;
using ServiceStatusHub.Application.Mappings;
using ServiceStatusHub.Application.Validators.Incident;
using ServiceStatusHub.Infrastructure.DependencyInjection;
using ServiceStatusHub.Infrastructure.IoC;
using ServiceStatusHub.WebApi.HealthCheck;
using ServiceStatusHub.WebApi.Middleware;


var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://192.168.0.27:9200"))
    {
        AutoRegisterTemplate = true,
        IndexFormat = "servicehub-api-logs-" + DateTime.UtcNow.ToString("yyyy.MM.dd")
    })
    .CreateLogger();

builder.Host.UseSerilog(); // substitui o log padrão

// Add AutoMapper services
builder.Services.AddAutoMapper( cfg =>
{
    cfg.AddMaps(typeof(IncidentProfile).Assembly);
});

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registra o MediatR e os handlers
// Configurações do MediatR e FluentValidation
var applicationAssembly = typeof(CreateIncidentCommand).Assembly;
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));
builder.Services.AddValidatorsFromAssembly(applicationAssembly);

// Add Infrastructure services
builder.Services.AddCachedDependencies();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddInfrastructureConfigurationMapping();

//Configuring Health Ckeck
builder.Services.ConfigureHealthChecks(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Registra o middleware de logging de requisições e respostas
app.UseMiddleware<RequestResponseLoggingMiddleware>();

// Registra o middleware de tratamento de exceções 
app.UseMiddleware<ExceptionMiddleware>();

//HealthCheck Middleware
app.MapHealthChecks("/api/health", new HealthCheckOptions()
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.UseHealthChecksUI(delegate (Options options)
{
    options.UIPath = "/healthcheck-ui";
    // Caso queira adicionar um CSS customizado, descomente a linha abaixo
    //options.AddCustomStylesheet("./HealthCheck/Custom.css");

});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


// REMARK: Required for functional and integration tests to work.
namespace ServiceStatusHub.WebApi
{
    public partial class Program;
}