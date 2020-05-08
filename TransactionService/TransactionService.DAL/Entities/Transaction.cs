using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TransactionService.DAL.Entities.Enums;

namespace TransactionService.DAL.Entities
{
    public class Transaction
    {

        [MaxLength(50)]
        public string TransactionId { get; set; }

        public decimal Amount { get; set; }

        [MaxLength(3)]
        public string CurrencyCode { get; set; }

        public DateTime Date { get; set; }

        public TransactionStatus Status { get; set; }
    }
}
