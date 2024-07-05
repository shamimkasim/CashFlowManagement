using System;

namespace CashFlowManagement.Web.Models
{
    public class TransactionDto
    {
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
    }
}
