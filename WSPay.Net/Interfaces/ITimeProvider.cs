using System;

namespace WSPay.Net
{
    public interface ITimeProvider
    {
        DateTime Get();
    }
}