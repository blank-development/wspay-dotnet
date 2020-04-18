namespace WSPay.Net.Test
{
    using System;

    public class TestTimeProvider: ITimeProvider
    {
        public DateTime Get()
        {
            return new DateTime(2020, 04, 01, 15, 20, 30);
        }
    }
}