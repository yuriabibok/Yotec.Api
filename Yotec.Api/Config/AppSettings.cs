using System.Configuration;

namespace Yotec.Api.Config
{
    public class AppSettings
    {
        public static string ArticleBaseUrl => ConfigurationManager.AppSettings[nameof(ArticleBaseUrl)];

        public static string DefaultArticleSection => ConfigurationManager.AppSettings[nameof(DefaultArticleSection)];

        public static string ArticleApiAccessKey => ConfigurationManager.AppSettings[nameof(ArticleApiAccessKey)];
    }
}
