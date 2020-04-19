namespace WSPay.Net
{
    public class RequestFactory: IRequestFactory
    {
        private readonly ISignatureFactory signatureFactory;
        private readonly ITimeProvider timeProvider;

        public RequestFactory()
            : this(new SignatureFactory(), new TimeProvider())
        {
        }
        
        public RequestFactory(
            ISignatureFactory signatureFactory,
            ITimeProvider timeProvider)
        {
            this.signatureFactory = signatureFactory;
            this.timeProvider = timeProvider;
        }

        public ProcessPaymentRequest CreateProcessPaymentRequest(string shoppingCartId, double price, string token, string tokenNumber)
        {
            var shop = WSPayConfiguration.TokenShop;

            var formattedPrice = WSPayHelpers.FormatPrice(price);
            var signature = signatureFactory.GenerateFormRequestSignature(WSPayConfiguration.TokenShop, shoppingCartId,
                price);
            
            return new ProcessPaymentRequest
            {
                ShopID =  shop.ShopId,
                ShoppingCartID = shoppingCartId,
                TotalAmount = formattedPrice,
                Signature = signature,
                Token = token,
                TokenNumber = tokenNumber,
                DateTime = timeProvider.Get().ToString("yyyyMMddHHmmss")
            };
        }

        public CompleteTransactionRequest CreateCompleteTransactionRequest(Shop shop, string wsPayOrderId, string stan, string approvalCode, double price)
        {
            var formattedPrice = WSPayHelpers.FormatPrice(price);
            var signature =
                signatureFactory.GenerateTransactionCompletionRequestSignature(shop, wsPayOrderId, stan,
                    approvalCode, price);
            
            return new CompleteTransactionRequest
            {
                WSPayOrderId = wsPayOrderId,
                ShopId = shop.ShopId,
                Amount = formattedPrice,
                Stan = stan,
                ApprovalCode = approvalCode,
                Signature = signature
            };
        }
        
        public StatusCheckRequest CreateStatusCheckRequest(Shop shop, string shoppingCartId)
        {
            var signature = signatureFactory.GenerateTransactionStatusCheckSignature(shop, shoppingCartId);
            return new StatusCheckRequest()
            {
                Signature = signature,
                ShopId= shop.ShopId,
                ShoppingCartId = shoppingCartId
            };
        }
        
        public FormRequest CreateFormRequest(string shoppingCartId, double price, Customer customer, PaymentType paymentType, IReturnUrlProvider returnUrlProvider)
        {
            var shop = WSPayConfiguration.RegularShop;
            var formattedPriceForRegularForm = WSPayHelpers.FormatAmountForRegularShopForm(price);
            
            return new FormRequest
            {
                Url = WSPayConfiguration.FormUrl.ToString(),
                ShopId = shop.ShopId,
                ShoppingCartID = shoppingCartId,
                Amount = formattedPriceForRegularForm,
                Signature = signatureFactory.GenerateFormRequestSignature(shop, shoppingCartId, price),
                CustomerFirstName = customer?.FirstName,
                CustomerSurname = customer?.LastName,
                CustomerEmail = customer?.Email,
                CustomerAddress = customer?.Address,
                CustomerPhone = customer?.Phone,
                IsTokenRequest = paymentType.IsNewTokenRequest,
                Token = paymentType.Token,
                TokenNumber = paymentType.TokenNumber,
                FormattedDateTime = timeProvider.Get().ToString("yyyyMMddHHmmss"),
                ReturnUrl = returnUrlProvider.GetReturnUrl(),
                CancelUrl = returnUrlProvider.GetCancelUrl(shoppingCartId),
                ErrorUrl = returnUrlProvider.GetErrorUrl()
            };
        }
    }
}