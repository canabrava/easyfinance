namespace RegisterSystem.Application.Services.UserServices.Commands.Register
{
    public record RegisterResult(
        Guid id,
        string name,
        string email,
        DateTime registerDate);
}
