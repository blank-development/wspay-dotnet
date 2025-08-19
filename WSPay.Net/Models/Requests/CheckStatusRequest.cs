namespace WSPay.Net
{
    using Newtonsoft.Json;

    [JsonObject]
    public class StatusCheckRequest
    {
        [JsonProperty("ShopID")]
        public string ShopId { get; set; }

        [JsonProperty("ShoppingCartID")]
        public string ShoppingCartId { get; set; }
        
        [JsonProperty("Signature")]
        public string Signature { get; set; }

        [JsonProperty("Version")]
        public string Version { get; set; }
    }
}