using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PostingControlService.Application.Interfaces;
using PostingControlService.Application.Models;
using PostingControlService.Application.Services;
using PostingControlService.Domain.Entities;
using PostingControlService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostingControlService.Application.Test
{
    [TestClass]
    public class TransactionServiceTests
    {
        private Mock<ITransactionRepository> _transactionRepositoryMock;
        private ITransactionService _transactionService;

        [TestInitialize]
        public void Setup()
        {
            _transactionRepositoryMock = new Mock<ITransactionRepository>();
            _transactionService = new TransactionService(_transactionRepositoryMock.Object);
        }

        [TestMethod]
        public async Task AddTransaction_ShouldCallRepository()
        {
            // Arrange
            var transactionDto = new TransactionDto { Type = "Credit", Amount = 100, CreatedAt = DateTime.Now, UserId = 1 };

            // Act
            await _transactionService.AddTransaction(transactionDto);

            // Assert
            _transactionRepositoryMock.Verify(repo => repo.AddTransaction(It.IsAny<Transaction>()), Times.Once);
        }

        [TestMethod]
        public async Task GetTransactions_ShouldReturnTransactions()
        {
            // Arrange
            var transactions = new List<Transaction>
            {
                new Transaction { Type = "Credit", Amount = 100, CreatedAt = DateTime.Now, UserId = 1 },
                new Transaction { Type = "Debit", Amount = 50, CreatedAt = DateTime.Now, UserId = 1 }
            };
            _transactionRepositoryMock.Setup(repo => repo.GetTransactions()).ReturnsAsync(transactions);

            // Act
            var result = await _transactionService.GetTransactions();

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public async Task GetDailyReport_ShouldReturnCorrectReport()
        {
            // Arrange
            var date = DateTime.Now.Date;
            var transactions = new List<Transaction>
            {
                new Transaction { Type = "Credit", Amount = 100, CreatedAt = date, UserId = 1 },
                new Transaction { Type = "Debit", Amount = 50, CreatedAt = date, UserId = 1 }
            };
            _transactionRepositoryMock.Setup(repo => repo.GetTransactions()).ReturnsAsync(transactions);

            // Act
            var result = await _transactionService.GetDailyReport(date);

            // Assert
            Assert.AreEqual(100, result.TotalCredits);
            Assert.AreEqual(50, result.TotalDebits);
            Assert.AreEqual(50, result.Balance);
        }
    }
}