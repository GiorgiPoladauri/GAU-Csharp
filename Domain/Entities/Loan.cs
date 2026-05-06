using Domain.Enums;
using System;

namespace Domain.Entities
{
    public class Loan
    {
        public int Id { get; set; }
        public int ReaderId { get; set; }
        public Reader Reader { get; set; } = null!;
        public int BookId { get; set; }
        public Book Book { get; set; } = null!;
        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public BookCondition? ReturnCondition { get; set; }
        public decimal PenaltyAmount { get; set; }
    }
}