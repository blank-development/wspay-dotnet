﻿using Newtonsoft.Json;

 namespace WSPay.Net
{
    public class ProcessPaymentResponse : ProcessPaymentRequest
    {
        [JsonProperty("WSPayOrderId")]
        public string WSPayOrderId { get; set; }
        [JsonProperty("ApprovalCode")]
        public string ApprovalCode { get; set; }
        [JsonProperty("STAN")]
        public string Stan { get; set; }
        [JsonProperty("PaymentType")]
        public string PaymentType { get; set; }
        [JsonProperty("ErrorMessage")]
        public string ErrorMessage { get; set; }
        [JsonProperty("ECI")]
        public string Eci { get; set; }
        [JsonProperty("Approved")]
        public string Approved { get; set; }

        public bool IsSuccess => Approved == "1";
    }
}