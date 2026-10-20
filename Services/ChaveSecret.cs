using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Services
{
    public static class ChaveSecret
    {
        public static string Get()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appSettings.json")
                    .Build();
  
            return configuration["Chaves:Secret"];
        }
    }
}
