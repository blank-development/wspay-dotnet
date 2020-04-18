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
                ShopID = this.settings.RegularShop.ShopId,
                ShoppingCartID = "123",
                DateTime = "20200401152030",
                Signature = "asdasd",
                TokenNumber = "token123",
                Token = "token",
                TotalAmount = "15025"
            };

            actual.Should().BeEquivalentTo(expected);
        }
    }
}