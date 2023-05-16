using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Utilities.Configurations
{
    public class ElasticServer
    {
        public const string Common = "Common";
    }

    public class ElasticSearchConfig
    {
        public int MaximumRecord { get; set; }
        public int TimeoutSecond { get; set; }
        public ElasticSearch_CommonConfig Common { get; set; }

    }


    public class ElasticSearchModelConfig
    {
        public List<string> ConnectionString { get; set; }
        public string ApiKey { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class ElasticSearch_CommonConfig : ElasticSearchModelConfig
    {
        public string GameIndex { get; set; }

    }

}
