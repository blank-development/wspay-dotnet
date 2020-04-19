using FluentAssertions;
using Xunit;

namespace WSPay.Net.Test
{
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
            actual.Should().Be("9a2235f5a4ef2e0d4a5036e9647a2522");
        }
        
        [Fact]
        public void GenerateTransactionCompletionRequestSignature()
        {
            var actual = signatureFactory.GenerateTransactionCompletionRequestSignature(RegularShop, "testCartId", "stan", "approvalCode", 15.25);
            actual.Should().Be("811a70921015b6b95e6541da6d73cf43");
        }
        
        [Fact]
        public void GenerateTransactionStatusCheckSignature()
        {
            var actual = signatureFactory.GenerateTransactionStatusCheckSignature(RegularShop, "testCartId");
            actual.Should().Be("7ba0869877d3bf902f2973bd84428f9a");
        }
    }
}