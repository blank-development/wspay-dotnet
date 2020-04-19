namespace WSPay.Net
{
    using Newtonsoft.Json;

    [JsonObject]
    public class StatusCheckResponse
    {
        [JsonProperty("WSPayOrderID")]
        public string WSPayOrderId { get; set; }
        [JsonProperty("Signature")]
        public string Signature { get; set; }
        [JsonProperty("ErrorMessage")]
        public string ErrorMessage { get; set; }
        [JsonProperty("STAN")]
        public string Stan { get; set; }
        [JsonProperty("ApprovalCode")]
        public string ApprovalCode { get; set; }
        [JsonProperty("ShopID")]
        public string ShopId { get; set; }
        [JsonProperty("Amount")]
        public string Amount { get; set; }
        [JsonProperty("ActionSuccess")]
        public string ActionSuccess { get;set; }
        [JsonProperty("Success")]
        public string Success { get; set; }
        [JsonProperty("Completed")]
        public string Completed { get; set; }
        [JsonProperty("Voided")]
        public string Voided { get; set; }
        [JsonProperty("Refunded")]
        public string Refunded { get; set; }
        [JsonProperty("PaymentPlan")]
        public string PaymentPlan { get; set; }
        [JsonProperty("Partner")]
        public string Partner { get; set; }
        [JsonProperty("CreditCardName")]
        public string CreditCardName { get; set; }
        [JsonProperty("PaymentType")]
        public string PaymentType { get; set; }
        [JsonProperty("TokenNumber")]
        public string TokenNumber { get; set; }
        public bool IsActionSuccess => ActionSuccess == ActionCodes.Success.ToString("D");
        public bool IsSuccess => Success == ActionCodes.Success.ToString("D");
        public bool IsCompleted => Completed == ActionCodes.Success.ToString("D");
        public bool IsVoided => Voided == ActionCodes.Success.ToString("D");
        public bool IsRefunded => Refunded == ActionCodes.Success.ToString("D");
    }
}
