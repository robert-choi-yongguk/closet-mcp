using Closet.MCP.Server.Models;
using Microsoft.Azure.Functions.Worker.Extensions.Mcp;

namespace Closet.MCP.Server;

public static class Extensions
{
    public static DefaultArguments? GetDefaultArguments(this ToolInvocationContext context)
    {
        if (context.Arguments is null
            || context.Arguments.TryGetValue(Constants.GroupIdArgumentName, out var groupId) is false
            || context.Arguments.TryGetValue(Constants.GroupTokenArgumentName, out var groupToken) is false)
        {
            return null;
        }

        var defaultArguments = new DefaultArguments(groupId.ToString(), groupToken.ToString());
        return defaultArguments.IsValid() is false ? null : defaultArguments;
    }
}