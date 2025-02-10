using Challenge.App;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Service.Implementation;
using Service.Interfaces;

class Program
{
    public static async Task Main(string[] args)
    {
        // Set up Dependency Injection
        var serviceProvider = new ServiceCollection()
            .AddHttpClient()
            .AddScoped<IUserService, UserService>()
            .AddLogging(configure => configure
                            .AddConsole()
                            .SetMinimumLevel(LogLevel.Information))
            .BuildServiceProvider();

        // Get services
        var userService = serviceProvider.GetRequiredService<IUserService>();
        var logger = serviceProvider.GetRequiredService<ILogger<Application>>();

        var builder = new ConfigurationBuilder();
        BuildConfig(builder);

        IConfiguration config = builder.Build();

        var app = new Application(userService, logger);
        await app.RunAsync();
    }

    static void BuildConfig(ConfigurationBuilder builder)
    {
        builder.SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true);
    }
}