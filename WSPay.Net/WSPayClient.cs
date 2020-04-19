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

        public async Task<TRes> RequestAsync<TReq, TRes>(TReq request, string service) 
            where TReq: class
            where TRes: class
        {
            var requestContent = BuildRequestContent(request);
            var response = await httpClient.PostAsync($"api/service/{service}", requestContent).ConfigureAwait(false);
            var result = await ProcessResponse<TRes>(response);
            return result;
        }
        
        public TRes Request<TReq, TRes>(TReq request, string service)
            where TReq: class
            where TRes: class
        {
            return RequestAsync<TReq, TRes>(request, service).WaitTask();
        }
        
        private StringContent BuildRequestContent<T>(T request)
        {
            return new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
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
        
        private static HttpClient BuildDefaultHttpClient()
        {
            return new HttpClient();
        }
    }
}