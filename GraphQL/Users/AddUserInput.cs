namespace Server.GraphQL
{
    public record AddUserInput(
        string? Username,
        string? Password,
        string? Name,
        int Experience);

}