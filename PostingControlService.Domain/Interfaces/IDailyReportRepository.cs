using PostingControlService.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace PostingControlService.Domain.Interfaces
{
    public interface IDailyReportRepository
    {
        Task<DailyReport> GetDailyReport(DateTime date);
        Task AddDailyReport(DailyReport dailyReport);
    }
}
