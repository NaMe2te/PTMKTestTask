using Microsoft.Extensions.Configuration;

namespace PTMKTest.Console.Configurations;

public class AppConfig
{
    private IConfigurationRoot _configuration;

    public AppConfig()
    {
        _configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(@"C:\Users\vlad\Desktop\Веб\с#\PTMKTest\PTMKTest.Console\appsettings.json")
            .Build();
    }

    public string GetConnectionString()
    {
        return _configuration.GetConnectionString("СonnectionString");
    }
}