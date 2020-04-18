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

        [Theory]
        [InlineData(null)]
        [InlineData(AutoServiceType.Refund)]
        [InlineData(AutoServiceType.Void)]
        public void CreateAutoServiceRequest(AutoServiceType? type)
        {
            var actual = this.modelFactory.CreateAutoServiceRequest(this.settings.RegularShop, "testShoppingCartid", "stan", "approvalCode", 150.50, type);
            var expected = new Dictionary<string, string>
            {
                { "WSPayOrderId", "testShoppingCartid" },
                { "ShopID", this.settings.RegularShop.ShopId },
                { "STAN", "stan" },
                { "Amount", "15050" },
                { "Signature", "9591e1a405bb1ab7a233b5d23287b976" },
                { "ApprovalCode", "approvalCode" },
                { "ReturnURL", "" },
                { "ReturnErrorURL", "" },
            };

            if (type != null)
            {
                expected.Add("ServiceType", type.ToString());
            }
            
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