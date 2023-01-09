using N5Challenge.Transverse.Dto;
using Nest;

namespace N5Challenge.Application.WebApi.Extensions
{
    public static class ElasticSearchExtensions
    {
        public static void AddElasticsearch(this IServiceCollection services, ElasticSearchOptionsDto options)
        {
            string url = options.ServerUrl;
            string defaultIndex = options.IndexName;

            ConnectionSettings settings = new ConnectionSettings(new Uri(url))
                .PrettyJson()
                .DefaultIndex(defaultIndex);

            AddDefaultMappings(settings);

            ElasticClient client = new ElasticClient(settings);

            services.AddSingleton<IElasticClient>(client);

            CreateIndex(client, defaultIndex);
        }

        private static void AddDefaultMappings(ConnectionSettings settings)
        {
            settings.DefaultMappingFor<PermissionsDto>(m => m);
        }

        private static void CreateIndex(IElasticClient client, string indexName)
        {
            var createIndexResponse = client.Indices.Create(indexName,
                index => index.Map<PermissionsDto>(x => x.AutoMap())
            );
        }
    }

}
