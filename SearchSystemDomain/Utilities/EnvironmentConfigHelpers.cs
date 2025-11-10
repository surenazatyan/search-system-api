using Microsoft.Extensions.Configuration;
using SearchSystemDomain.Constants;

namespace SearchSystemDomain.Utilities;

public static class EnvironmentConfigHelpers
{
    /// <summary>
    /// 1. Try environment variable
    /// 2. Try appsettings.json if env var is missing or empty
    /// 3. Fallback to constant
    /// </summary>
    public static string GetApiUrlWithPort(IConfigurationRoot _configuration)
    {
        var apiUrlWithPort = Environment.GetEnvironmentVariable(EnvironmentConfigConstants.ApiUrlWithPortEnvVar);
        if (string.IsNullOrWhiteSpace(apiUrlWithPort))
        {
            apiUrlWithPort = _configuration[$"MySettings:{EnvironmentConfigConstants.ApiUrlWithPortEnvVar}"];
            if (string.IsNullOrWhiteSpace(apiUrlWithPort))
            {
                apiUrlWithPort = EnvironmentConfigConstants.FallbackApiUrlWithPort;
            }
        }

        return apiUrlWithPort;
    }
}
