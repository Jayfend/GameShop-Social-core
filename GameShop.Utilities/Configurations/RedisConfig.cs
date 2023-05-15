using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Utilities.Configurations
{
    public class RedisConfig
    {
        public string Configuration { get; set; }
        public long AuthExpiresIn { get; set; }
        public string Prefix { get; set; }
        public string DSMKey { get; set; }


    }
}
