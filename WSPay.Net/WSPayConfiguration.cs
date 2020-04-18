namespace WSPay.Net
{
    using System.Configuration;
    using System;
    
    public static class WSPayConfiguration
    {
        private static Mode mode { get; set; } = Mode.Test;
        private static Shop tokenShop { get; set; }
        private static Shop regularShop { get; set; }
        
        public static Mode Mode
        {
            get
            {
                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["WSPayMode"]))
                {
                    if (Enum.TryParse<Mode>(ConfigurationManager.AppSettings["WSPayMode"], out var parsedMode))
                    {
                        throw new ArgumentException("Invalid WSPayMode configured");
                    }

                    mode = parsedMode;
                }

                return mode;
            }

            set => mode = value;
        }
        
        public static Shop TokenShop
        {
            get
            {
                if (tokenShop == null 
                    && string.IsNullOrEmpty(ConfigurationManager.AppSettings["WSPayTokenShopId"])
                    && string.IsNullOrEmpty(ConfigurationManager.AppSettings["WSPayTokenShopSecret"]))
                {
                    tokenShop = new Shop(ConfigurationManager.AppSettings["WSPayTokenShopId"], ConfigurationManager.AppSettings["WSPayTokenShopSecret"]);
                }

                return tokenShop;
            }

            set => tokenShop = value;
        }
        
        public static Shop RegularShop
        {
            get
            {
                if (regularShop == null 
                    && string.IsNullOrEmpty(ConfigurationManager.AppSettings["WSPayRegularShopId"])
                    && string.IsNullOrEmpty(ConfigurationManager.AppSettings["WSPayRegularShopSecret"]))
                {
                    regularShop = new Shop(ConfigurationManager.AppSettings["WSPayRegularShopId"], ConfigurationManager.AppSettings["WSPayRegularShopSecret"]);
                }

                return regularShop;
            }

            set => regularShop = value;
        }
        
        public static Uri BaseUrl
        {
            get
            {
                switch (Mode)
                {
                    case Mode.Prod:
                        return new Uri("https://secure.wspay.biz");
                    case Mode.Test:
                        return new Uri("https://test.wspay.biz");
                
                    default:
                        throw new ArgumentException("Invalid mode");
                }
            }
        }
    }
}