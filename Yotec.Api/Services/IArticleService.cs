using System.Collections.Generic;
using System.Threading.Tasks;
using Yotec.Api.Models;

namespace Yotec.Api.Services
{
    public interface IArticleService
    {
        Task<string> CheckAvailabilityAsync();

        Task<IEnumerable<ArticleView>> GetSectionItemsAsync(string section);

        Task<ArticleView> GetFirstSectionItemAsync(string section);

        Task<IEnumerable<ArticleView>> GetSectionItemsByDateAsync(string section, string updatedDate);

        Task<ArticleView> GetSectionItemByShortUrlAsync(string shortUrl);

        Task<IEnumerable<ArticleGroupByDateView>> GetSectionItemsGroupedByDateAsync(string section);
    }
}