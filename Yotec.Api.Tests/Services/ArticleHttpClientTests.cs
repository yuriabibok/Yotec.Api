using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using Yotec.Api.Models;
using Yotec.Api.Services;

namespace Yotec.Api.Tests.Services
{
    [TestFixture]
    public class ArticleHttpClientTests
    {
        [Test]
        public async Task IsAvailableAsync_Available_ReturnTrue()
        {
            var httpClient = MockHttpClient(HttpStatusCode.OK, "");
            var mapperMock = new Mock<IMapper>();
            var articleClient = new ArticleHttpClient(httpClient, mapperMock.Object);

            var result = await articleClient.IsAvailableAsync();

            Assert.IsTrue(result);
        }

        [Test]
        public async Task IsAvailableAsync_NotAvailable_ReturnFalse()
        {
            var httpClient = MockHttpClient(HttpStatusCode.ServiceUnavailable, "");
            var mapperMock = new Mock<IMapper>();
            var articleClient = new ArticleHttpClient(httpClient, mapperMock.Object);

            var result = await articleClient.IsAvailableAsync();

            Assert.IsFalse(result);
        }

        [Test]
        public async Task GetItemsAsync_Valid_ReturnResponse()
        {
            var content =
                "{\"results\":[{\"title\":\"Some title\",\"updated_date\":\"2020-05-02T06:40:13-04:00\",\"short_url\":\"https://nyti.ms/dfrty\"}]}";
            var expectedResult = new List<ArticleView>
            {
                new ArticleView
                {
                    Heading = "Some title",
                    Updated = DateTime.Parse("2020-05-02T06:40:13-04:00"),
                    Link = "https://nyti.ms/dfrty"
                }
            };
            var httpClient = MockHttpClient(HttpStatusCode.OK, content);
            var mapperMock = new Mock<IMapper>();
            mapperMock
                .Setup(x => x.Map<IEnumerable<ArticleView>>(It.IsAny<object>()))
                .Returns(expectedResult);
            var articleClient = new ArticleHttpClient(httpClient, mapperMock.Object);

            var result = await articleClient.GetItemsAsync("section");

            Assert.AreEqual(result, expectedResult);
        }

        [Test]
        public void GetItemsAsync_ServerIsNotAvailable_ThrowException()
        {
            var httpClient = MockHttpClient(HttpStatusCode.ServiceUnavailable, "", "Something went wrong");
            var mapperMock = new Mock<IMapper>();
            var articleClient = new ArticleHttpClient(httpClient, mapperMock.Object);

            Assert.ThrowsAsync<Exception>(async () =>
            {
                await articleClient.GetItemsAsync("section");
            }, "Error was occured during connection to server. Code: 503. Reason: Something went wrong");
        }

        private HttpClient MockHttpClient(HttpStatusCode code, string content, string reason = null)
        {
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = code,
                    Content = new StringContent(content),
                    ReasonPhrase = reason
                })
                .Verifiable();

            return new HttpClient(handlerMock.Object);
        }
    }
}
