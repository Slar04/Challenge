using Microsoft.Extensions.Logging;
using Service.Interfaces;

namespace Challenge.App
{
    public class Application
    {
        private readonly IUserService _userService;
        private readonly ILogger<Application> _logger;

        public Application(IUserService userService, ILogger<Application> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        public async Task RunAsync()
        {
            _logger.LogInformation("Console application started.");

            try
            {
                var users = await _userService.GetUsersAsync();
                _logger.LogInformation($"Retrieved {users.Count} users.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred: {ex.Message}");
            }
        }
    }
}
