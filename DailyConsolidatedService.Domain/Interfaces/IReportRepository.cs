using DailyConsolidatedService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DailyConsolidatedService.Domain.Interfaces
{
    public interface IReportRepository
    {
        Task<IEnumerable<DailyReport>> GetDailyReportsAsync(DateTime date);
        Task<IEnumerable<Transaction>> GetTransactionsByTypeAsync(string type);
        Task<DailyReport> GetDailyReport(DateTime date);
    }
}