using DailyConsolidatedService.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace DailyConsolidatedService.Domain.Interfaces
{
    public interface IReportGenerationService
    {
        Task<DailyReport> GetDailyReport(DateTime date);
    }
}
