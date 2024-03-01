using RegisterSystem.Application.Common.Interfaces.Services;

namespace RegisterSystem.Infrastructure.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime GetNow() =>  DateTime.Now;
        
        public DateTime GetUtcNow() => DateTime.UtcNow;
    }
}
