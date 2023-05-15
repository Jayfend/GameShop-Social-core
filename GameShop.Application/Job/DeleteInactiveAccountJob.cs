using FRT.MasterDataCore.Application.Job;
using GameShop.Application.Services.Publishers;
using Hangfire.Server;
using Hangfire;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GameShop.Application.System.Users;
using Hangfire.Console;
using Hangfire.RecurringJobExtensions;

namespace GameShop.Application.Job
{
    public class DeleteInactiveAccountJob : IRecurringJob
    {
        readonly IBackgroundJobClient _backgroundJobClient;
    readonly IUserService _userService;
    public DeleteInactiveAccountJob(IBackgroundJobClient backgroundJobClient, IUserService userService)
    {
        _backgroundJobClient = backgroundJobClient;
        _userService = userService;


    }
    [LogExpirationTimeAttribute(0.01)]
    public void Execute(PerformContext context)
    {
        _backgroundJobClient.Enqueue(() => RunAsync(null));
    }
    public async Task RunAsync(PerformContext context)
    {
        var result = await _userService.DeleteInactiveAccount();

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
