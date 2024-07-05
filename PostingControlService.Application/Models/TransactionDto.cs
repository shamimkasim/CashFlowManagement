using System;

namespace PostingControlService.Application.Models
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; } 
        public DateTime CreatedAt { get; set; }
    }
}