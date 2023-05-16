using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Utilities.Redis
{
    public interface IRedisConnectionFactory
    {
        IDatabase GetDatabase(string connectionString);
    }
}
