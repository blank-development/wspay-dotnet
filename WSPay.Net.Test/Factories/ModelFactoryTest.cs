namespace WSPay.Net.Test
{
    using System.Collections.Generic;
    using FluentAssertions;
    using Xunit;
    
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
        [InlineData(false, null, null)]
        [InlineData(true, null, null)]
        [InlineData(false, "token", "tokenNumber")]
        [InlineData(true, "token", "tokenNumber")]
        public void CreateFormRequest(bool isNewTokenRequest, string token, string tokenNumber)
        {
            var customer = CustomerFactory.Create();
            var paymentType = new PaymentType(isNewTokenRequest, token, tokenNumber);
            var urlProvider = new TestUrlProvider();
            var actual = this.modelFactory.CreateFormRequest("testShoppingCartId", 15.50,  customer, paymentType, urlProvider);

            var expected = new FormRequest
            {
                Url = this.settings.FormRequestUrl,
                ShopId = this.settings.RegularShop.ShopId,
                ShoppingCartID = "testShoppingCartId",
                Amount = "15,5",
                Signature = "dc703fac7712bbda560090ef58881734",
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
        
        [Theory]
        [InlineData(null)]
        [InlineData(AutoServiceType.Refund)]
        [InlineData(AutoServiceType.Void)]
        public void CreateAutoServiceRequest(AutoServiceType? type)
        {
            var actual = this.modelFactory.CreateAutoServiceRequest(this.settings.RegularShop, "wsPayOrderId", "stan", "approvalCode", 150.50, type);
            var expected = new Dictionary<string, string>
            {
                { "WSPayOrderId", "wsPayOrderId" },
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