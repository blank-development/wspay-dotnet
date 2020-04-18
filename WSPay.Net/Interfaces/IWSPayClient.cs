namespace WSPay.Net
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    
    public interface IWSPayClient
    {
        Task<ApiResponse> SendAutoServicesRequestAsync(IDictionary<string, string> postData);
        Task<ProcessPaymentResponse> ProcessPaymentAsync(ProcessPaymentRequest request);
    }
}