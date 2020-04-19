namespace WSPay.Net
{
    public class FormRequest
    {
        public string Url { get; set; }
        public string ShopId { get; set; }
        public string ShoppingCartID { get; set; }
        public string Amount { get; set; }
        public string Signature { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerSurname { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerCity { get; set; }
        public string CustomerZip { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerAddress { get; set; }
        public bool IsTokenRequest { get; set; }
        public string Token { get; set; }
        public string TokenNumber { get; set; }
        public string FormattedDateTime { get; set; }

        public string ReturnUrl { get; set; }
        public string CancelUrl { get; set; }
        public string ErrorUrl { get; set; }

        public bool IsPayingWithToken => !string.IsNullOrEmpty(Token);
    }
}