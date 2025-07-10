using FluentValidation;
using Serilog;
using ServiceStatusHub.Application.Commands.Incident;
using ServiceStatusHub.Application.Mappings;
using ServiceStatusHub.Application.Validators.Incident;
using ServiceStatusHub.Infrastructure.DependencyInjection;
using ServiceStatusHub.WebApi.Middleware;


var builder = WebApplication.CreateBuilder(args);

// Add Logging services
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console(new Serilog.Formatting.Json.JsonFormatter())
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
builder.Services.AddInfrastructure(builder.Configuration);


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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


// REMARK: Required for functional and integration tests to work.
namespace ServiceStatusHub.WebApi
{
    public partial class Program;
}