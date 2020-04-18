namespace WSPay.Net
{
    using System.Threading.Tasks;

    public class WSPayService: IWSPayService
    {
        private readonly IModelFactory modelFactory;
        private readonly IWSPayClient wsPayClient;
        
        public WSPayService(IModelFactory modelFactory, IWSPayClient wsPayClient)
        {
            this.modelFactory = modelFactory;
            this.wsPayClient = wsPayClient;
        }

        public Task<ProcessPaymentResponse> ProcessPaymentAsync(string shoppingCartId, double price, string token, string tokenNumber)
        {
            var request = modelFactory.CreateProcessPaymentRequest(shoppingCartId, price, token, tokenNumber);
            return this.wsPayClient.ProcessPaymentAsync(request);
        }

        public Task<CompleteTransactionResponse> CompleteTransactionAsync(Shop shop, string wsPayOrderId, string stan,
            string approvalCode, double price)
        {
            var requestData = modelFactory.CreateCompleteTransactionRequest(shop, wsPayOrderId, stan, approvalCode, price);
            return this.wsPayClient.CompleteTransactionAsync(requestData);
        }

        public Task<CompleteTransactionResponse> RefundTransactionAsync(Shop shop, string wsPayOrderId, string stan,
            string approvalCode, double price)
        {
            var requestData = modelFactory.CreateCompleteTransactionRequest(shop, wsPayOrderId, stan, approvalCode, price);
            return this.wsPayClient.RefundTransactionAsync(requestData);
        }
        
        public Task<CompleteTransactionResponse> VoidTransactionAsync(Shop shop, string wsPayOrderId, string stan,
            string approvalCode, double price)
        {
            var requestData = modelFactory.CreateCompleteTransactionRequest(shop, wsPayOrderId, stan, approvalCode, price);
            return this.wsPayClient.VoidTransactionAsync(requestData);
        }

        public Task<StatusCheckResponse> CheckStatusAsync(Shop shop, string shoppingCartId)
        {
            var request = modelFactory.CreateStatusCheckRequest(shop, shoppingCartId);
            return this.wsPayClient.CheckStatusAsync(request);
        }
    }
}