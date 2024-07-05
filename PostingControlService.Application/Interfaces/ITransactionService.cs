using PostingControlService.Application.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PostingControlService.Application.Interfaces
{
    public interface ITransactionService
    {     
        Task<TransactionDto> AddTransaction(TransactionDto transactionDto);
        Task<IEnumerable<TransactionDto>> GetTransactions();
        Task<TransactionDto> GetTransactionById(int id);
        Task<DailyReportDto> GetDailyReport(DateTime date);
    }
}
