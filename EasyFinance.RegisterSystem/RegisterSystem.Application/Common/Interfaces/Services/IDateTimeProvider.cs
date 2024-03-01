namespace RegisterSystem.Application.Common.Interfaces.Services
{
    public interface IDateTimeProvider
    {
        DateTime GetNow();

        DateTime GetUtcNow();
    }
}
