using Domain.Entities;
using Nest;

namespace WebApi.Repositories
{
    public class ElasticSearchRepository : ISearchRepository<Permission>
    {
        private readonly IElasticClient elasticSearchCliente;

        public ElasticSearchRepository()
        {
            // create a single node
            var node = new Uri("http://elasticsearch-repository:9200");

            // settings
            var settings = new ConnectionSettings(node);

            // default index of this client
            settings.DefaultIndex("permissions");
            elasticSearchCliente = new ElasticClient(settings);
        }

        public async Task Add(IEnumerable<Permission> items)
        {
            var response = await elasticSearchCliente.IndexManyAsync(items);
        }

        public async Task<IEnumerable<Permission>> Search(string query)
        {
            // return 20 results, searching by name or description and order by id asc
            var request = new SearchRequest
            {
                From = 0,
                Size = 20,
                Sort = new List<ISort>() {
                    new SortField
                    {
                        Field = "id",
                        Order = Nest.SortOrder.Ascending

                    }
                },
                Query = new MatchQuery { Field = "name", Query = query }
            };

            var response = await elasticSearchCliente.SearchAsync<Permission>(request);

            // return all docs
            return response.Documents;
        }
    }
}
