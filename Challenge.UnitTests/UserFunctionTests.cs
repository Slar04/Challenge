using Challenge.Functions;
using Data.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Service.Interfaces;

namespace Challenge.UnitTests
{
    public class UserFunctionTests
    {
        private readonly Mock<IUserService> _mockUserService;
        private readonly Mock<ILogger<UserFunction>> _mockLogger;

        public UserFunctionTests()
        {
            _mockUserService = new Mock<IUserService>();
            _mockLogger = new Mock<ILogger<UserFunction>>();
        }

        [Fact]
        public async Task Run_Should_Return_Success_When_Users_Are_Fetched()
        {
            // Arrange
            var userFunction = new UserFunction(_mockUserService.Object);
            var request = new DefaultHttpContext().Request;
            var logger = _mockLogger.Object;

            var users = new List<User> { new User { id = 1, name = "Esteban Rosales" } };

            _mockUserService.Setup(us => us.GetUsersAsync()).ReturnsAsync(users);

            // Act
            var result = await userFunction.Run(request, logger);

            // Assert
            var okResult = Assert.IsType<ActionResult<List<User>>>(result);
        }

        [Fact]
        public async Task Run_Should_Return_Error_When_Exception_Occurs()
        {
            // Arrange
            var userFunction = new UserFunction(_mockUserService.Object);
            var request = new DefaultHttpContext().Request;
            var logger = _mockLogger.Object;

            _mockUserService.Setup(us => us.GetUsersAsync()).ThrowsAsync(new Exception("Something went wrong"));

            // Act
            var result = await userFunction.Run(request, logger);
        }
    }
}