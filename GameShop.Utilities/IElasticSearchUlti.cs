using Abp.Application.Services.Dto;
using GameShop.Data.ElasticSearch;
using GameShop.ViewModels.Common;
using Nest;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameShop.Utilities
{
    public interface IElasticSearchUlti
    {
        Task<PagedResult<T>> SearchAsync<T>(
          string server,
          string indexName,
          SearchDescriptor<T> query,
          int pageIndex,
          int pageSize,
          string[] includeFields = null,
          string[] highFields = null)
          where T : class;

        Task<bool> AddAsync<T>(T data, string indexName, string server = null) where T : BaseDocumentElasticSearch;
        Task<bool> AddRangeAsync<T>(List<T> dataList, string indexName, string server = null) where T : BaseDocumentElasticSearch;
        Task<bool> UpdateAsync<T>(T data, string indexName, string server = null) where T : BaseDocumentElasticSearch;
        Task<bool> DeleteAsync<T>(string esId, string indexName, string server = null) where T : BaseDocumentElasticSearch;
        Task<bool> DeleteByIdsAsync<T>(List<string> esId, string indexName, string server = null) where T : BaseDocumentElasticSearch;
        Task<bool> DeleteAllDataAsync<T>(string indexName, string server = null) where T : BaseDocumentElasticSearch;

        Task<bool> InitIndex<T>(string indexName, string server = null) where T : BaseDocumentElasticSearch;

    }
}
