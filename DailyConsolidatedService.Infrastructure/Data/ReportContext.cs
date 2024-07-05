using DailyConsolidatedService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DailyConsolidatedService.Infrastructure.Data
{
    public class ReportContext : DbContext
    {
        public ReportContext(DbContextOptions<ReportContext> options) : base(options) { }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<DailyReport> DailyReports { get; set; }
    }
}
