using Microsoft.Extensions.Configuration;
using Moq;
using PostingControlService.Application.Interfaces;
using PostingControlService.Application.Services;
using PostingControlService.Domain.Entities;
using PostingControlService.Domain.Interfaces;
using System.Threading.Tasks;
using Xunit;

namespace PostingControlService.Application.Tests
{
    public class AuthServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly IAuthService _authService;

        public AuthServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _configurationMock = new Mock<IConfiguration>();
            _configurationMock.SetupGet(x => x["Jwt:Key"]).Returns("your-256-bit-secret");
            _authService = new AuthService(_userRepositoryMock.Object, _configurationMock.Object);
        }

        [Fact]
        public async Task Register_ShouldAddUser()
        {
            // Arrange
            var username = "testuser";
            var password = "password";
            _userRepositoryMock.Setup(repo => repo.AddUser(It.IsAny<User>())).ReturnsAsync(true);

            // Act
            var result = await _authService.Register(username, password);

            // Assert
            Assert.True(result);
            _userRepositoryMock.Verify(repo => repo.AddUser(It.Is<User>(u => u.Username == username && u.Password == password)), Times.Once);
        }

        [Fact]
        public async Task Login_ShouldReturnToken()
        {
            // Arrange
            var username = "testuser";
            var password = "password";
            var user = new User { Id = 1, Username = username, Password = password };
            _userRepositoryMock.Setup(repo => repo.GetUser(username, password)).ReturnsAsync(user);

            // Act
            var token = await _authService.Login(username, password);

            // Assert
            Assert.NotNull(token);
        }

        [Fact]
        public async Task Login_InvalidCredentials_ShouldReturnNull()
        {
            // Arrange
            var username = "invaliduser";
            var password = "password";
            _userRepositoryMock.Setup(repo => repo.GetUser(username, password)).ReturnsAsync((User)null);

            // Act
            var token = await _authService.Login(username, password);

            // Assert
            Assert.Null(token);
        }
    }
}