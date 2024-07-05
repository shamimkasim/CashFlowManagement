using Microsoft.EntityFrameworkCore;
using PostingControlService.Domain.Entities;
namespace PostingControlService.Infrastructure.Data
{
    public class TransactionContext : DbContext
    {
        public TransactionContext(DbContextOptions<TransactionContext> options) : base(options) { }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<DailyReport> DailyReports { get; set; }
    }
}