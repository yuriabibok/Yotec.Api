using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json;
using Yotec.Api.Helpers;
using Yotec.Api.Models;

namespace Yotec.Api.Services
{
    public class ArticleHttpClient : IArticleHttpClient
    {
        private readonly HttpClient client;
        private readonly IMapper mapper;

        public ArticleHttpClient(HttpClient client, IMapper mapper)
        {
            this.client = client;
            this.mapper = mapper;
        }

        public async Task<bool> IsAvailableAsync()
        {
            var response = await client.GetAsync(ArticleUrlBuilder.GetDefaultUrl());

            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<ArticleView>> GetItemsAsync(string section)
        {
            var response = await client.GetAsync(ArticleUrlBuilder.GetSectionUrl(section));
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(
                    $"Error was occured during connection to server. Code: {(int) response.StatusCode}. Reason: {response.ReasonPhrase}");
            }

            var content = await response.Content.ReadAsStringAsync();
            var results = JsonConvert.DeserializeObject<dynamic>(content).results;

            return mapper.Map<IEnumerable<ArticleView>>(results);
        }
    }
}
