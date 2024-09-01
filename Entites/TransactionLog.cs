using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingSystem.Enums;

namespace BankingSystem.Entites
{
    public class TransactionLog
    {
        public int Id {  get; set; }
        public int HolderAccountId { get; set; }
        public HolderAccount HolderAccount { get; set; }
        public decimal Amount {  get; set; }
        public string? From { get; set; }
        public string? To { get; set; }
        public TransactionType TransactionType { get; set; }
        public DateTime CreatedAt { get; set; }

        public TransactionLog(decimal amount, TransactionType transactionType, DateTime createdAt, string from, string to) 
        {
            Amount = amount;
            TransactionType = transactionType;
            CreatedAt = createdAt;
            From = from;
            To = to;
        }
        public TransactionLog(decimal amount, TransactionType transactionType, DateTime createdAt)
        {
            Amount = amount;
            TransactionType = transactionType;
            CreatedAt = createdAt;
        }
    }
}
