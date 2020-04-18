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
            return wsPayClient.ProcessPaymentAsync(request);
        }

        public Task<ApiResponse> CompleteTransactionAsync(Shop shop, string wsPayOrderId, string stan,
            string approvalCode, double price)
        {
            var requestData = modelFactory.CreateAutoServiceRequest(shop, wsPayOrderId, stan, approvalCode, price);
            return this.wsPayClient.SendAutoServicesRequestAsync(requestData);
        }

        public Task<ApiResponse> RefundTransactionAsync(Shop shop, string wsPayOrderId, string stan,
            string approvalCode, double price)
        {
            return SendAutoServicesRequestAsync(shop, wsPayOrderId, stan, approvalCode, price, AutoServiceType.Refund);
        }
        
        public Task<ApiResponse> VoidTransactionAsync(Shop shop, string wsPayOrderId, string stan,
            string approvalCode, double price)
        {
            return SendAutoServicesRequestAsync(shop, wsPayOrderId, stan, approvalCode, price, AutoServiceType.Void);
        }

        public async Task<StatusCheckResponse> CheckStatusAsync(Shop shop, string shoppingCartId)
        {
            var requestData = modelFactory.CreateStatusCheckRequest(shop, shoppingCartId);
            var response = await this.wsPayClient.SendAutoServicesRequestAsync(requestData).ConfigureAwait(false);
            return StatusCheckResponse.FromResultContent(response.ResultContent);
        }
        
        private Task<ApiResponse> SendAutoServicesRequestAsync(Shop shop, string wsPayOrderId, string stan,
            string approvalCode, double price, AutoServiceType? type)
        {
            var requestData = modelFactory.CreateAutoServiceRequest(shop, wsPayOrderId, stan, approvalCode, price, type);
            return this.wsPayClient.SendAutoServicesRequestAsync(requestData);
        }
    }
}