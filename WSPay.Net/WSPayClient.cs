namespace WSPay.Net
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    
    public class WSPayApiClient: IWSPayClient
    {
        private readonly Settings settings;
        private readonly HttpClient httpClient;

        public WSPayApiClient(Settings settings, HttpClient httpClient)
        {
            this.settings = settings;
            this.httpClient = httpClient;

            // ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }

        public async Task<ApiResponse> SendAutoServicesRequestAsync(IDictionary<string, string> postData)
        {
            var formUrlEncodedData = new FormUrlEncodedContent(postData);
            var result = await httpClient.PostAsync(this.settings.AutoServicesUrl, formUrlEncodedData).ConfigureAwait(false);;
            var resultContent = await result.Content.ReadAsStringAsync();

            var isActionSuccessful = WSPayHelpers.IsActionSuccessful(resultContent);

            return isActionSuccessful
                ? ApiResponse.CreateSuccess(resultContent)
                : ApiResponse.CreateError(WSPayHelpers.GetErrorMessageFromResponseString(resultContent));
        }

        public async Task<ProcessPaymentResponse> ProcessPaymentAsync(ProcessPaymentRequest request)
        {
            var requestContent =  new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var result = await httpClient.PostAsync(this.settings.ProcessPaymentJsonApiUrl, requestContent).ConfigureAwait(false);

            var resultContent = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<ProcessPaymentResponse>(resultContent);
        }
    }
}