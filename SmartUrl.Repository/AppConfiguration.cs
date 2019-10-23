using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUrl.Repository
{
    public static class AppConfiguration
    {
        private static IConfiguration currentConfig;

        public static void SetConfig(IConfiguration configuration)
        {
            currentConfig = configuration;
        }

        public static string GetConfiguration(string configKey)
        {
            try
            {
                return currentConfig.GetConnectionString(configKey);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

    }
}
