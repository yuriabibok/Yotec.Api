using System.Collections.Generic;
using System.Threading.Tasks;
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
            var items = await articleHttpClient.GetItemsAsync(section);

            return items;
        }
    }
}
