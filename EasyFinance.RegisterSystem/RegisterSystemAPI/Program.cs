using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using RegisterSystem.Api.Errors;
using RegisterSystem.Application;
using RegisterSystem.Infrastructure;
using RegisterSystem.Infrastructure.Persistence;
using Serilog;
using Serilog.Sinks.Network;

var builder = WebApplication.CreateBuilder(args);

var logstashConfig = builder.Configuration.GetSection("Logstash");
var logstashHost = logstashConfig["Host"];
var logstashPort = int.Parse(logstashConfig["Port"]!);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.TCPSink(logstashHost, port: logstashPort)
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddControllers();

// Configura��o do Swagger/OpenAPI com coment�rios XML para documenta��o
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Define o caminho e o arquivo que ser�o usados para gerar a documenta��o XML.
    var xmlFilename = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});


var absolutePath = Path.GetFullPath(builder.Configuration.GetConnectionString("DefaultConnection"));

builder.Services.AddDbContext<RegisterSystemDbContext>(options =>
    options.UseSqlite($"Data Source={absolutePath}"));

builder.Services.AddSingleton<ProblemDetailsFactory, ApiProblemDetailsFactory>();

builder.Services.AddApplication();
builder.Services.AddInfrastructure();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseExceptionHandler("/error");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
