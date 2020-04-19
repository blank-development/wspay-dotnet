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

        public async Task<ChangeTransactionStatusResponse> CompleteTransactionAsync(ChangeTransactionStatusRequest request)
        {
            var requestContent = BuildRequestContent(request);
            var result = await httpClient.PostAsync("api/services/Completion", requestContent).ConfigureAwait(false);

            var resultContent = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<ChangeTransactionStatusResponse>(resultContent);
        }
        
        public async Task<ChangeTransactionStatusResponse> RefundTransactionAsync(ChangeTransactionStatusRequest request)
        {
            var requestContent = BuildRequestContent(request);
            var result = await httpClient.PostAsync("api/services/Refund", requestContent).ConfigureAwait(false);

            var resultContent = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<ChangeTransactionStatusResponse>(resultContent);
        }
        
        public async Task<ChangeTransactionStatusResponse> VoidTransactionAsync(ChangeTransactionStatusRequest request)
        {
            var requestContent = BuildRequestContent(request);
            var result = await httpClient.PostAsync("api/services/Void", requestContent).ConfigureAwait(false);

            var resultContent = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<ChangeTransactionStatusResponse>(resultContent);
        }
        
        public async Task<ProcessPaymentResponse> ProcessPaymentAsync(ProcessPaymentRequest request)
        {
            var requestContent = BuildRequestContent(request);
            var result = await httpClient.PostAsync("api/services/ProcessPayment", requestContent).ConfigureAwait(false);

            var resultContent = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<ProcessPaymentResponse>(resultContent);
        }

        public async Task<StatusCheckResponse> CheckStatusAsync(StatusCheckRequest request)
        {
            var requestContent = BuildRequestContent(request);
            var result = await httpClient.PostAsync("api/services/StatusCheck", requestContent).ConfigureAwait(false);
            var resultContent = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<StatusCheckResponse>(resultContent);
        }

        private static HttpClient BuildDefaultHttpClient()
        {
            return new HttpClient();
        }

        private StringContent BuildRequestContent<T>(T request)
        {
            return new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        }
    }
}