using System;

namespace PostingControlService.Domain.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
    }
}