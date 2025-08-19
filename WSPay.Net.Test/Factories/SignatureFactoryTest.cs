namespace WSPay.Net.Test
{
    using FluentAssertions;
    using Xunit;
    
    public class SignatureFactoryTest: WSPayTestBase
    {
        private readonly ISignatureFactory signatureFactory;
        public SignatureFactoryTest()
        {
            signatureFactory = new SignatureFactory();
        }
        
        [Fact]
        public void GenerateFormRequestSignature()
        {
            var actual = signatureFactory.GenerateFormRequestSignature(RegularShop, "testCartId", 15.25);
            actual.Should().Be("05b44b25ce8883c16e40cff78163ddfd40a5b86baf6f042a848cddebbe9a8db2fc03079f7cfa31c13d469f262ad93540445f9433a8df51dff689c59ec1596234");
        }
        
        [Fact]
        public void GenerateChangeTransactionStatusSignature()
        {
            var actual = signatureFactory.GenerateChangeTransactionStatusSignature(RegularShop, "testCartId", "stan", "approvalCode", 15.25);
            actual.Should().Be("811a70921015b6b95e6541da6d73cf43");
        }
        
        [Fact]
        public void GenerateTransactionStatusCheckSignature()
        {
            var actual = signatureFactory.GenerateTransactionStatusCheckSignature(RegularShop, "testCartId");
            actual.Should().Be("9ea4962173b9cbbddceb7e54311a92127df62c6c5e04be3e10014bf8dd3bf954588f701fa7fffcc432cc81c402be799c88f68ddcca0baebbd466e07df7aacb4d");
        }
    }
}