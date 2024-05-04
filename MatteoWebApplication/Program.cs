using MatteoWebApplication.Services;
using System.Net.NetworkInformation;

var builder = WebApplication.CreateBuilder(args);

IHostEnvironment env = builder.Environment;


builder.Configuration.Sources.Clear();

builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true);

if (args is { Length: > 0 })
{
    builder.Configuration.AddCommandLine(args);
}

//Using the default configuration, the EnvironmentVariablesConfigurationProvider loads configuration from environment variable key-value pairs 
//after reading appsettings.json, appsettings.Environment.json, and Secret manager. 
//Therefore, key values read from the environment override values read from appsettings.json, appsettings.Environment.json, and Secret manager.

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<WeatherForecastService>();

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
