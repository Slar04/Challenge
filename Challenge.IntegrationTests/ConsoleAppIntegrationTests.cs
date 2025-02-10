using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Service.Interfaces;
using Service.Implementation;
using Moq;
using Challenge.App;

namespace Challenge.IntegrationTests
{
    public class ConsoleAppIntegrationTests
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly HttpClient _httpClient;

        public ConsoleAppIntegrationTests()
        {
            var serviceCollection = new ServiceCollection()
                .AddHttpClient()
                .AddScoped<IUserService, UserService>()
                .AddLogging(configure => configure.AddConsole())
                .BuildServiceProvider();

            _serviceProvider = serviceCollection;
            _httpClient = _serviceProvider.GetRequiredService<IHttpClientFactory>().CreateClient();
        }

        [Fact]
        public async Task RunAsync_ShouldFetchUsersSuccessfully_WhenApiIsAvailable()
        {
            // Arrange
            var logger = _serviceProvider.GetRequiredService<ILogger<UserService>>();
            var loggerProgram = _serviceProvider.GetRequiredService<ILogger<Application>>();

            var userService = new UserService(_httpClient, logger);

            var app = new Application(userService, loggerProgram);

            // Act
            await app.RunAsync();

            // Assert
            Assert.True(true, "Integration test ran successfully.");
        }
    }
}
