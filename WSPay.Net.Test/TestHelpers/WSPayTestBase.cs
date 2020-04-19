namespace WSPay.Net.Test
{
    public abstract class WSPayTestBase
    {
        protected WSPayTestBase()
        {
            WSPayConfiguration.Mode = Mode.Test;
            
            WSPayConfiguration.RegularShop = new Shop("regularShopId", "regularShopSecret");
            WSPayConfiguration.TokenShop = new Shop("tokenShopId", "tokenShopSecret");
        }

        public Shop RegularShop => WSPayConfiguration.RegularShop;
        public Shop TokenShop => WSPayConfiguration.TokenShop;
    }
}