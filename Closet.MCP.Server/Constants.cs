namespace Closet.MCP.Server;

public static class Constants
{
    static Constants()
    {
        EnvironmentVariables = new EnvironmentVariables();
    }
    
    public static EnvironmentVariables EnvironmentVariables { get; }
    
    public const string WorkroomApiPath = "api/rooms";
    
    public const string GroupIdArgumentName = "groupId";
    public const string GroupTokenArgumentName = "groupToken";
    public const string ClosetHttpClientName = "CLO-SET-HttpClient";
}

public class EnvironmentVariables
{
    public string ClosetBaseAddressEnvironmentVariableName => "CLOSET_BASE_ADDRESS";    
}