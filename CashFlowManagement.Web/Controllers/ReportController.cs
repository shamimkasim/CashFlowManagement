using CashFlowManagement.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace CashFlowManagement.Web.Controllers
{
    public class ReportController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ReportController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Daily()
        {
            var client = _httpClientFactory.CreateClient("DailyConsolidatedService");
            var response = await client.GetAsync("https://localhost:49153/api/reports/daily");
            response.EnsureSuccessStatusCode();

            var dailyReport = await response.Content.ReadAsAsync<DailyReportDto>();
            return View(dailyReport);
        }
    }
}