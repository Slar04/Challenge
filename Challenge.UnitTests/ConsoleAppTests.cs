using Moq;
using Microsoft.Extensions.Logging;
using Service.Interfaces;
using Data.Model;
using Challenge.App;

namespace Challenge.UnitTests
{
    public class ConsoleAppTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly Mock<ILogger<Application>> _loggerMock;

        public ConsoleAppTests()
        {
            _userServiceMock = new Mock<IUserService>();
            _loggerMock = new Mock<ILogger<Application>>();
        }

        [Fact]
        public async Task RunAsync_ShouldLogInformation_WhenUsersAreFetchedSuccessfully()
        {
            // Arrange
            _userServiceMock.Setup(s => s.GetUsersAsync()).ReturnsAsync(new List<User> { new User() });
            var logger = _loggerMock.Object;
            var application = new Application(_userServiceMock.Object, logger);

            // Act
            await application.RunAsync();
        }

        [Fact]
        public async Task RunAsync_ShouldLogError_WhenExceptionOccurs()
        {
            // Arrange
            _userServiceMock.Setup(s => s.GetUsersAsync()).ThrowsAsync(new Exception("An error occurred:"));

            var logger = _loggerMock.Object;

            var application = new Application(_userServiceMock.Object, logger);

            // Act
            await application.RunAsync();
        }
    }
}
