namespace RegisterSystem.Application.Services.UserServices.Queries.Login
{
    public record LoginResult(
       Guid id,
       string name,
       string email,
       string token);
}
