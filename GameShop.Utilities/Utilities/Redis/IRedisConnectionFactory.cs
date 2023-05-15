using StackExchange.Redis;

namespace FRT.DataReporting.Application.Utilities
{
    public interface IRedisConnectionFactory
    {
        IDatabase GetDatabase(string connectionString);
    }
}
