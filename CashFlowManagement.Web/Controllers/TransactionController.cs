using CashFlowManagement.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowManagement.Web.Controllers
{
    [Authorize]
    public class TransactionController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TransactionController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("PostingControlService");
            var token = HttpContext.Session.GetString("JWToken");

            if (token != null)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await client.GetAsync("/api/transactions");

            if (response.IsSuccessStatusCode)
            {
                var transactions = await response.Content.ReadAsAsync<IEnumerable<TransactionDto>>();
                return View(transactions);
            }

            ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
            return View(new List<TransactionDto>());
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.TransactionTypes = new List<SelectListItem>
    {
        new SelectListItem { Value = "Credit", Text = "Credit" },
        new SelectListItem { Value = "Debit", Text = "Debit" }
    };
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TransactionDto transaction)
        {
            var client = _httpClientFactory.CreateClient("PostingControlService");
            var token = HttpContext.Session.GetString("JWToken");

            if (token != null)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                transaction.UserId = int.Parse(userId);

                var jsonContent = new StringContent(JsonConvert.SerializeObject(transaction), Encoding.UTF8, "application/json");
                var response = await client.PostAsync("https://localhost:9001/api/transactions", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Transaction created successfully!";
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
            ViewBag.TransactionTypes = new List<SelectListItem>
            {
                new SelectListItem { Value = "Credit", Text = "Credit" },
                new SelectListItem { Value = "Debit", Text = "Debit" }
            };
            return View(transaction);
        }


        public async Task<IActionResult> DailyReport(DateTime date)
        {
            if (date == default(DateTime))
            {
                date = DateTime.Now.Date;
            }

            var client = _httpClientFactory.CreateClient("PostingControlService");
            var token = HttpContext.Session.GetString("JWToken");

            if (token != null)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await client.GetAsync($"https://localhost:9001/api/transactions/dailyreport?date={date.ToString("yyyy-MM-dd")}");

            if (response.IsSuccessStatusCode)
            {
                var report = await response.Content.ReadAsAsync<DailyReportDto>();
                return View(report);
            }

            ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
            return View(new DailyReportDto { Date = date });
        }

    }
}
