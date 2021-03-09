using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    class AppConfiguration
    {
        public AppConfiguration()
        {
            var configBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configBuilder.AddJsonFile(path);
            var root = configBuilder.Build();
            var appSetting = root.GetSection("ConnectionStrings:MoodyConnection");
            sqlConnectionString = appSetting.Value;

        }
        public string sqlConnectionString { get; set; }
    }
}
