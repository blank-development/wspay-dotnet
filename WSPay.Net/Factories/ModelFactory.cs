namespace WSPay.Net
{
    public class ModelFactory: IModelFactory
    {
        private readonly SignatureFactory signatureFactory;
        private readonly ITimeProvider timeProvider;
        
        public ModelFactory(
            SignatureFactory signatureFactory,
            ITimeProvider timeProvider)
        {
            this.signatureFactory = signatureFactory;
            this.timeProvider = timeProvider;
        }

        public ProcessPaymentRequest CreateProcessPaymentRequest(string shoppingCartId, double price, string token, string tokenNumber)
        {
            var shop = WSPayConfiguration.TokenShop;

            var formattedPrice = WSPayHelpers.FormatPrice(price);
            var signature = this.signatureFactory.GenerateFormRequestSignature(WSPayConfiguration.TokenShop, shoppingCartId,
                formattedPrice);
            
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
                this.signatureFactory.GenerateTransactionCompletionRequestSignature(shop, wsPayOrderId, stan,
                    approvalCode, formattedPrice);
            
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

        public FormRequest CreateFormRequest(string shoppingCartId, double price, Customer customer, PaymentType paymentType, IReturnUrlProvider returnUrlProvider)
        {
            var shop = WSPayConfiguration.RegularShop;
            
            var formattedPriceForRegularForm = WSPayHelpers.FormatAmountForRegularShopForm(price);
            var formattedPrice = WSPayHelpers.FormatPrice(price);
            
            return new FormRequest
            {
               // Url = this.settings.FormRequestUrl,
                ShopId = shop.ShopId,
                ShoppingCartID = shoppingCartId,
                Amount = formattedPriceForRegularForm,
                Signature = this.signatureFactory.GenerateFormRequestSignature(shop, shoppingCartId, formattedPrice),
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
       
        public StatusCheckRequest CreateStatusCheckRequest(Shop shop, string shoppingCartId)
        {
            var signature = this.signatureFactory.GenerateTransactionStatusCheckSignature(shop, shoppingCartId);
            return new StatusCheckRequest()
            {
                Signature = signature,
                ShopId= shop.ShopId,
                ShoppingCartId = shoppingCartId
            };
        }
    }
}