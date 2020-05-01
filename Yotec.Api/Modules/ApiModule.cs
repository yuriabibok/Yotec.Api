using Nancy;
using Yotec.Api.Helpers;
using Yotec.Api.Services;

namespace Yotec.Api.Modules
{
    public class ApiModule : NancyModule
    {
        private readonly IArticleService articleService;

        public ApiModule(IArticleService articleService)
        {
            this.articleService = articleService;

            SetEndpoints();
        }

        private void SetEndpoints()
        {
            Get("/", async _ => SerializeHelper.Serialize(await articleService.CheckAvailabilityAsync()));

            Get("/list/{section}", async _ => SerializeHelper.Serialize(await articleService.GetSectionItemsAsync(_.section)));

            //Get("/list/{section}/first", async _ => SerializeHelper.Serialize(await articleService.GetFirstSectionItem()));

            //Get("/list/{section}/{updatedDate}", async _ => SerializeHelper.Serialize(await articleService.GetSectionItemsByDate()));

            //Get("/article/{shortUrl}", async _ => SerializeHelper.Serialize(await articleService.GetSectionItemByShortUrl()));

            //Get("/group/{section}", async _ => SerializeHelper.Serialize(await articleService.GetSectionItemsGroupedByDate()));
        }
    }
}
