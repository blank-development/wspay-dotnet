namespace WSPay.Net
{
    public class Shop
    {
        public Shop(string shopId, string secret)
        {
            this.ShopId = shopId;
            this.Secret = secret;
        }

        public string ShopId { get; private set; }
        public string Secret { get; private set; }
    }
}