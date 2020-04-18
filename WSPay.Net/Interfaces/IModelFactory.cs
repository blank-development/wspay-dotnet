namespace WSPay.Net
{
    public interface IModelFactory
    {
        ProcessPaymentRequest CreateProcessPaymentRequest(string shoppingCartId, double price, string token,
            string tokenNumber);

        CompleteTransactionRequest CreateCompleteTransactionRequest(Shop shop, string wsPayOrderId, string stan,
            string approvalCode, double price);
        
        FormRequest CreateFormRequest(string shoppingCartId, double price, Customer customer,
            PaymentType paymentType, IReturnUrlProvider returnUrlProvider);

        StatusCheckRequest CreateStatusCheckRequest(Shop shop, string shoppingCartId);
    }
}