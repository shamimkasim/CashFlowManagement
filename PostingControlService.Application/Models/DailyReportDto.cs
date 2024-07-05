using System;

namespace PostingControlService.Application.Models
{
    public class DailyReportDto
    {
        public DateTime Date { get; set; }
        public decimal TotalCredits { get; set; }
        public decimal TotalDebits { get; set; }
        public decimal Balance { get; set; }
    }
}
