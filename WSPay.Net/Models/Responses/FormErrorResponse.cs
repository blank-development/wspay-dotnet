﻿namespace WSPay.Net
{
    public class FormErrorResponse : FormRequest
    {
        public string ErrorMessage { get; set; }
        public string PaymentType { get; set; }
        public string ECI { get; set; }
    }
}