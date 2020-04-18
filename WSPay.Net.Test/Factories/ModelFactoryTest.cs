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
    }
}