using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Helpers
{
    public class AppSettings
    {
        public string ConnectionString = string.Empty;
        public string Secret = string.Empty;
        public AppSettings() 
        {
            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);
            var root = configurationBuilder.Build();
            Secret = root.GetSection("AppSettings").GetSection("Secret").Value??string.Empty;
        }
        public string getTheRightConnection()
        {
            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);
            var root = configurationBuilder.Build();
            string conn = root.GetSection("AppSettings").GetSection("ConnectionString").Value??string.Empty;
            return conn;
        }
    }
}
