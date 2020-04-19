﻿namespace WSPay.Net
{
    public class PaymentType
    {
        public PaymentType(bool isNewTokenRequest, string token, string tokenNumber)
        {
            Token = token;
            TokenNumber = tokenNumber;
            IsNewTokenRequest = !IsPayingWithToken && isNewTokenRequest;
        }

        public bool IsNewTokenRequest { get; set; }
        public string Token { get; set; }
        public string TokenNumber { get; set; }

        public bool IsPayingWithToken => !string.IsNullOrEmpty(Token) && !string.IsNullOrEmpty(TokenNumber);
    }
}
