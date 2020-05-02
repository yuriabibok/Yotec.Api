using NUnit.Framework;
using Yotec.Api.Helpers;

namespace Yotec.Api.Tests.Helpers
{
    [TestFixture]
    public class ArticleUrlBuilderTests
    {
        [Test]
        public void GetDefaultUrl_Valid_ReturnUrl()
        {
            var url = ArticleUrlBuilder.GetDefaultUrl();

            Assert.AreEqual(url, "https://baseurl/section.json?api-key=key");
        }

        [Test]
        public void GetSectionUrl_Valid_ReturnUrl()
        {
            var url = ArticleUrlBuilder.GetSectionUrl("section");

            Assert.AreEqual(url, "https://baseurl/section.json?api-key=key");
        }
    }
}
