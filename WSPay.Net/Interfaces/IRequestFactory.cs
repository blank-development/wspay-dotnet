namespace WSPay.Net
{
    public interface IRequestFactory
    {
        ProcessPaymentRequest CreateProcessPaymentRequest(string shoppingCartId, double price, string token,
            string tokenNumber);

        ChangeTransactionStatusRequest CreateChangeTransactionStatusRequest(Shop shop, string wsPayOrderId, string stan,
            string approvalCode, double price);
        
        StatusCheckRequest CreateStatusCheckRequest(Shop shop, string shoppingCartId);
        
        FormRequest CreateFormRequest(string shoppingCartId, double price, Customer customer,
            PaymentType paymentType, IReturnUrlProvider returnUrlProvider);
    }
}