namespace WSPay.Net
{
    using System.Threading.Tasks;

    public class WSPayService: IWSPayService
    {
        private readonly IRequestFactory requestFactory;
        private readonly IWSPayClient wsPayClient;

        public WSPayService(): this(new RequestFactory(), new WSPayApiClient())
        {
        }
        
        public WSPayService(IRequestFactory requestFactory, IWSPayClient wsPayClient)
        {
            this.requestFactory = requestFactory;
            this.wsPayClient = wsPayClient;
        }

        public Task<ProcessPaymentResponse> ProcessPaymentAsync(string shoppingCartId, double price, string token, string tokenNumber)
        {
            var request = requestFactory.CreateProcessPaymentRequest(shoppingCartId, price, token, tokenNumber);
            return wsPayClient.ProcessPaymentAsync(request);
        }

        public Task<ChangeTransactionStatusResponse> CompleteTransactionAsync(Shop shop, string wsPayOrderId, string stan,
            string approvalCode, double price)
        {
            var requestData = requestFactory.CreateChangeTransactionStatusRequest(shop, wsPayOrderId, stan, approvalCode, price);
            return wsPayClient.CompleteTransactionAsync(requestData);
        }

        public Task<ChangeTransactionStatusResponse> RefundTransactionAsync(Shop shop, string wsPayOrderId, string stan,
            string approvalCode, double price)
        {
            var requestData = requestFactory.CreateChangeTransactionStatusRequest(shop, wsPayOrderId, stan, approvalCode, price);
            return wsPayClient.RefundTransactionAsync(requestData);
        }
        
        public Task<ChangeTransactionStatusResponse> VoidTransactionAsync(Shop shop, string wsPayOrderId, string stan,
            string approvalCode, double price)
        {
            var requestData = requestFactory.CreateChangeTransactionStatusRequest(shop, wsPayOrderId, stan, approvalCode, price);
            return wsPayClient.VoidTransactionAsync(requestData);
        }

        public Task<StatusCheckResponse> CheckStatusAsync(Shop shop, string shoppingCartId)
        {
            var request = requestFactory.CreateStatusCheckRequest(shop, shoppingCartId);
            return wsPayClient.CheckStatusAsync(request);
        }
    }
}