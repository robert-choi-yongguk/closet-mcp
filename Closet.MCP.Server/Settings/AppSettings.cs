using Closet.MCP.Core.Settings;

namespace Closet.MCP.Server.Settings;

public sealed class AppSettings : IAppSettings
{
    public string GetClosetBaseAddress()
        => Environment.GetEnvironmentVariable(Constants.EnvironmentVariables.ClosetBaseAddressEnvironmentVariableName)
           ?? throw new InvalidOperationException(
               $"The environment variable {Constants.EnvironmentVariables.ClosetBaseAddressEnvironmentVariableName} is not set.");
}