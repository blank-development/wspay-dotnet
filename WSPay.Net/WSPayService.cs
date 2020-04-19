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
            return wsPayClient.RequestAsync<ProcessPaymentRequest, ProcessPaymentResponse>(request, Services.ProcessPayment);
        }
        
        public ProcessPaymentResponse ProcessPayment(string shoppingCartId, double price, string token, string tokenNumber)
        {
            var request = requestFactory.CreateProcessPaymentRequest(shoppingCartId, price, token, tokenNumber);
            return wsPayClient.Request<ProcessPaymentRequest, ProcessPaymentResponse>(request, Services.ProcessPayment);
        }
        
        public Task<ChangeTransactionStatusResponse> CompleteTransactionAsync(Shop shop, string wsPayOrderId, string stan,
            string approvalCode, double price)
        {
            var requestData = requestFactory.CreateChangeTransactionStatusRequest(shop, wsPayOrderId, stan, approvalCode, price);
            return wsPayClient.RequestAsync<ChangeTransactionStatusRequest, ChangeTransactionStatusResponse>(requestData, Services.Completion);
        }

        public ChangeTransactionStatusResponse CompleteTransaction(Shop shop, string wsPayOrderId, string stan, string approvalCode,
            double price)
        {
            var requestData = requestFactory.CreateChangeTransactionStatusRequest(shop, wsPayOrderId, stan, approvalCode, price);
            return wsPayClient.Request<ChangeTransactionStatusRequest, ChangeTransactionStatusResponse>(requestData, Services.Completion);
        }

        public Task<ChangeTransactionStatusResponse> RefundTransactionAsync(Shop shop, string wsPayOrderId, string stan,
            string approvalCode, double price)
        {
            var requestData = requestFactory.CreateChangeTransactionStatusRequest(shop, wsPayOrderId, stan, approvalCode, price);
            return wsPayClient.RequestAsync<ChangeTransactionStatusRequest, ChangeTransactionStatusResponse>(requestData, Services.Refund);
        }

        public ChangeTransactionStatusResponse RefundTransaction(Shop shop, string wsPayOrderId, string stan, string approvalCode,
            double price)
        {
            var requestData = requestFactory.CreateChangeTransactionStatusRequest(shop, wsPayOrderId, stan, approvalCode, price);
            return wsPayClient.Request<ChangeTransactionStatusRequest, ChangeTransactionStatusResponse>(requestData, Services.Refund);
        }

        public Task<ChangeTransactionStatusResponse> VoidTransactionAsync(Shop shop, string wsPayOrderId, string stan,
            string approvalCode, double price)
        {
            var requestData = requestFactory.CreateChangeTransactionStatusRequest(shop, wsPayOrderId, stan, approvalCode, price);
            return wsPayClient.RequestAsync<ChangeTransactionStatusRequest, ChangeTransactionStatusResponse>(requestData, Services.Void);
        }

        public ChangeTransactionStatusResponse VoidTransaction(Shop shop, string wsPayOrderId, string stan, string approvalCode,
            double price)
        {
            var requestData = requestFactory.CreateChangeTransactionStatusRequest(shop, wsPayOrderId, stan, approvalCode, price);
            return wsPayClient.Request<ChangeTransactionStatusRequest, ChangeTransactionStatusResponse>(requestData, Services.Void);
        }

        public Task<StatusCheckResponse> CheckStatusAsync(Shop shop, string shoppingCartId)
        {
            var request = requestFactory.CreateStatusCheckRequest(shop, shoppingCartId);
            return wsPayClient.RequestAsync<StatusCheckRequest, StatusCheckResponse>(request, Services.StatusCheck);
        }

        public StatusCheckResponse CheckStatus(Shop shop, string shoppingCartId)
        {
            var request = requestFactory.CreateStatusCheckRequest(shop, shoppingCartId);
            return wsPayClient.Request<StatusCheckRequest, StatusCheckResponse>(request, Services.StatusCheck);
        }
    }
}