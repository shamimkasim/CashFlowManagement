using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
 
using CashFlowManagement.Web.Models;
using CashFlowManagement.Web.Service.Interfaces;

namespace CashFlowManagement.Web.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TransactionService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task AddTransaction(TransactionDto transactionDto)
        {
            var client = _httpClientFactory.CreateClient("PostingControlService");
            var response = await client.PostAsJsonAsync("/api/transactions", transactionDto);
            response.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<TransactionDto>> GetTransactions()
        {
            var client = _httpClientFactory.CreateClient("PostingControlService");
            return await client.GetFromJsonAsync<IEnumerable<TransactionDto>>("/api/transactions");
        }

        public async Task<DailyReportDto> GetDailyReport(DateTime date)
        {
            var client = _httpClientFactory.CreateClient("DailyConsolidatedService");
            return await client.GetFromJsonAsync<DailyReportDto>($"/api/reports/daily?date={date:yyyy-MM-dd}");
        }
    }
}
