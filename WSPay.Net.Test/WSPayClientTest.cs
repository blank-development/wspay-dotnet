namespace WSPay.Net.Test
{
    using FluentAssertions;
    using Newtonsoft.Json;
    using Xunit;
    
    public class WSPayClientTest
    {
        [Fact]
        public void ProcessPaymentAsync()
        {
            var response = new ProcessPaymentResponse()
            {
                Approved = "1",
                Eci = "12f",
                Signature = "asdokasokdas",
                Stan = "Stan",
                Token = "Token",
                ApprovalCode = "123",
                DateTime = "1212",
                ErrorMessage = null,
                PaymentType = "123",
                ShopId = "REGULAR",
                TokenNumber = "asd",
                TotalAmount = "122",
                ShoppingCartId = "cartId",
                WSPayOrderId = "orderId"
            };
            
            var client = TestHttpClient.CreateSuccessClientWithResponse(JsonConvert.SerializeObject(response));
            var result = client.ProcessPaymentAsync(new ProcessPaymentRequest()).Result;

            result.Should().BeEquivalentTo(response);
        }
        
        [Fact]
        public void CheckStatusAsync()
        {
            var response = new StatusCheckResponse()
            {
                Amount = "12",
                Completed = "1",
                Partner = "Part",
                Refunded = "0",
                Signature = "asdsad",
                Stan = "stan",
                Success = "1",
                Voided = "0",
                ActionSuccess = "1",
                ApprovalCode = "code",
                ErrorMessage = "",
                PaymentPlan = "PLAN",
                PaymentType = "123",
                ShopId = "REGULAR",
                TokenNumber = "asd",
                CreditCardName = "AMEX",
                WSPayOrderId = "orderId"
            };
            
            var client = TestHttpClient.CreateSuccessClientWithResponse(JsonConvert.SerializeObject(response));
            var result = client.CheckStatusAsync(new StatusCheckRequest()).Result;

            result.Should().BeEquivalentTo(response);
        }
        
        [Fact]
        public void RefundTransactionAsync()
        {
            var response = new ChangeTransactionStatusResponse()
            {
                Signature = "asdf",
                Stan = "STAN",
                ActionSuccess = "1",
                ApprovalCode = "code",
                ErrorMessage = "",
                ShopId = "REGULAR",
                WSPayOrderId = "orderId"
            };
            
            var client = TestHttpClient.CreateSuccessClientWithResponse(JsonConvert.SerializeObject(response));
            var result = client.RefundTransactionAsync(new ChangeTransactionStatusRequest()).Result;

            result.Should().BeEquivalentTo(response);
        }
        
        [Fact]
        public void VoidTransactionAsync()
        {
            var response = new ChangeTransactionStatusResponse()
            {
                Signature = "asdf",
                Stan = "STAN2",
                ActionSuccess = "1",
                ApprovalCode = "code",
                ErrorMessage = "",
                ShopId = "REGULAR",
                WSPayOrderId = "orderId"
            };
            
            var client = TestHttpClient.CreateSuccessClientWithResponse(JsonConvert.SerializeObject(response));
            var result = client.VoidTransactionAsync(new ChangeTransactionStatusRequest()).Result;

            result.Should().BeEquivalentTo(response);
        }
        
        [Fact]
        public void CompleteTransactionAsync()
        {
            var response = new ChangeTransactionStatusResponse()
            {
                Signature = "asdf",
                Stan = "STAN3",
                ActionSuccess = "1",
                ApprovalCode = "code",
                ErrorMessage = "",
                ShopId = "REGULAR",
                WSPayOrderId = "orderId"
            };
            
            var client = TestHttpClient.CreateSuccessClientWithResponse(JsonConvert.SerializeObject(response));
            var result = client.CompleteTransactionAsync(new ChangeTransactionStatusRequest()).Result;

            result.Should().BeEquivalentTo(response);
        }
    }
}