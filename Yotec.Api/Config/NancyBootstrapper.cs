using AutoMapper;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Configuration;
using Nancy.TinyIoc;
using Yotec.Api.Services;

namespace Yotec.Api.Config
{
    public class NancyBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);

            Register(container);
        }

        public override void Configure(INancyEnvironment environment)
        {
            environment.Tracing(false, true);
        }

        private void Register(TinyIoCContainer container)
        {
            RegisterMapper(container);

            container.Register<IArticleService, ArticleService>();
            container.Register<IArticleHttpClient, ArticleHttpClient>();
        }

        private void RegisterMapper(TinyIoCContainer container)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperProfile());
            });

            var mapper = mappingConfig.CreateMapper();
            container.Register(mapper);
        }
    }
}