using Elasticsearch.Net;
using GameShop.Utilities.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using System;
using System.Collections.Generic;

namespace GameShop.Application.Module
{
    public static class ElasticsearchModule
    {
        public delegate IServiceCollection ServiceResolver(string key);
        static double _timeoutSecond;


        public static void AddElasticsearch(this IServiceCollection services, IConfiguration configuration)
        {
            ElasticSearchConfig elastic = new ElasticSearchConfig();
            configuration.GetSection("ElasticSearch").Bind(elastic);
            _timeoutSecond = elastic.TimeoutSecond;


            var commonClient = CreateElasticClient(elastic.Common.ConnectionString, elastic.Common.GameIndex, elastic.Common.ApiKey, elastic.Common.Username, elastic.Common.Password);


            services.AddTransient<ServiceResolver>(serviceProvider => key =>
            {
                if (key == ElasticServer.Common)
                {
                    services.AddSingleton<IElasticClient>(commonClient);
                }

                return services;
            });
        }

        static ElasticClient CreateElasticClient(List<string> connections, string defaultIndex, string apiKey, string userName, string password)
        {


            List<Uri> arrUris = new List<Uri>();
            foreach (var connection in connections)
            {
                arrUris.Add(new Uri(connection));
            }

            var pool = new StaticConnectionPool(arrUris);
            var settings = new ConnectionSettings(pool)

                .DefaultIndex(defaultIndex)
                .EnableDebugMode()
                .PrettyJson()
                .RequestTimeout(TimeSpan.FromSeconds(_timeoutSecond))
                .DefaultMappingFor<object>(m => m.IdProperty("code"));
            if (!string.IsNullOrWhiteSpace(apiKey))
            {
                settings.ServerCertificateValidationCallback((o, certificate, chain, errors) => true);
                settings.ServerCertificateValidationCallback(CertificateValidations.AllowAll);
                settings.ApiKeyAuthentication(apiKey, apiKey);
            }
            if (!string.IsNullOrWhiteSpace(userName))
            {
                settings.ServerCertificateValidationCallback((o, certificate, chain, errors) => true);
                settings.ServerCertificateValidationCallback(CertificateValidations.AllowAll);
                settings.BasicAuthentication(userName, password);
            }
            var client = new ElasticClient(settings);

            return client;
        }
    }
}
