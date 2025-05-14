namespace Closet.MCP.Server.Models;

public sealed record DefaultArguments(
    string? GroupId,
    string? GroupToken)
{
    public bool IsValid() => string.IsNullOrEmpty(GroupId) is false && string.IsNullOrEmpty(GroupToken) is false;
}