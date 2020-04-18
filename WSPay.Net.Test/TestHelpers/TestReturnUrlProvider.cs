namespace WSPay.Net.Test
{
    public class TestUrlProvider: IReturnUrlProvider
    {
        public string GetReturnUrl()
        {
            return "https://dobartek.hr/success";
        }

        public string GetCancelUrl(string shoppingCartId)
        {
            return "https://dobartek.hr/cancel";
        }

        public string GetErrorUrl()
        {
            return "https://dobartek.hr/error";
        }
    }
}