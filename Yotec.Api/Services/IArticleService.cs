using System.Collections.Generic;
using System.Threading.Tasks;
using Yotec.Api.Models;

namespace Yotec.Api.Services
{
    public interface IArticleService
    {
        Task<string> CheckAvailabilityAsync();

        Task<IEnumerable<ArticleView>> GetSectionItemsAsync(string section);
    }
}