namespace WSPay.Net
{
    using Newtonsoft.Json;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    
    public class WSPayApiClient: IWSPayClient
    {
        private readonly HttpClient httpClient;

        public WSPayApiClient() : this(BuildDefaultHttpClient())
        {
        }
        
        public WSPayApiClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            this.httpClient.BaseAddress = WSPayConfiguration.BaseApiUrl;
        }

        public Task<ProcessPaymentResponse> ProcessPaymentAsync(ProcessPaymentRequest request)
        {
            return RequestAsync<ProcessPaymentRequest, ProcessPaymentResponse>(request, Services.ProcessPayment);
        }

        public Task<StatusCheckResponse> CheckStatusAsync(StatusCheckRequest request)
        {
            return RequestAsync<StatusCheckRequest, StatusCheckResponse>(request, Services.StatusCheck);
        }
        
        public Task<ChangeTransactionStatusResponse> CompleteTransactionAsync(ChangeTransactionStatusRequest request)
        {
            return RequestAsync<ChangeTransactionStatusRequest, ChangeTransactionStatusResponse>(request, Services.Completion);
        }
        
        public Task<ChangeTransactionStatusResponse> RefundTransactionAsync(ChangeTransactionStatusRequest request)
        {
            return RequestAsync<ChangeTransactionStatusRequest, ChangeTransactionStatusResponse>(request, Services.Refund);
        }
        
        public Task<ChangeTransactionStatusResponse> VoidTransactionAsync(ChangeTransactionStatusRequest request)
        {
            return RequestAsync<ChangeTransactionStatusRequest, ChangeTransactionStatusResponse>(request, Services.Void);
        }

        private static HttpClient BuildDefaultHttpClient()
        {
            return new HttpClient();
        }

        private StringContent BuildRequestContent<T>(T request)
        {
            return new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        }

        private async Task<TRes> RequestAsync<TReq, TRes>(TReq request, string service) 
        {
            var requestContent = BuildRequestContent(request);
            var response = await httpClient.PostAsync($"api/service/{service}", requestContent).ConfigureAwait(false);
            var result = await ProcessResponse<TRes>(response);
            return result;
        }
        
        private async Task<T> ProcessResponse<T>(HttpResponseMessage response)
        {
            var resultContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                throw new WSPayException(response.StatusCode, resultContent);
            }

            return JsonConvert.DeserializeObject<T>(resultContent);
        }
    }
}