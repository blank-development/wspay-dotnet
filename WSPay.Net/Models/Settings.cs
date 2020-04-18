﻿namespace WSPay.Net
{
    public class Settings
    {
        public string FormRequestUrl { get; set; }
        public string AutoServicesUrl { get; set; }
        public string AutoUrl { get; set; }
        public string ProcessPaymentJsonApiUrl { get; set; }

        public Shop TokenShop { get; set; }
        public Shop RegularShop { get; set; }

        public bool CheckSignature { get; set; }
    }
}