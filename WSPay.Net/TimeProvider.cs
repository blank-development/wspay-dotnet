namespace WSPay.Net
{
    using System;
    
    public class TimeProvider : ITimeProvider
    {
        public DateTime Get()
        {
            return DateTime.UtcNow;
        }
    }
}