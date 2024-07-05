using DailyConsolidatedService.Domain.Entities;
using DailyConsolidatedService.Domain.Interfaces;
using DailyConsolidatedService.Infrastructure.Data;
using DailyConsolidatedService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;


namespace DailyConsolidatedService.Infrastructure.Tests
{
    public class ReportRepositoryTests
    {
        private readonly IReportRepository _repository;

        public ReportRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ReportContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            var context = new ReportContext(options);
            _repository = new ReportRepository(context);
        }

        [Fact]
        public async Task GetDailyReport_ShouldReturnDailyReport()
        {
            var date = DateTime.UtcNow;
            var result = await _repository.GetDailyReport(date);

            Assert.NotNull(result);
            
        }
    }
}
