namespace WSPay.Net
{
    public class FormSuccessResponse : FormRequest
    {
        public string WSPayOrderId { get; set; }
        public string ApprovalCode { get; set; }
        public string Stan { get; set; }
        public string TokenExp { get; set; }
        public string PaymentType { get; set; }
        public string Signature { get; set; }

        public bool IsTokenResponse => !string.IsNullOrEmpty(Token);
    }
}