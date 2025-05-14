using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Mcp;
using Microsoft.Extensions.Logging;

namespace Closet.MCP.Server.Tools;

public class WorkroomTool(
    IHttpClientFactory httpClientFactory,
    ILogger<WorkroomTool> logger)
{
    private const string UrlPath = "api/rooms";
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
    private readonly ILogger<WorkroomTool> _logger = logger;

    [Function(nameof(SearchWorkrooms))]
    public async Task<string> SearchWorkrooms(
        [McpToolTrigger("SearchWorkrooms", "Get the workroom by keyword")] ToolInvocationContext context,
        [McpToolProperty] string keyword = "",
        [McpToolProperty] int pageNo = 1,
        [McpToolProperty] int pageSize = 40)
    {
        var arguments = context.GetDefaultArguments();
        if (arguments is null)
        {
            _logger.LogWarning("Default arguments are not set.");
            return "MCP Default arguments are not set. Please check the configuration.";
        }
        
        var client = _httpClientFactory.CreateClient(Constants.ClosetHttpClientName);
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {arguments.GroupToken}");
        var parameters = new Dictionary<string, string?>
        {
            { "groupId", arguments.GroupId! },
            { "pageNo", $"{pageNo}" },
            { "pageSize", $"{pageSize}" },
            { "keyword", keyword },
            { "sort", "0" },
            { "isDescending", "true" }
        };
        
        var requestUri = QueryHelpers.AddQueryString(UrlPath, parameters);
        var response = await client.GetAsync(requestUri);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
}