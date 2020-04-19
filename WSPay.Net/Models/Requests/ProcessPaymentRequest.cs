﻿using Newtonsoft.Json;

namespace WSPay.Net
{
    [JsonObject]
    public class ProcessPaymentRequest
    {
        [JsonProperty("ShopID")]
        public string ShopId { get; set; }
        [JsonProperty("ShoppingCartID")]
        public string ShoppingCartId { get; set; }
        [JsonProperty("TotalAmount")]
        public string TotalAmount { get; set; }
        [JsonProperty("Signature")]
        public string Signature { get; set; }
        [JsonProperty("Token")]
        public string Token { get; set; }
        [JsonProperty("TokenNumber")]
        public string TokenNumber { get; set; }
        [JsonProperty("DateTime")]
        public string DateTime { get; set; }
        public string PaymentPlan => "0000";
        public string Lang => "HR";
    }
}