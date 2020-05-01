using Yotec.Api.Config;

namespace Yotec.Api.Helpers
{
    public static class ArticleUrlBuilder
    {
        public static string GetDefaultUrl()
        {
            return GetSectionUrl(AppSettings.DefaultArticleSection);
        }

        public static string GetSectionUrl(string section)
        {
            return string.Format(AppSettings.ArticleBaseUrl, section, AppSettings.ArticleApiAccessKey);
        }
    }
}
