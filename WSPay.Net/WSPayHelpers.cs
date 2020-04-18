namespace WSPay.Net
{
    using System;
    using System.Globalization;
    using System.Text.RegularExpressions;
    
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
        
        public static bool IsActionSuccessful(string response)
        {
            var regex = new Regex("<input type=\"hidden\" name=\"ActionSuccess\" value=\"(\\d)\">");
            var matchedGroups = regex.Match(response);

            var actionSuccessValue = matchedGroups.Groups[1].Value;

            return actionSuccessValue == "1";
        }

        public static string GetErrorMessageFromResponseString(string response)
        {
            var regex = new Regex("<input type=\"hidden\" name=\"ErrorMessage\" value=\"([a-zA-Z0-9_ ]*)\">");
            var matchedGroups = regex.Match(response);

            return matchedGroups.Groups[1].Value;
        }
    }
}