namespace WSPay.Net
{
    public class StatusCheckResponse
    {
        public static StatusCheckResponse FromResultContent(string resultContent)
        {
            return new StatusCheckResponse()
            {
                Stan = WSPayHelpers.ExtractField(resultContent, "STAN"),
                WSPayOrderId = WSPayHelpers.ExtractField(resultContent, "WSPayOrderId"),
                ApprovalCode = WSPayHelpers.ExtractField(resultContent, "ApprovalCode"),
                Success = WSPayHelpers.ExtractField(resultContent, "Success"),
                PaymentType = WSPayHelpers.ExtractField(resultContent, "PaymentType"),
                TokenNumber = WSPayHelpers.ExtractField(resultContent, "TokenNumber"),
                Signature = WSPayHelpers.ExtractField(resultContent, "Signature"),
                Finished = WSPayHelpers.ExtractField(resultContent, "Finished"),
                Voided = WSPayHelpers.ExtractField(resultContent, "Voided"),
                Refunded = WSPayHelpers.ExtractField(resultContent, "Refunded"),
            };
        }
 
        public string Approved { get; set; }
        public string WSPayOrderId { get; set; }
        public string Stan { get; set; }
        public string ApprovalCode { get; set; }
        public string Signature { get; set; }

        public string PaymentType { get; set; }
        public string TokenNumber { get; set; }
        public string Success { get; set; }
        public string Finished { get; set; }
        public string Voided { get; set; }
        public string Refunded { get; set; }

        public bool IsFinished => Finished == "1";
        public bool IsVoided => Voided == "1";
        public bool IsRefunded => Refunded == "1";

        public bool IsCompleted => IsFinished || IsVoided || IsRefunded;

        public bool IsTransactionSuccessful => Success == "1" 
            && !string.IsNullOrWhiteSpace(ApprovalCode);

        public bool IsError { get; set; }
    }
}
