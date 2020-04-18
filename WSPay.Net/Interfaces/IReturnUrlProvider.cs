namespace WSPay.Net
{
    public interface IReturnUrlProvider
    {
        string GetReturnUrl();
        string GetCancelUrl(string shoppingCartId);
        string GetErrorUrl();
    }
}
