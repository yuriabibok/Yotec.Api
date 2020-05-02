using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Yotec.Api.Helpers;
using Yotec.Api.Models;
using Yotec.Api.Services;

namespace Yotec.Api.Tests.Services
{
    [TestFixture]
    public class ArticleServiceTests
    {
        private readonly IEnumerable<ArticleView> articles = new List<ArticleView>
        {
            new ArticleView
            {
                Heading = "Title1",
                Updated = DateTime.Parse("2020-05-01T06:40:13-04:00"),
                Link = "https://nyti.ms/title1"
            },
            new ArticleView
            {
                Heading = "Title2",
                Updated = DateTime.Parse("2020-05-02T06:40:13-04:00"),
                Link = "https://nyti.ms/title2"
            },
            new ArticleView
            {
                Heading = "Title3",
                Updated = DateTime.Parse("2020-05-03T06:40:13-04:00"),
                Link = "https://nyti.ms/title3"
            },
            new ArticleView
            {
                Heading = "Title4",
                Updated = DateTime.Parse("2020-05-01T06:40:13-04:00"),
                Link = "https://nyti.ms/title4"
            },
            new ArticleView
            {
                Heading = "Title5",
                Updated = DateTime.Parse("2020-05-01T06:40:13-04:00"),
                Link = "https://nyti.ms/title5"
            }
        };

        [Test]
        public async Task CheckAvailabilityAsync_ServerAvailable_ReturnSuccessString()
        {
            var articleHttpClientMock = new Mock<IArticleHttpClient>();
            articleHttpClientMock.Setup(x => x.IsAvailableAsync()).ReturnsAsync(true);
            var articleService = new ArticleService(articleHttpClientMock.Object);

            var result = await articleService.CheckAvailabilityAsync();

            Assert.AreEqual("Service is available.", result);
        }

        [Test]
        public async Task CheckAvailabilityAsync_ServerNotAvailable_ReturnErrorString()
        {
            var articleHttpClientMock = new Mock<IArticleHttpClient>();
            articleHttpClientMock.Setup(x => x.IsAvailableAsync()).ReturnsAsync(false);
            var articleService = new ArticleService(articleHttpClientMock.Object);

            var result = await articleService.CheckAvailabilityAsync();

            Assert.AreEqual("Service is not available. Please contact to administrator.", result);
        }

        [Test]
        public void GetSectionItemsAsync_SectionArgNull_ThrowException()
        {
            var articleHttpClientMock = new Mock<IArticleHttpClient>();
            var articleService = new ArticleService(articleHttpClientMock.Object);

            Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await articleService.GetSectionItemsAsync(null);
            });
        }

        [Test]
        public async Task GetSectionItemsAsync_Valid_ResurnResponse()
        {
            var articleHttpClientMock = new Mock<IArticleHttpClient>();
            articleHttpClientMock.Setup(x => x.GetItemsAsync("section")).ReturnsAsync(articles);
            var articleService = new ArticleService(articleHttpClientMock.Object);

            var response = await articleService.GetSectionItemsAsync("section");

            Assert.AreEqual(articles, response);
        }

        [Test]
        public async Task GetFirstSectionItemAsync_Valid_ResurnResponse()
        {
            var expectedResult = new ArticleView
            {
                Heading = "Title1",
                Updated = DateTime.Parse("2020-05-01T06:40:13-04:00"),
                Link = "https://nyti.ms/title1"
            };
            var articleHttpClientMock = new Mock<IArticleHttpClient>();
            articleHttpClientMock.Setup(x => x.GetItemsAsync("section")).ReturnsAsync(articles);
            var articleService = new ArticleService(articleHttpClientMock.Object);

            var response = await articleService.GetFirstSectionItemAsync("section");

            Assert.AreEqual(SerializeHelper.Serialize(expectedResult), SerializeHelper.Serialize(response));
        }

        [Test]
        public async Task GetSectionItemsByDateAsync_Valid_ResurnResponse()
        {
            var expectedResult = new List<ArticleView>
            {
                new ArticleView
                {
                    Heading = "Title2",
                    Updated = DateTime.Parse("2020-05-02T06:40:13-04:00"),
                    Link = "https://nyti.ms/title2"
                }
            };
            var articleHttpClientMock = new Mock<IArticleHttpClient>();
            articleHttpClientMock.Setup(x => x.GetItemsAsync("section")).ReturnsAsync(articles);
            var articleService = new ArticleService(articleHttpClientMock.Object);

            var response = await articleService.GetSectionItemsByDateAsync("section", "2020-05-02");

            Assert.AreEqual(SerializeHelper.Serialize(expectedResult), SerializeHelper.Serialize(response));
        }

        [Test]
        public async Task GetSectionItemByShortUrlAsync_Valid_ResurnResponse()
        {
            var expectedResult = new ArticleView
            {
                Heading = "Title3",
                Updated = DateTime.Parse("2020-05-03T06:40:13-04:00"),
                Link = "https://nyti.ms/title3"
            };
            var articleHttpClientMock = new Mock<IArticleHttpClient>();
            articleHttpClientMock.Setup(x => x.GetItemsAsync("section")).ReturnsAsync(articles);
            var articleService = new ArticleService(articleHttpClientMock.Object);

            var response = await articleService.GetSectionItemByShortUrlAsync("title3");

            Assert.AreEqual(SerializeHelper.Serialize(expectedResult), SerializeHelper.Serialize(response));
        }

        [Test]
        public async Task GetSectionItemsGroupedByDateAsync_Valid_ResurnResponse()
        {
            var expectedResult = new List<ArticleGroupByDateView>
            {
                new ArticleGroupByDateView
                {
                    Date = "2020-05-01",
                    Total = 3
                },
                new ArticleGroupByDateView
                {
                    Date = "2020-05-02",
                    Total = 1
                },
                new ArticleGroupByDateView
                {
                    Date = "2020-05-03",
                    Total = 1
                },
            };
            var articleHttpClientMock = new Mock<IArticleHttpClient>();
            articleHttpClientMock.Setup(x => x.GetItemsAsync("section")).ReturnsAsync(articles);
            var articleService = new ArticleService(articleHttpClientMock.Object);

            var response = await articleService.GetSectionItemsGroupedByDateAsync("section");

            Assert.AreEqual(SerializeHelper.Serialize(expectedResult), SerializeHelper.Serialize(response));
        }
    }
}
