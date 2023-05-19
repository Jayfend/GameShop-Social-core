using Elasticsearch.Net;
using GameShop.Data.ElasticSearch;
using GameShop.Utilities;
using GameShop.Utilities.Configurations;
using GameShop.Utilities.Exceptions;
using GameShop.ViewModels.Catalog.Games;
using GameShop.ViewModels.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static GameShop.Application.Module.ElasticsearchModule;

namespace GameShop.Application.Utilities
{
    public class ElasticSearchUlti : IElasticSearchUlti
    {
        IElasticClient _elasticClient;
        ServiceResolver _serviceResolver;
        readonly ElasticSearchConfig _elasticSearchConfig;

        public ElasticSearchUlti(ServiceResolver serviceResolver
            , IOptions<ElasticSearchConfig> elasticSearchOption)
        {
            _serviceResolver = serviceResolver;
            _elasticSearchConfig = elasticSearchOption.Value;
        }

        void InitElasticService(string server)
        {
            if (string.IsNullOrWhiteSpace(server))
            {
                server = ElasticServer.Common;
            }

            var service = _serviceResolver(server);
            var serviceProvider = service.BuildServiceProvider();
            _elasticClient = serviceProvider.GetService<IElasticClient>();
        }

        public async Task<PagedResult<T>> SearchAsync<T>(
            string server,
            string indexName,
            SearchDescriptor<T> query,
            int pageIndex,
            int pageSize,
            string[] includeFields = null,
            string[] highFields = null)
            where T : class
        {

            InitElasticService(server);

            if (query == null)
            {
                query = new SearchDescriptor<T>();
            }

            query.Index(indexName);

            var highlight = new HighlightDescriptor<T>();

            var isHigh = highFields != null && highFields.Length > 0;

            var hfs = new List<Func<HighlightFieldDescriptor<T>, IHighlightField>>();

            if (isHigh)
            {
                foreach (var s in highFields)
                {
                    hfs.Add(f => f.Field(s));
                }

                highlight.Fields(hfs.ToArray());
                query.Highlight(h => highlight);
            }


            if (includeFields != null)
                query.Source(ss => ss.Includes(ff => ff.Fields(includeFields.ToArray())));

            List<T> results;
            long totalCount;

            ISearchResponse<T> searchResponse;
            if (pageIndex == -1)
            {
                searchResponse = await _elasticClient.SearchAsync<T>(query.Size(_elasticSearchConfig.MaximumRecord).Scroll("1m"));
                results = searchResponse.Documents.ToList();

                while (searchResponse.Documents.Any())
                {
                    searchResponse = await _elasticClient.ScrollAsync<T>("1m", searchResponse.ScrollId);
                    results.AddRange(searchResponse.Documents);
                }
            }
            else if (pageIndex == 1)
            {
                searchResponse = await _elasticClient.SearchAsync<T>(query.Size(pageSize).Scroll("1m"));
                results = searchResponse.Documents.ToList();
            }
            else
            {

                if (pageIndex * pageSize > _elasticSearchConfig.MaximumRecord)
                {
                    searchResponse = await _elasticClient.SearchAsync<T>(query.From(_elasticSearchConfig.MaximumRecord - pageSize).Size(pageSize));
                }
                else
                {
                    searchResponse = await _elasticClient.SearchAsync<T>(query.From((pageIndex - 1) * pageSize).Size(pageSize));
                }

                results = searchResponse.Documents.ToList();
            }

            totalCount = searchResponse.Total;

            return new PagedResult<T>()
            {
                Items = results,
                TotalRecords = totalCount
            };
        }


        public async Task<bool> InitIndex<T>(string indexName, string server = null) where T : BaseDocumentElasticSearch
        {
            InitElasticService(server);

            var response = await _elasticClient.Indices.CreateAsync(indexName,
                    index => index.Map<T>(
                        x => x.AutoMap()
                    ));

            if (response.ServerError != null)
            {
                var status = response.ServerError.Status;
                var errorMessage = response.ServerError.Error.ToString();
                throw new ExceptionCustom(errorMessage);
            }


            return true;
        }


        public async Task<bool> AddAsync<T>(T data, string indexName, string server = null) where T : BaseDocumentElasticSearch
        {
            InitElasticService(server);

            var createResponse = await _elasticClient.IndexAsync(data, i => i.Index(indexName).Refresh(Refresh.True));

            if (createResponse.ServerError != null)
            {
                var status = createResponse.ServerError.Status;
                var errorMessage = createResponse.ServerError.Error.ToString();
                throw new ExceptionCustom(errorMessage);
            }


            return createResponse.Result == Result.Created;
        }



