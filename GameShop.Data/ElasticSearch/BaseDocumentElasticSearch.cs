using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Data.ElasticSearch
{
    [ElasticsearchType(IdProperty = nameof(ESId))]
    public class BaseDocumentElasticSearch
    {
        [Text(Index = true)]
        public string ESId { get; set; }
    }
}
