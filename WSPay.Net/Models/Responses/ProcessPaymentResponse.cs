﻿namespace WSPay.Net
{
    public class ProcessPaymentResponse : ProcessPaymentRequest
    {
        public static ProcessPaymentResponse CreateError()
        {
            return new ProcessPaymentResponse
            {
                Approved = "0"
            };
        }

        public string WSPayOrderId { get; set; }
        public string ApprovalCode { get; set; }
        public string STAN { get; set; }
        public string PaymentType { get; set; }
        public string ErrorMessage { get; set; }
        public string ECI { get; set; }
        public string Approved { get; set; }

        public bool IsSuccess => Approved == "1";
    }
}