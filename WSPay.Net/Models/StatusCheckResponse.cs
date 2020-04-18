namespace WSPay.Net
{
    using Newtonsoft.Json;

    [JsonObject]
    public class StatusCheckResponse
    {
        [JsonProperty("WSPayOrderID")]
        public string WSPayOrderId { get; set; }
        public string Signature { get; set; }
        public string ErrorMessage { get; set; }
        [JsonProperty("STAN")]
        public string Stan { get; set; }
        [JsonProperty("ShopID")]
        public string ShopId { get; set; }
        public double Amount { get; set; }
        public string ActionSuccess { get;set; }
        public string Success { get; set; }
        public string Completed { get; set; }
        public string Voided { get; set; }
        public string Refunded { get; set; }
        public string PaymentPlan { get; set; }
        public string Partner { get; set; }
    }
}
