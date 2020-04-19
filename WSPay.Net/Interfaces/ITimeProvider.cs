namespace WSPay.Net
{
    using System;

    public interface ITimeProvider
    {
        DateTime Get();
    }
}