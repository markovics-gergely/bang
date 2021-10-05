using Microsoft.Extensions.Configuration;

namespace ApiGateway.API.Extensions
{
    public static class JsonExtension
    {
        public static void AddOcelotJsonFiles(this IConfigurationBuilder config)
        {
            config.AddJsonFile("ocelot.json");
        }
    }
}
