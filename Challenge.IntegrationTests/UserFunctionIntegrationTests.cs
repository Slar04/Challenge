using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Xunit;
using Newtonsoft.Json;
using Challenge.Functions;

namespace Challenge.IntegrationTests
{
    public class UserFunctionIntegrationTests
    {
        private readonly HttpClient _client;

        public UserFunctionIntegrationTests()
        {
            var host = new HostBuilder()
                .ConfigureWebHost(webHost =>
                {
                    webHost.UseTestServer();
                    webHost.UseStartup<Startup>();
                })
                .Build();

            _client = host.GetTestClient();
        }

        [Fact]
        public async Task Run_Should_Return_Success_When_Valid_Request()
        {
            // Arrange
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/UserFunction");

            // Act
            var response = await _client.SendAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Hello, John", responseString);
        }
    }
}