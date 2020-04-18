using System.Collections.Generic;

namespace WSPay.Net
{
    public interface IModelFactory
    {
        ProcessPaymentRequest CreateProcessPaymentRequest(string shoppingCartId, double price, string token,
            string tokenNumber);

        FormRequest CreateFormRequest(string shoppingCartId, double price, Customer customer,
            PaymentType paymentType, IReturnUrlProvider returnUrlProvider);

        IDictionary<string, string> CreateAutoServiceRequest(Shop shop, string wsPayOrderId, string stan,
            string approvalCode, double totalPrice, AutoServiceType? serviceType = null);

        StatusCheckRequest CreateStatusCheckRequest(Shop shop, string shoppingCartId);
    }
}