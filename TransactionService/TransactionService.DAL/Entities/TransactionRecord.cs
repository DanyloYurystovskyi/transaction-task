using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TransactionService.DAL.Entities.Enums;

namespace TransactionService.DAL.Entities
{
    public class TransactionRecord
    {
        [Key]
        [MaxLength(50)]
        public string TransactionId { get; set; }

        //default 18,2 precision is good enough
        public decimal Amount { get; set; }

        [MaxLength(3)]
        public string CurrencyCode { get; set; }

        public DateTime Date { get; set; }

        public TransactionRecordStatus Status { get; set; }
    }
}
