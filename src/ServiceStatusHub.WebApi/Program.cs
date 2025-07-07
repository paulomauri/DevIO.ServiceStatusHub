using FluentValidation;
using ServiceStatusHub.Application.Commands.Incident;
using ServiceStatusHub.Application.Validators.Incident;

var builder = WebApplication.CreateBuilder(args);

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


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
