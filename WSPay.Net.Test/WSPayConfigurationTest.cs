namespace WSPay.Net.Test
{
    using System.Configuration;
    using FluentAssertions;
    using Xunit;
    
    public class WSPayConfigurationTest: WSPayTestBase
    {
        [Theory]
        [InlineData(null, Net.Mode.Test)]
        [InlineData("Test", Net.Mode.Test)]
        [InlineData("Prod", Net.Mode.Prod)]
        public void Mode(string configMode, Mode expected)
        {
            ConfigurationManager.AppSettings["WSPayMode"] = configMode;

            WSPayConfiguration.Mode.Should().Be(expected);
        }

        [Fact]
        public void Mode_ManualSet()
        {
            WSPayConfiguration.Mode = Net.Mode.Prod;
            
            WSPayConfiguration.Mode.Should().Be(Net.Mode.Prod);
        }

        [Fact]
        public void TokenShop()
        {
            WSPayConfiguration.TokenShop = null;

            ConfigurationManager.AppSettings["WSPayTokenShopId"] = "tokenShopId";
            ConfigurationManager.AppSettings["WSPayTokenShopSecret"] = "tokenShopSecret";
            var expected = new Shop("tokenShopId", "tokenShopSecret");
            
            WSPayConfiguration.TokenShop.Should().BeEquivalentTo(expected);
        }
        
        [Fact]
        public void TokenShop_ManualSet()
        {
            WSPayConfiguration.TokenShop = null;

            var shop = new Shop("tokenShopId", "tokenShopSecret");
            WSPayConfiguration.TokenShop = shop;
            
            WSPayConfiguration.TokenShop.Should().BeEquivalentTo(shop);
        }
        
        [Fact]
        public void RegularShop()
        {
            WSPayConfiguration.RegularShop = null;

            ConfigurationManager.AppSettings["WSPayRegularShopId"] = "regularShopId";
            ConfigurationManager.AppSettings["WSPayRegularShopSecret"] = "regularShopSecret";
            var expected = new Shop("regularShopId", "regularShopSecret");
            
            WSPayConfiguration.RegularShop.Should().BeEquivalentTo(expected);
        }
        
        [Fact]
        public void RegularShop_ManualSet()
        {
            WSPayConfiguration.RegularShop = null;

            var shop = new Shop("regularShopId", "regularShopSecret");
            WSPayConfiguration.RegularShop = shop;
            
            WSPayConfiguration.RegularShop.Should().BeEquivalentTo(shop);
        }

        [Theory]
        [InlineData("shopid", null)]
        [InlineData(null, "shopSecret")]
        [InlineData(null, null)]
        public void RegularShop_MissingConfigThrows(string shopId, string shopSecret)
        {
            WSPayConfiguration.RegularShop = null;
            
            ConfigurationManager.AppSettings["WSPayRegularShopId"] = shopId;
            ConfigurationManager.AppSettings["WSPayRegularShopSecret"] = shopSecret;

            Assert.Throws<WSPayException>(() => WSPayConfiguration.RegularShop);
        }
    }
}