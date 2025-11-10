using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SearchSystemApplication.Services;
using SearchSystemInfrastructure;
using SearchSystemInfrastructure.DatabaseRepositories;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

var logPath = Path.Combine("logs", $"log-.txt");

// Configure Serilog for colorful console and daily rolling file sink
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console(theme: AnsiConsoleTheme.Code)
    .WriteTo.File(
        logPath,
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 7,
        restrictedToMinimumLevel: LogEventLevel.Information,
        outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"
    )
    .CreateLogger();

try
{
    Log.Information("Starting SearchSystemApi");

    var builder = WebApplication.CreateBuilder(args);

    // Use Serilog for logging
    builder.Host.UseSerilog();

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "SearchSystem API", Version = "v1" });

        // Enable Swashbuckle attributes such as [SwaggerOperation], [SwaggerResponse], etc.
        c.EnableAnnotations();

        // Optional: include XML comments so <summary>/<remarks> appear in Swagger UI
        var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        if (File.Exists(xmlPath))
        {
            c.IncludeXmlComments(xmlPath);
        }
    });

    builder.Services.AddDbContext<SearchSystemDbContext>(options => options.UseInMemoryDatabase("SearchSystemDb"));

    builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
    builder.Services.AddScoped<IServiceInfoService, ServiceInfoService>();

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "SearchSystem API v1");
            c.RoutePrefix = string.Empty;
        });
    }

    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<SearchSystemDbContext>();
        DbSeeder.Seed(db);

        Log.Information("Database seeded.");
    }

    app.UseHttpsRedirection();
    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "SearchSystemApi start-up failed");
}
finally
{
    Log.CloseAndFlush();
}
