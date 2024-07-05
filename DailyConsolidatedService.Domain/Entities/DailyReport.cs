using System;
using System.ComponentModel.DataAnnotations;

namespace DailyConsolidatedService.Domain.Entities
{
    public class DailyReport
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalCredits { get; set; }
        public decimal TotalDebits { get; set; }
        public decimal Balance { get; set; }
    }
}