namespace WSPay.Net
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    
    public class WSPayApiClient: IWSPayClient
    {
        private readonly HttpClient httpClient;

        public WSPayApiClient(HttpClient httpClient)
        {
            this.httpClient = httpClient ?? BuildDefaultHttpClient();
            this.httpClient.BaseAddress = WSPayConfiguration.BaseUrl;
            
            // ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }

        public async Task<ApiResponse> SendAutoServicesRequestAsync(IDictionary<string, string> postData)
        {
            var formUrlEncodedData = new FormUrlEncodedContent(postData);
            var result = await httpClient.PostAsync("WSPayAutoServices.aspx", formUrlEncodedData).ConfigureAwait(false);;
            var resultContent = await result.Content.ReadAsStringAsync();

            var isActionSuccessful = WSPayHelpers.IsActionSuccessful(resultContent);

            return isActionSuccessful
                ? ApiResponse.CreateSuccess(resultContent)
                : ApiResponse.CreateError(WSPayHelpers.GetErrorMessageFromResponseString(resultContent));
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

        private HttpClient BuildDefaultHttpClient()
        {
            return new HttpClient();
        }

        private StringContent BuildRequestContent<T>(T request)
        {
            return new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        }
    }
}