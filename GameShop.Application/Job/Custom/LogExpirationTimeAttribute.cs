using Hangfire.Common;
using Hangfire.States;
using Hangfire.Storage;
using System;

namespace FRT.MasterDataCore.Application.Job
{
    public class LogExpirationTimeAttribute : JobFilterAttribute, IApplyStateFilter
    {
        double _expirationTimeHours;
        public LogExpirationTimeAttribute(double expirationTimeHours = 168)
        {
            _expirationTimeHours = expirationTimeHours;
        }

        public void OnStateApplied(ApplyStateContext context, IWriteOnlyTransaction transaction)
        {
            context.JobExpirationTimeout = TimeSpan.FromHours(_expirationTimeHours);
        }

        public void OnStateUnapplied(ApplyStateContext context, IWriteOnlyTransaction transaction)
        {
        }
    }
}
