using Closet.MCP.Core.Settings;
using Closet.MCP.Server;
using Closet.MCP.Server.Settings;
using Closet.MCP.Server.Tools;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

builder.EnableMcpToolMetadata();
builder.Services.AddSingleton<IAppSettings, AppSettings>();

builder.Services.AddHttpClient(Constants.ClosetHttpClientName, (provider, client) =>
{
    var appSettings = provider.GetRequiredService<IAppSettings>();
    client.BaseAddress = new Uri(new Uri(appSettings.GetClosetBaseAddress()), Constants.WorkroomApiPath);
    client.DefaultRequestHeaders.UserAgent.ParseAdd("Closet-MCP-Server");
    
    // TODO : Timeout, Retry, Circuit Breaker, etc.
    // client.Timeout = TimeSpan.FromSeconds(10);
});

builder.Build().Run();