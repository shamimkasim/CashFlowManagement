using Microsoft.EntityFrameworkCore;
using PostingControlService.Domain.Entities;
using PostingControlService.Domain.Interfaces;
using PostingControlService.Infrastructure.Data;
using System;
using System.Threading.Tasks;

namespace PostingControlService.Infrastructure.Repositories
{
    public class DailyReportRepository : IDailyReportRepository
    {
        private readonly TransactionContext _context;


        public DailyReportRepository(TransactionContext context)
        {
            _context = context;
        }

        public async Task<DailyReport> GetDailyReport(DateTime date)
        {
            return await _context.DailyReports.FirstOrDefaultAsync(dr => dr.Date.Date == date.Date);
        }

        public async Task AddDailyReport(DailyReport dailyReport)
        {
            _context.DailyReports.Add(dailyReport);
            await _context.SaveChangesAsync();
        }
    }
}