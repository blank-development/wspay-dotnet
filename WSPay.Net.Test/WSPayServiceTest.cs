using RichardSzalay.MockHttp;

namespace WSPay.Net.Test
{
    using Newtonsoft.Json;
    using Xunit;
    using FluentAssertions;

    public class WSPayServiceTest: WSPayTestBase
    {
        [Fact]
        public void ProcessPayment()
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
            
            var service = BuildServiceWithSuccessResponse("api/services/ProcessPayment", response);
            
            var asyncResult = service.ProcessPaymentAsync("cartId", 15.50, "token", "tokenNumer").WaitTask();
            asyncResult.Should().BeEquivalentTo(response);

            var syncResult = service.ProcessPayment("cartId", 15.50, "token", "tokenNumer");
            syncResult.Should().BeEquivalentTo(response);
        }
        
        [Fact]
        public void ProcessPayment_Error()
        {
            var service = BuildServiceWithErrorResponse("Doslo je do pogreske");
            
            Assert.Throws<WSPayException>(() =>
                service.ProcessPaymentAsync("cartId", 15.50, "token", "tokenNumer").WaitTask());
            
            Assert.Throws<WSPayException>(() =>
                service.ProcessPayment("cartId", 15.50, "token", "tokenNumer"));
        }
        
        [Fact]
        public void CheckStatus()
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
            
            var service = BuildServiceWithSuccessResponse("api/services/StatusCheck", response);
            
            var asyncResult = service.CheckStatusAsync(RegularShop, "shoppingCartId").WaitTask();
            asyncResult.Should().BeEquivalentTo(response);

            var syncResult = service.CheckStatus(RegularShop, "shoppingCartId");
            syncResult.Should().BeEquivalentTo(response);
        }
        
        [Fact]
        public void CheckStatus_Error()
        {
            var service = BuildServiceWithErrorResponse("Doslo je do pogreske");
            
            Assert.Throws<WSPayException>(() =>
                service.CheckStatusAsync(RegularShop, "shoppingCartId").WaitTask());
            
            Assert.Throws<WSPayException>(() =>
                service.CheckStatus(RegularShop, "shoppingCartId"));
        }
        
        [Fact]
        public void RefundTransaction()
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
            
            var service = BuildServiceWithSuccessResponse("api/services/Refund", response);
            
            var asyncResult = service.RefundTransactionAsync(RegularShop, "wsPayOrderId", "stan", "approvalCode", 15.50)
                .WaitTask();
            asyncResult.Should().BeEquivalentTo(response);

            var syncResult = service.RefundTransaction(RegularShop, "wsPayOrderId", "stan", "approvalCode", 15.50);
            syncResult.Should().BeEquivalentTo(response);
        }
        
        [Fact]
        public void RefundTransaction_Error()
        {
            var service = BuildServiceWithErrorResponse("Doslo je do pogreske");
            
            Assert.Throws<WSPayException>(() =>
                service.RefundTransactionAsync(RegularShop, "wsPayOrderId", "stan", "approvalCode", 15.50).WaitTask());
            
            Assert.Throws<WSPayException>(() =>
                service.RefundTransaction(RegularShop, "wsPayOrderId", "stan", "approvalCode", 15.50));
        }
        
        [Fact]
        public void VoidTransaction()
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
            
            var service = BuildServiceWithSuccessResponse("api/services/Void", response);
            
            var asyncResult = service.VoidTransactionAsync(RegularShop, "wsPayOrderId", "stan", "approvalCode", 15.50)
                .WaitTask();
            asyncResult.Should().BeEquivalentTo(response);

            var syncResult = service.VoidTransaction(RegularShop, "wsPayOrderId", "stan", "approvalCode", 15.50);
            syncResult.Should().BeEquivalentTo(response);
        }
        
        [Fact]
        public void VoidTransaction_Error()
        {
            var service = BuildServiceWithErrorResponse("Doslo je do pogreske");
            
            Assert.Throws<WSPayException>(() =>
                service.VoidTransactionAsync(RegularShop, "wsPayOrderId", "stan", "approvalCode", 15.50).WaitTask());
            
            Assert.Throws<WSPayException>(() =>
                service.VoidTransaction(RegularShop, "wsPayOrderId", "stan", "approvalCode", 15.50));
        }
        
        [Fact]
        public void CompleteTransaction()
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
            
            var service = BuildServiceWithSuccessResponse("api/services/Completion", response);
            
            var asyncResult = service.CompleteTransactionAsync(RegularShop, "wsPayOrderId", "stan", "approvalCode", 15.50)
                .WaitTask();
            asyncResult.Should().BeEquivalentTo(response);

            var syncResult = service.CompleteTransaction(RegularShop, "wsPayOrderId", "stan", "approvalCode", 15.50);
            syncResult.Should().BeEquivalentTo(response);
        }
        
        [Fact]
        public void CompleteTransaction_Error()
        {
            var service = BuildServiceWithErrorResponse("Doslo je do pogreske");
            
            Assert.Throws<WSPayException>(() =>
                service.CompleteTransactionAsync(RegularShop, "wsPayOrderId", "stan", "approvalCode", 15.50).WaitTask());
            
            Assert.Throws<WSPayException>(() =>
                service.CompleteTransaction(RegularShop, "wsPayOrderId", "stan", "approvalCode", 15.50));
        }

        private IWSPayService BuildServiceWithSuccessResponse<T>(string url, T response) where T: class
        {
            var client = TestHttpClient.CreateSuccessClientWithResponse(url, JsonConvert.SerializeObject(response));
            return new WSPayService(new RequestFactory(), client);
        }
        
        private IWSPayService BuildServiceWithErrorResponse(string errorResponse)
        {
            var client = TestHttpClient.CreateErrorClientWithResponse(errorResponse);
            return new WSPayService(new RequestFactory(), client);
        }
    }
}