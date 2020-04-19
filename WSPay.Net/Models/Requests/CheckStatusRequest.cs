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
        public string Signature { get; set; }
    }
}