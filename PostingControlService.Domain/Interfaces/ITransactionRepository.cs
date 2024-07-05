using System.Collections.Generic;
using System.Threading.Tasks;
using PostingControlService.Domain.Entities;

namespace PostingControlService.Domain.Interfaces
{
    public interface ITransactionRepository
    {
        Task AddTransaction(Transaction transaction);
        Task<IEnumerable<Transaction>> GetTransactions();
        Task<Transaction> GetTransactionById(int id);
    }
}