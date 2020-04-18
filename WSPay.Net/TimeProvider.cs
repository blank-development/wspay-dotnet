using System;

namespace WSPay.Net
{
    public class TimeProvider : ITimeProvider
    {
        public DateTime Get()
        {
            return DateTime.UtcNow;
        }
    }
}