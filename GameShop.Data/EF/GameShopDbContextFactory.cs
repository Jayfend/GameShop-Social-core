using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GameShop.Data.EF
{
    public class GameShopDbContextFactory : IDesignTimeDbContextFactory<GameShopDbContext>
    {
        public GameShopDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json")
              .Build();
            var connectionstring = configuration.GetConnectionString("GameShopDatabase");
            var optionsBuilder = new DbContextOptionsBuilder<GameShopDbContext>();
            optionsBuilder.UseSqlServer(connectionstring);

            return new GameShopDbContext(optionsBuilder.Options);
        }
    }
}
