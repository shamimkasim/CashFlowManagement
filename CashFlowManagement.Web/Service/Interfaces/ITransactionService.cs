using CashFlowManagement.Web.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CashFlowManagement.Web.Service.Interfaces
{
    public interface ITransactionService
    {
        Task AddTransaction(TransactionDto transactionDto);
        Task<IEnumerable<TransactionDto>> GetTransactions();
        Task<DailyReportDto> GetDailyReport(DateTime date);
    }
}
