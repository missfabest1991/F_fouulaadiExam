using Microsoft.Extensions.Configuration;

namespace MVCLibrary.Utilites
{
    public static class ConfigurationHelper
    {
        public static IConfiguration Config { get; set; }
        public static void InitConfig(IConfiguration configuration) => Config = configuration;
    }
}