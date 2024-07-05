using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DailyConsolidatedService.Application.Interfaces;
using System.Threading.Tasks;

namespace DailyConsolidatedService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("daily")]
        public async Task<IActionResult> GetDailyReport()
        {
            var report = await _reportService.GetDailyReport();
            return Ok(report);
        }
    }
}
