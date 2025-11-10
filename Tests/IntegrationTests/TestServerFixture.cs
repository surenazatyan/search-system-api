using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using System.Diagnostics;

public class TestServerFixture : IDisposable
{
    private Process? _apiProcess;
    private IConfigurationRoot _configuration;

    public IConfigurationRoot Configuration { get => _configuration; set => _configuration = value; }

    static TestServerFixture()
    {
        var logPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "logs", $"log-.txt");
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
        Log.Information("Starting Tests logging");
    }

    public TestServerFixture()
    {
        // Ensure no lingering processes from previous runs - KILL
        try
        {
            using var ps = new System.Diagnostics.Process();
            ps.StartInfo.FileName = "powershell";
            ps.StartInfo.Arguments = "-Command \"Get-Process -Name SearchSystemApi* -ErrorAction SilentlyContinue | Stop-Process -Force\"";
            ps.StartInfo.UseShellExecute = false;
            ps.StartInfo.CreateNoWindow = true;
            ps.Start();
            ps.WaitForExit();
        }
        catch
        {
            // Ignore errors if processes do not exist
        }

        Configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory()) // or AppContext.BaseDirectory
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                   .Build();




        // Start SearchSystemApi project
        try
        {

            var apiProjectPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "SearchSystemApi", "SearchSystemApi.csproj"));
            var apiStartInfo = new ProcessStartInfo
            {
                FileName = "dotnet",
                Arguments = $"run --project \"{apiProjectPath}\" --launch-profile http",
                //RedirectStandardOutput = true,
                //RedirectStandardError = true,
                UseShellExecute = true, // KEY: Makes it independent of this process
                CreateNoWindow = false // Shows window (useful for debugging)
            };
            _apiProcess = Process.Start(apiStartInfo);

            Thread.Sleep(500);
        }
        catch (Exception ex2)
        {
            string? stdOut = null, stdErr = null;
            if (_apiProcess != null)
            {
                stdOut = _apiProcess.StandardOutput.ReadToEndAsync().Result;
                stdErr = _apiProcess.StandardError.ReadToEndAsync().Result;

                if (!_apiProcess.HasExited)
                {
                    _apiProcess.Kill();
                    _apiProcess.Dispose();
                }
            }
            throw new Exception($"API server did not start/respond.\nSTDOUT:\n{stdOut}\nSTDERR:\n{stdErr}", ex2);
        }
    }

    public void Dispose()
    {
        if (_apiProcess != null && !_apiProcess.HasExited)
        {
            _apiProcess.Kill();
            _apiProcess.Dispose();
        }

        Log.Information("Disposing TestServerFixture");
        Log.CloseAndFlush();
    }
}
