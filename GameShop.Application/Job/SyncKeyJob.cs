using FRT.MasterDataCore.Application.Job;
using GameShop.Application.Services.Publishers;
using Hangfire.RecurringJobExtensions;
using Hangfire.Server;
using Hangfire;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Hangfire.Console;

namespace GameShop.Application.Job
{
    public class SyncKeyJob : IRecurringJob
    {
        readonly IBackgroundJobClient _backgroundJobClient;
        readonly IPublisherService _publisherService;
        public SyncKeyJob(IBackgroundJobClient backgroundJobClient, IPublisherService publisherService)
        {
            _backgroundJobClient = backgroundJobClient;
            _publisherService = publisherService;


        }
        [LogExpirationTimeAttribute(0.01)]
        public void Execute(PerformContext context)
        {
            _backgroundJobClient.Enqueue(() => RunAsync(null));
        }
        public async Task RunAsync(PerformContext context)
        {
            var result = await _publisherService.SyncKey();

            if (result == true)
            {
                context.WriteLine("Sync Thành Công");
            }
            else
            {
                context.WriteLine("Sync Thất Bại");
            }

        }
    }
}
