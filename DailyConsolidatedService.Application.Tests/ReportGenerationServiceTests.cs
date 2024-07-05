using DailyConsolidatedService.Application.Services;
using DailyConsolidatedService.Domain.Entities;
using DailyConsolidatedService.Domain.Interfaces;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace DailyConsolidatedService.Application.Tests
{
    public class ReportGenerationServiceTests
    {
        private readonly Mock<IReportRepository> _reportRepositoryMock;
        private readonly IReportGenerationService _reportService;

        public ReportGenerationServiceTests()
        {
            _reportRepositoryMock = new Mock<IReportRepository>();
            _reportService = new ReportGenerationService(_reportRepositoryMock.Object);
        }

        [Fact]
        public async Task GetDailyReport_ReturnsCorrectReport()
        {
            var date = DateTime.UtcNow;
            _reportRepositoryMock.Setup(repo => repo.GetDailyReport(date))
                .ReturnsAsync(new DailyReport());

            var result = await _reportService.GetDailyReport(date);

            Assert.NotNull(result);

        }
    }
}
