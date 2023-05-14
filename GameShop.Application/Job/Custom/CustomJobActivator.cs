using Hangfire;
using System;

namespace FRT.MasterDataCore.Application.Job
{
    public class CustomJobActivator : JobActivator
    {
        private readonly IServiceProvider _serviceProvider;

        public CustomJobActivator(IServiceProvider serviceProvider)
        {
            // Service provider from IApplicationBuilder.ApplicationServices.
            _serviceProvider = serviceProvider;
        }

        public override object ActivateJob(Type type)
        {
            var implementation = _serviceProvider.GetService(type);
            return implementation;
        }
    }
}
