using Microsoft.EntityFrameworkCore;
using PostingControlService.Domain.Entities;
using PostingControlService.Domain.Interfaces;
using PostingControlService.Infrastructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PostingControlService.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly TransactionContext _context;

        public TransactionRepository(TransactionContext context)
        {
            _context = context;
        }

        public async Task AddTransaction(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Transaction>> GetTransactions()
        {
            return await _context.Transactions.ToListAsync();
        }

        public async Task<Transaction> GetTransactionById(int id)
        {
            return await _context.Transactions.FindAsync(id);
        }
    }
}