using AutomationRadar.Data.Context;
using Microsoft.EntityFrameworkCore;
using AutomationRadar.Business.Interfaces;
using AutomationRadar.Business.Repositories;
using System.Text.Json.Serialization;
using AutomationRadar.API.Mappings;
using Oracle.EntityFrameworkCore; 

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingProfile));

// DbContext com compatibilidade Oracle configurada
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("OracleConnection");
    options.UseOracle(connectionString, oracleOptions =>
    {
        // Gera SQL compatível com Oracle 19c (evita TRUE/FALSE)
        oracleOptions.UseOracleSQLCompatibility(OracleSQLCompatibility.DatabaseVersion19);
    });
});

// Repositórios
builder.Services.AddScoped<IOccupationRepository, OccupationRepository>();
builder.Services.AddScoped<IAutomationRiskRepository, AutomationRiskRepository>();
builder.Services.AddScoped<ICareerTransitionRepository, CareerTransitionRepository>();

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
