namespace WSPay.Net.Test
{
    public class TestSettingsFactory
    {
        public static Settings Create()
        {
            return new Settings
            {
                FormRequestUrl = "https://formtest.wspay.biz/Authorization.asp",
                AutoUrl = "https://test.wspay.biz/WsPayAuto.asp",
                AutoServicesUrl = "https://test.wspay.biz/WSPayAutoServices.aspx",
                ProcessPaymentJsonApiUrl = "https://test.wspay.biz/api/services/ProcessPayment",
                TokenShop = new Shop("tokenShopId", "tokenTokenSecret"),
                RegularShop = new Shop("regularShopId", "regularTokenSecret")
            };
        }
    }
}