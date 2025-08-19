namespace WSPay.Net.Test
{
    using FluentAssertions;
    using Xunit;
    
    public class RequestFactoryTest : WSPayTestBase
    {
        private readonly IRequestFactory modelFactory;
        public RequestFactoryTest()
        {
            modelFactory = new RequestFactory(new SignatureFactory(), new TestTimeProvider());
        }
        
        [Fact]
        public void CreateProcessPaymentRequest()
        {
            var actual = modelFactory.CreateProcessPaymentRequest("123", 150.25, "token", "token123");
            var expected = new ProcessPaymentRequest
            {
                ShopId = "tokenShopId",
                ShoppingCartId = "123",
                DateTime = "20200401152030",
                Signature = "b6dcfe6c4deed0a7f5078b8d1091c8daff3182f7d766cb25040b508a49d27bdf7e2085b87e335a8a5aa2a20aca968565b101cd2454f18101f656f92f6baf0834",
                TokenNumber = "token123",
                Token = "token",
                TotalAmount = "15025",
                Version = "2.0"
            };

            actual.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData(false, null, null)]
        [InlineData(true, null, null)]
        [InlineData(false, "token", "tokenNumber")]
        [InlineData(true, "token", "tokenNumber")]
        public void CreateFormRequest(bool isNewTokenRequest, string token, string tokenNumber)
        {
            var customer = CustomerFactory.Create();
            var paymentType = new PaymentType(isNewTokenRequest, token, tokenNumber);
            var urlProvider = new TestUrlProvider();
            var actual = modelFactory.CreateFormRequest("testShoppingCartId", 15.50,  customer, paymentType, urlProvider);

            var expected = new FormRequest
            {
                Url = WSPayConfiguration.FormUrl.ToString(),
                ShopId = RegularShop.ShopId,
                ShoppingCartID = "testShoppingCartId",
                Amount = "15,5",
                Signature = "ee06d42e8589d2454b29c91c3ac8587f3c70699cca0f008c5405d3aab3b7f0510d78d17b73d6f8fe2becee865941bd32e6b9f7d5c537f977c97fef276b055844",
                CustomerFirstName = customer.FirstName,
                CustomerSurname = customer.LastName,
                CustomerEmail = customer.Email,
                CustomerAddress = customer.Address,
                CustomerPhone = customer.Phone,
                IsTokenRequest = paymentType.IsNewTokenRequest,
                Token = paymentType.Token,
                TokenNumber = paymentType.TokenNumber,
                FormattedDateTime = "20200401152030",
                ReturnUrl = urlProvider.GetReturnUrl(),
                CancelUrl = urlProvider.GetCancelUrl("testShoppingCartId"),
                ErrorUrl = urlProvider.GetErrorUrl(),
                Version = "2.0"
            };
            
            actual.Should().BeEquivalentTo(expected);
        }
        
        [Fact]
        public void CreateStatusCheckRequest()
        {
            var actual = modelFactory.CreateStatusCheckRequest(RegularShop, "testShoppingCartid");
            var expected = new StatusCheckRequest()
            {
                Signature = "7756f016b34d15aaa321f9338d947800ba1f91d268776337490b1fac906cd418a19b8e251d876c26524239f59e772502b945c0e25b06e8a73a3a3cf7656718a0",
                ShopId= RegularShop.ShopId,
                ShoppingCartId = "testShoppingCartid",
                Version = "2.0"
            };
            
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void CreateChangeTransactionStatusRequest()
        {
            var actual = modelFactory.CreateChangeTransactionStatusRequest(RegularShop, "testShoppingCartid", "stan", "approvalCode", 15.25);
            var expected = new ChangeTransactionStatusRequest
            {
                WSPayOrderId = "testShoppingCartid",
                ShopId = RegularShop.ShopId,
                Amount = "1525",
                Stan = "stan",
                ApprovalCode = "approvalCode",
                Signature = "fb19368246bfde27ab63381bb2a7282fab33a90330a9d1a54e686f36f2913f5da06d64b60abf1b71bc6a43d82c14623f2120f51e42b3458bf0417a5043a616c6",
                Version = "2.0"
            };
            
            actual.Should().BeEquivalentTo(expected);
        }
    }
}