﻿namespace WSPay.Net
{
    public class ProcessPaymentRequest
    {
        public string ShopID { get; set; }
        public string ShoppingCartID { get; set; }
        public string TotalAmount { get; set; }
        public string Signature { get; set; }
        public string Token { get; set; }
        public string TokenNumber { get; set; }
        public string DateTime { get; set; }
        public string PaymentPlan => "0000";
        public string Lang => "HR";
    }
}