namespace WSPay.Net
{
    using Newtonsoft.Json;

    [JsonObject]
    public class CompleteTransactionRequest
    {
        [JsonProperty("WSPayOrderID")]
        public string WSPayOrderId { get; set; }
        
        [JsonProperty("ShopID")]
        public string ShopId { get; set; }
        
        [JsonProperty("ApprovalCode")]
        public string ApprovalCode { get; set; }
        
        [JsonProperty("STAN")]
        public string Stan { get; set; }
        
        [JsonProperty("Amount")]
        public string Amount { get; set; }
        
        [JsonProperty("Signature")]
        public string Signature { get; set; }
    }
}
