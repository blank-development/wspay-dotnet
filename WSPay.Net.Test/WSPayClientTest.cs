using FluentAssertions;
using Newtonsoft.Json;

namespace WSPay.Net.Test
{
    using Xunit;

    public class WSPayClientTest
    {
        class TestRequest
        {
            public int Id { get; set; } = 1;
        }
        
        class TestResponse
        {
            public int Id { get; set; } = 1;
        }
        
        [Fact]
        public void RequestAsync()
        {
            var response = new TestResponse();
            var client = TestHttpClient.CreateSuccessClientWithResponse("api/services/test", JsonConvert.SerializeObject(response));
            
            var asyncResult = client.RequestAsync<TestRequest, TestResponse>(new TestRequest(), "test").WaitTask();
            asyncResult.Should().BeEquivalentTo(response);
        }
        
         
        [Fact]
        public void RequestAsync_Error()
        {
            var client = TestHttpClient.CreateErrorClientWithResponse("Doslo je do pogreske");
            
            var exception = Assert.Throws<WSPayException>(() => client.RequestAsync<TestRequest, TestResponse>(new TestRequest(), "test").WaitTask());
            exception.Message.Should().Be("Doslo je do pogreske");
            exception.HttpStatusCode.Should().Be(400);
        }
        
        [Fact]
        public void Request()
        {
            var response = new TestResponse();
            var client = TestHttpClient.CreateSuccessClientWithResponse("api/services/test", JsonConvert.SerializeObject(response));
            
            var syncResult = client.Request<TestRequest, TestResponse>(new TestRequest(), "test");
            syncResult.Should().BeEquivalentTo(response);
        }
        
        [Fact]
        public void Request_Error()
        {
            var client = TestHttpClient.CreateErrorClientWithResponse("Doslo je do pogreske");
            
            var exception = Assert.Throws<WSPayException>(() => client.Request<TestRequest, TestResponse>(new TestRequest(), "test"));
            exception.Message.Should().Be("Doslo je do pogreske");
            exception.HttpStatusCode.Should().Be(400);
        }
    }
}