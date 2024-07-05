using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DailyConsolidatedService.Domain.Entities;
using DailyConsolidatedService.Domain.Interfaces;
using DailyConsolidatedService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using PostingControlService.Domain.Entities;
using Transaction = DailyConsolidatedService.Domain.Entities.Transaction;

namespace DailyConsolidatedService.Infrastructure.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly ReportContext _context;

        public ReportRepository(ReportContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DailyReport>> GetDailyReportsAsync(DateTime date)
        {
            return await _context.DailyReports
                .Where(r => r.Date.Date == date.Date)
                .ToListAsync();
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByTypeAsync(string type)
        {
            return await _context.Transactions
                .Where(t => t.Type == type)
                .ToListAsync();
        }

        public async Task<DailyReport> GetDailyReport(DateTime date)
        {
            return await _context.DailyReports
                .FirstOrDefaultAsync(r => r.Date.Date == date.Date);
        }
    }
}