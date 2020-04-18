namespace WSPay.Net
{
    using Newtonsoft.Json;

    [JsonObject]
    public class CompleteTransactionResponse
    {
        [JsonProperty("WSPayOrderID")]
        public string WSPayOrderId { get; set; }
        
        [JsonProperty("ShopID")]
        public string ShopId { get; set; }
        
        [JsonProperty("ApprovalCode")]
        public string ApprovalCode { get; set; }
        
        [JsonProperty("STAN")]
        public string Stan { get; set; }
        
        [JsonProperty("ErrorMessage")]
        public string ErrorMessage { get; set; }
        
        [JsonProperty("Signature")]
        public string Signature { get; set; }
        
        [JsonProperty("ActionSuccess")]
        public string ActionSuccess { get; set; }
    }
}