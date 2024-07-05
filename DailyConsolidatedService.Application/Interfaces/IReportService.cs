using DailyConsolidatedService.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyConsolidatedService.Application.Interfaces
{
    public interface IReportService
    {
        Task<DailyReportDto> GetDailyReport();
    }
}