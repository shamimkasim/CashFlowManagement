using Microsoft.AspNetCore.Mvc;
using PostingControlService.Application.Interfaces;
using PostingControlService.Application.Models;
using System;
using System.Threading.Tasks;

namespace PostingControlService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost]
        public async Task<IActionResult> AddTransaction([FromBody] TransactionDto transaction)
        {
            var addedTransaction = await _transactionService.AddTransaction(transaction);
            return CreatedAtAction(nameof(GetTransactionById), new { id = addedTransaction.Id }, addedTransaction);
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactions()
        {
            var transactions = await _transactionService.GetTransactions();
            return Ok(transactions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransactionById(int id)
        {
            var transaction = await _transactionService.GetTransactionById(id);
            if (transaction == null)
            {
                return NotFound();
            }
            return Ok(transaction);
        }

         
        [HttpGet("dailyreport")]
        public async Task<IActionResult> GetDailyReport(DateTime date)
        {
            if (date == default(DateTime))
            {
                date = DateTime.Now.Date;  
            }

            var report = await _transactionService.GetDailyReport(date);
            if (report == null)
            {
                return NotFound();
            }
            return Ok(report);
        }

    }
}
