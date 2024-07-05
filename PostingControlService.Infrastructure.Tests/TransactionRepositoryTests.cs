using Microsoft.EntityFrameworkCore;
using PostingControlService.Domain.Entities;
using PostingControlService.Infrastructure.Data;
using PostingControlService.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace PostingControlService.Infrastructure.Tests
{
    public class TransactionRepositoryTests
    {
        private readonly TransactionRepository _repository;
        private readonly TransactionContext _context;

        public TransactionRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<TransactionContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new TransactionContext(options);
            _repository = new TransactionRepository(_context);
        }

        [Fact]
        public async Task AddTransaction_ShouldAddTransaction()
        {
            // Arrange
            var transaction = new Transaction { Type = "credit", Amount = 100, Date = DateTime.UtcNow };

            // Act
            await _repository.AddTransaction(transaction);

            // Assert
            var result = await _context.Transactions.FindAsync(transaction.Id);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetTransactions_ShouldReturnTransactions()
        {
            // Arrange
            var transactions = new List<Transaction>
            {
                new Transaction { Type = "credit", Amount = 100, Date = DateTime.UtcNow },
                new Transaction { Type = "debit", Amount = 50, Date = DateTime.UtcNow }
            };
            _context.Transactions.AddRange(transactions);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetTransactions();

            // Assert
            Assert.Equal(2, result.Count());
        }
    }
}
