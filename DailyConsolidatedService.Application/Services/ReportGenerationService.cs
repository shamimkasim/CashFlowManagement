 
using DailyConsolidatedService.Domain.Entities;
using DailyConsolidatedService.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace DailyConsolidatedService.Application.Services
{
    public class ReportGenerationService : IReportGenerationService
    {
        private readonly IReportRepository _reportRepository;

        public ReportGenerationService(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public async Task<DailyReport> GetDailyReport(DateTime date)
        {
            return await _reportRepository.GetDailyReport(date);
        }
    }
}