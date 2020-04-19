namespace WSPay.Net.Test
{
    using FluentAssertions;
    using Xunit;
    
    public class ModelFactoryTest: WSPayTestBase
    {
        private readonly IRequestFactory modelFactory;
        public ModelFactoryTest()
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
                Signature = "a7e5f92c6238781650a12b4632a22381",
                TokenNumber = "token123",
                Token = "token",
                TotalAmount = "15025"
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
                Signature = "8bb5ec7f987f3cf3ce1e3153cfeab963",
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
                ErrorUrl = urlProvider.GetErrorUrl()
            };
            
            actual.Should().BeEquivalentTo(expected);
        }
        
        [Fact]
        public void CreateStatusCheckRequest()
        {
            var actual = modelFactory.CreateStatusCheckRequest(RegularShop, "testShoppingCartid");
            var expected = new StatusCheckRequest()
            {
                Signature = "aa91668118a78018da84f88f1a6fe341",
                ShopId= RegularShop.ShopId,
                ShoppingCartId = "testShoppingCartid"
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
                Signature = "8c56fab77dcba8edca85cc5feb618a03"
            };
            
            actual.Should().BeEquivalentTo(expected);
        }
    }
}