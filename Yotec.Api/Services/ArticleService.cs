using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yotec.Api.Config;
using Yotec.Api.Helpers;
using Yotec.Api.Models;

namespace Yotec.Api.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleHttpClient articleHttpClient;

        public ArticleService(IArticleHttpClient articleHttpClient)
        {
            this.articleHttpClient = articleHttpClient;
        }

        public async Task<string> CheckAvailabilityAsync()
        {
            var isAvailable = await articleHttpClient.IsAvailableAsync();

            return isAvailable
                ? "Service is available."
                : "Service is not available. Please contact to administrator.";
        }

        public async Task<IEnumerable<ArticleView>> GetSectionItemsAsync(string section)
        {
            Contract.NotNull(section, nameof(section));

            var items = await articleHttpClient.GetItemsAsync(section);

            return items;
        }

        public async Task<ArticleView> GetFirstSectionItemAsync(string section)
        {
            Contract.NotNull(section, nameof(section));

            var items = await articleHttpClient.GetItemsAsync(section);

            return items.FirstOrDefault();
        }

        public async Task<IEnumerable<ArticleView>> GetSectionItemsByDateAsync(string section, string updatedDate)
        {
            Contract.NotNull(section, nameof(section));
            Contract.NotNull(section, nameof(updatedDate));

            var items = await articleHttpClient.GetItemsAsync(section);

            return items.Where(x => x.Updated.ToString("yyyy-MM-dd") == updatedDate);
        }

        public async Task<ArticleView> GetSectionItemByShortUrlAsync(string shortUrl)
        {
            Contract.NotNull(shortUrl, nameof(shortUrl));

            var items = await articleHttpClient.GetItemsAsync(AppSettings.DefaultArticleSection);

            return items.FirstOrDefault(x => x.Link.ToLower().Contains(shortUrl.ToLower()));
        }

        public async Task<IEnumerable<ArticleGroupByDateView>> GetSectionItemsGroupedByDateAsync(string section)
        {
            Contract.NotNull(section, nameof(section));

            var items = await articleHttpClient.GetItemsAsync(section);

            return items.GroupBy(x => x.Updated.Date).Select(x => new ArticleGroupByDateView
            {
                Date = x.Key.ToString("yyyy-MM-dd"),
                Total = x.Count()
            });
        }
    }
}
