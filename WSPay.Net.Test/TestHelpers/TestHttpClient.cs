
namespace WSPay.Net.Test
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Moq;
    using Moq.Protected;
    using System.Text;
    using RichardSzalay.MockHttp;

    public class TestHttpClient
    {
        public static IWSPayClient CreateSuccessClientWithResponse(string url, string response)
        {
            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When(HttpMethod.Post, $"{WSPayConfiguration.BaseApiUrl}{url}")
                .Respond("application/json", response);
            
            var httpClient = mockHttp.ToHttpClient();
            return new WSPayApiClient(httpClient);
        }
        
        public static IWSPayClient CreateErrorClientWithResponse(string error)
        {
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = new StringContent(error, Encoding.UTF8, "application/json"),
                })
                .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object);
            return new WSPayApiClient(httpClient);
        }
    }
}