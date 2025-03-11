using Common;
using System;

namespace Infrastructure.Services
{
    [AutoRegister(AutoRegisterTypes.Singleton)]
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