        public async Task<bool> AddRangeAsync<T>(List<T> dataList, string indexName, string server = null) where T : BaseDocumentElasticSearch
        {
            try
            {
                if (dataList.Any() == false)
                {
                    return true;
                }

                InitElasticService(server);

                await Task.Run(() =>
                {
                    BulkAllObservable<T> bulkAllNotificationsObservable = _elasticClient.BulkAll(dataList, b => b
                                                                                                               .Index(indexName)
                                                                                                               .BackOffTime(TimeSpan.FromSeconds(5))
                                                                                                               .BackOffRetries(3)
                                                                                                               .RefreshOnCompleted()
                                                                                                               .MaxDegreeOfParallelism(5)
                                                                                                               .Size(5000)
                                                                                                           );
                    ManualResetEvent waitHandle = new ManualResetEvent(false);
                    Exception ex = null;

                    BulkAllObserver bulkAllObserver = new BulkAllObserver(
                            onNext: bulkAllResponse =>
                            {
                            },
                            onError: exception =>
                            {
                                ex = exception;
                                waitHandle.Set();
                            },
                            onCompleted: () =>
                            {
                                waitHandle.Set();
                            });

                    bulkAllNotificationsObservable.Subscribe(bulkAllObserver);
                    waitHandle.WaitOne();

                    if (ex != null)
                    {
                        throw ex;
                    }
                });

                return true;

            }
            catch (Exception ex)
            {
                //_logger.LogException(ex);
                return false;
            }
        }
        public async Task<bool> UpdateAsync<T>(T data, string indexName, string server = null) where T : BaseDocumentElasticSearch
        {
            InitElasticService(server);

            var response = await _elasticClient
            .UpdateAsync<T>(data.ESId, u => u.Index(indexName)
            .Doc(data)
            .Refresh(Refresh.True));

            if (response.ServerError != null)
            {
                var status = response.ServerError.Status;
                var errorMessage = response.ServerError.Error.ToString();
                //_logger.LogError(errorMessage);
                throw new ExceptionCustom(errorMessage);
            }

            return response.Result == Result.Updated;

        }

        public async Task<bool> DeleteAsync<T>(string esId, string indexName, string server = null) where T : BaseDocumentElasticSearch
        {
            InitElasticService(server);

            var response = await _elasticClient.DeleteAsync<T>(esId, u => u.Index(indexName));

            if (response.ServerError != null)
            {
                var status = response.ServerError.Status;
                var errorMessage = response.ServerError.Error.ToString();
                //_logger.LogError(errorMessage);
                throw new ExceptionCustom(errorMessage);
            }

            return response.Result == Result.Deleted;
        }

        public async Task<bool> DeleteByIdsAsync<T>(List<string> esId, string indexName, string server = null) where T : BaseDocumentElasticSearch
        {
            InitElasticService(server);
            if (esId.Any() == false)
            {
                return true;
            }

            DeleteByQueryRequest<T> query = new DeleteByQueryRequest<T>(indexName);
            List<QueryContainer> must = new List<QueryContainer>();
            BoolQuery boolQuery = new BoolQuery()
            {
                Must = new QueryContainer[] { new MatchAllQuery() }
            };

            must.Add(new TermsQuery { Field = "eSId.keyword", Terms = esId });

            boolQuery.Must = must;
            query.Query = boolQuery;
            query.Refresh = true;

            var response = await _elasticClient.DeleteByQueryAsync(query);

            if (response.ServerError != null)
            {
                var status = response.ServerError.Status;
                var errorMessage = response.ServerError.Error.ToString();
                // _logger.LogError(errorMessage);
                throw new ExceptionCustom(errorMessage);
            }

            return true;
        }

        public async Task<bool> DeleteAllDataAsync<T>(string indexName, string server = null) where T : BaseDocumentElasticSearch
        {
            InitElasticService(server);
            await _elasticClient.DeleteByQueryAsync<T>(del => del.Index(indexName)
                .Query(q => q.QueryString(qs => qs.Query("*"))).Refresh(true)

            );

            return true;
        }

        public async Task<List<GameElasticModel>> SearchSuggestion(List<string> keyWords, string indexName, string server = null)
        {
            InitElasticService(server);
            List<GameElasticModel> result = new List<GameElasticModel>();
            foreach (var keyWord in keyWords)
            {
                var response = await _elasticClient.SearchAsync<GameElasticModel>(s => s
         .Suggest(su => su
             .Completion("genreSuggest", cs => cs
                 .Field(f => f.GenreSuggest)
                 .Prefix(keyWord)
                 .Fuzzy(f => f
                     .Fuzziness(Fuzziness.Auto)
                 )
                 .Size(5)
                 .SkipDuplicates(false)
             )
         )
     );

                var suggestions = response.Suggest["genreSuggest"]
                    .SelectMany(t => t.Options)
                    .Select(o => o.Source)
                    .Distinct()
                    .ToList();
                result = result.Concat(suggestions).Distinct()
                     .GroupBy(p => p.Id)
                  .Select(g => g.First())
                  .ToList();

            }

            return result.ToList();
        }
    }
}
