namespace WSPay.Net
{
    using System.Threading.Tasks;
    using System;
    using System.Globalization;
    
    internal static class WSPayHelpers
    {
        public static string FormatPrice(double price)
        {
            return (Math.Round(price, 2) * 100).ToString(CultureInfo.InvariantCulture);
        }

        public static string FormatAmountForRegularShopForm(double amount)
        {
            var rounded = (decimal)Math.Round(amount, 2);
            var isRounded = (rounded * 100) % 100 == 0;
            return isRounded 
                ? $"{decimal.Truncate(rounded)},00"
                : rounded.ToString(CultureInfo.InvariantCulture)
                    .Replace('.', ',');
        }

        public static TResult WaitTask<TResult>(this Task<TResult> task)
        {
            return task.ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
        }
    }
}