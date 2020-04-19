namespace WSPay.Net
{
    using System.Threading.Tasks;
    
    public interface IWSPayClient
    {
        Task<ProcessPaymentResponse> ProcessPaymentAsync(ProcessPaymentRequest request);
        Task<StatusCheckResponse> CheckStatusAsync(StatusCheckRequest request);
        Task<ChangeTransactionStatusResponse> VoidTransactionAsync(ChangeTransactionStatusRequest request);
        Task<ChangeTransactionStatusResponse> RefundTransactionAsync(ChangeTransactionStatusRequest request);
        Task<ChangeTransactionStatusResponse> CompleteTransactionAsync(ChangeTransactionStatusRequest request);
    }
}