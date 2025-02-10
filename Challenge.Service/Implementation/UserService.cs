using Data.Model;
using Service.Interfaces;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

namespace Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<UserService> _logger;

        public UserService(HttpClient httpClient, ILogger<UserService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            _logger.LogInformation("Requesting users from the external API.");

            try
            {
                var response = await _httpClient.GetStringAsync("https://jsonplaceholder.typicode.com/users");

                var users = JsonConvert.DeserializeObject<List<User>>(response);

                if (users == null || !users.Any<User>())
                {
                    _logger.LogWarning("No users found in the response.");
                }

                _logger.LogInformation("Successfully retrieved users.");
                return users;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while fetching users: {ex.Message}");
                throw new ApplicationException("Error fetching users.", ex);
            }
        }
    }
}
