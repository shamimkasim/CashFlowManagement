using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PostingControlService.Api.Controllers;
using PostingControlService.Application.Interfaces;
using PostingControlService.Application.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PostingControlService.APIs.Tests
{
    [TestClass]
    public class TransactionsControllerTests
    {
        private Mock<ITransactionService> _transactionServiceMock;
        private TransactionsController _controller;

        [TestInitialize]
        public void Setup()
        {
            _transactionServiceMock = new Mock<ITransactionService>();
            _controller = new TransactionsController(_transactionServiceMock.Object);
        }

        [TestMethod]
        public async Task GetTransactions_ShouldReturnOkResultWithTransactions()
        {
            // Arrange
            var transactions = new List<TransactionDto>
        {
            new TransactionDto { Type = "Credit", Amount = 100, CreatedAt = DateTime.Now },
            new TransactionDto { Type = "Debit", Amount = 50, CreatedAt = DateTime.Now }
        };
            _transactionServiceMock.Setup(service => service.GetTransactions()).ReturnsAsync(transactions);

            // Act
            var result = await _controller.GetTransactions();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(transactions, okResult.Value);
        }

         
    }

}
