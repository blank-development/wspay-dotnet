﻿namespace WSPay.Net
{
    public class PaymentType
    {
        public PaymentType(bool isNewTokenRequest, string token, string tokenNumber)
        {
            this.Token = token;
            this.TokenNumber = tokenNumber;
            this.IsNewTokenRequest = !this.IsPayingWithToken && isNewTokenRequest;
        }

        public bool IsNewTokenRequest { get; set; }
        public string Token { get; set; }
        public string TokenNumber { get; set; }

        public bool IsPayingWithToken => !string.IsNullOrEmpty(Token) && !string.IsNullOrEmpty(TokenNumber);
    }
}
