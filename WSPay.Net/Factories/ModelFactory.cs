namespace WSPay.Net
{
    using System;
    using System.Collections.Generic;
    
    public class ModelFactory: IModelFactory
    {
        private readonly Settings settings;
        private readonly SignatureFactory signatureFactory;
        private readonly ITimeProvider timeProvider;
        
        public ModelFactory(
            Settings settings, 
            SignatureFactory signatureFactory,
            ITimeProvider timeProvider)
        {
            this.settings = settings;
            this.signatureFactory = signatureFactory;
            this.timeProvider = timeProvider;
        }

        public ProcessPaymentRequest CreateProcessPaymentRequest(string shoppingCartId, double price, string token, string tokenNumber)
        {
            var shop = this.settings.TokenShop;

            var formattedPrice = WSPayHelpers.FormatPrice(price);
            var signature = this.signatureFactory.GenerateFormRequestSignature(this.settings.TokenShop, shoppingCartId,
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

        public FormRequest CreateFormRequest(string shoppingCartId, double price, Customer customer, PaymentType paymentType, IReturnUrlProvider returnUrlProvider)
        {
            var shop = this.settings.RegularShop;
            
            var formattedPriceForRegularForm = WSPayHelpers.FormatAmountForRegularShopForm(price);
            var formattedPrice = WSPayHelpers.FormatPrice(price);
            
            return new FormRequest
            {
                Url = this.settings.FormRequestUrl,
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

        public IDictionary<string, string> CreateAutoServiceRequest(Shop shop, string wsPayOrderId, string stan, string approvalCode, double totalPrice, AutoServiceType? serviceType = null)
        {
            var formattedPrice = WSPayHelpers.FormatPrice(totalPrice);
            var request = new Dictionary<string, string>()
            {
                { "WSPayOrderId", wsPayOrderId },
                { "ShopID", shop.ShopId },
                { "STAN", stan },
                { "Amount", formattedPrice },
                { "Signature", this.signatureFactory.GenerateTransactionRequestSignature(shop, wsPayOrderId, stan, approvalCode, formattedPrice) },
                { "ApprovalCode", approvalCode },
                { "ReturnURL", "" },
                { "ReturnErrorURL", "" },
            };

            if (serviceType != null)
            {
                request.Add("ServiceType", serviceType.ToString());
            }

            return request;
        }

        public IDictionary<string, string> CreateStatusCheckRequest(Shop shop, string shoppingCartId)
        {
            var data = new Dictionary<string, string>()
            {
                { "ServiceType", "StatusCheck" },
                { "ShopID", shop.ShopId },
                { "ShoppingCartID", shoppingCartId },
                { "Signature", this.signatureFactory.GenerateTransactionStatusCheckSignature(shop, shoppingCartId) },
                { "ReturnURL", "" },
                { "ReturnErrorURL", "" },
            };

            return data;
        }
    }
}