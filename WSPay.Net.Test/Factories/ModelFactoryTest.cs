using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace WSPay.Net.Test.Factories
{
    public class ModelFactoryTest
    {
        private readonly IModelFactory modelFactory;
        private readonly Settings settings;
        public ModelFactoryTest()
        {
            this.settings = TestSettingsFactory.Create();
            this.modelFactory = new ModelFactory(this.settings, new SignatureFactory(), new TestTimeProvider());
        }
        
        [Fact]
        public void CreateProcessPaymentRequest()
        {
            var actual = this.modelFactory.CreateProcessPaymentRequest("123", 150.25, "token", "token123");
            var expected = new ProcessPaymentRequest
            {
                ShopID = this.settings.TokenShop.ShopId,
                ShoppingCartID = "123",
                DateTime = "20200401152030",
                Signature = "ee9d361c6cc9d904db856034b1098318",
                TokenNumber = "token123",
                Token = "token",
                TotalAmount = "15025"
            };

            actual.Should().BeEquivalentTo(expected);
        }
        
        [Fact]
        public void CreateStatusCheckRequest()
        {
            var actual = this.modelFactory.CreateStatusCheckRequest(this.settings.RegularShop, "testShoppingCartid");
            var expected = new Dictionary<string, string>
            {
                {"ServiceType", "StatusCheck"},
                {"ShopID", this.settings.RegularShop.ShopId},
                {"ShoppingCartID", "testShoppingCartid"},
                {"Signature", "5148f1f97444f0ec1857e2cea4cac0c1"},
                {"ReturnURL", ""},
                {"ReturnErrorURL", ""},
            };

            actual.Should().BeEquivalentTo(expected);
        }
    }
}