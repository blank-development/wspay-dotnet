namespace WSPay.Net
{
    using System.Threading.Tasks;
    
    public interface IWSPayClient
    {
        Task<ProcessPaymentResponse> ProcessPaymentAsync(ProcessPaymentRequest request);
        Task<StatusCheckResponse> CheckStatusAsync(StatusCheckRequest request);
        Task<CompleteTransactionResponse> VoidTransactionAsync(CompleteTransactionRequest request);
        Task<CompleteTransactionResponse> RefundTransactionAsync(CompleteTransactionRequest request);
        Task<CompleteTransactionResponse> CompleteTransactionAsync(CompleteTransactionRequest request);

    }
}