using Microsoft.EntityFrameworkCore;
using PostingControlService.Domain.Entities;
using PostingControlService.Infrastructure.Data;
using PostingControlService.Infrastructure.Repositories;
using System.Threading.Tasks;
using Xunit;

namespace PostingControlService.Infrastructure.Tests
{
    public class UserRepositoryTests
    {
        private readonly UserRepository _repository;
        private readonly TransactionContext _context;

        public UserRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<TransactionContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new TransactionContext(options);
            _repository = new UserRepository(_context);
        }

        [Fact]
        public async Task AddUser_ShouldAddUser()
        {
            // Arrange
            var user = new User { Username = "testuser", Password = "password" };

            // Act
            var result = await _repository.AddUser(user);

            // Assert
            Assert.True(result);
            var dbUser = await _context.Users.FindAsync(user.Id);
            Assert.NotNull(dbUser);
        }

        [Fact]
        public async Task GetUser_ShouldReturnUser()
        {
            // Arrange
            var user = new User { Username = "testuser", Password = "password" };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetUser(user.Username, user.Password);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Username, result.Username);
        }
    }
}