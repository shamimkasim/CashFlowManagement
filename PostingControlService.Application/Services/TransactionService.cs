using PostingControlService.Application.Interfaces;
using PostingControlService.Application.Models;
using PostingControlService.Domain.Entities;
using PostingControlService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostingControlService.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<TransactionDto> AddTransaction(TransactionDto transactionDto)
        {
            var transaction = new Transaction
            {
                Type = transactionDto.Type,
                Amount = transactionDto.Amount,
                CreatedAt = transactionDto.CreatedAt,
                UserId = transactionDto.UserId
            };
            await _transactionRepository.AddTransaction(transaction);
 
            transactionDto.Id = transaction.Id;
            return transactionDto;
        }

        public async Task<IEnumerable<TransactionDto>> GetTransactions()
        {
            var transactions = await _transactionRepository.GetTransactions();
            return transactions.Select(t => new TransactionDto
            {
                Id = t.Id,
                Type = t.Type,
                Amount = t.Amount,
                CreatedAt = t.CreatedAt,
                UserId = t.UserId
            });
        }

        public async Task<TransactionDto> GetTransactionById(int id)
        {
            var transaction = await _transactionRepository.GetTransactionById(id);
            if (transaction == null)
            {
                return null;
            }

            return new TransactionDto
            {
                Id = transaction.Id,
                Type = transaction.Type,
                Amount = transaction.Amount,
                CreatedAt = transaction.CreatedAt,
                UserId = transaction.UserId
            };
        }
        public async Task<DailyReportDto> GetDailyReport(DateTime date)
        {
            var transactions = await _transactionRepository.GetTransactions();

            var dailyTransactions = transactions.Where(t => t.CreatedAt.Date == date.Date);
            var totalCredits = dailyTransactions.Where(t => t.Type == "Credit").Sum(t => t.Amount);
            var totalDebits = dailyTransactions.Where(t => t.Type == "Debit").Sum(t => t.Amount);
            var balance = totalCredits - totalDebits;

            return new DailyReportDto
            {
                Date = date,
                TotalCredits = totalCredits,
                TotalDebits = totalDebits,
                Balance = balance
            };
        }
    }
}

