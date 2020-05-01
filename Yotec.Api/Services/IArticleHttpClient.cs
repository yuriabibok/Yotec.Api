using System.Collections.Generic;
using System.Threading.Tasks;
using Yotec.Api.Models;

namespace Yotec.Api.Services
{
    public interface IArticleHttpClient
    {
        //Task<string> GetDate();
        Task<bool> IsAvailableAsync();

        Task<IEnumerable<ArticleView>> GetItemsAsync(string section);
    }
}
