using System;
using System.Collections.Generic;
using System.Text;
using TransactionService.DAL.Entities;

namespace TransactionService.BLL.Models
{
    public class RecordParsingResult
    {
        public bool IsValid { get; set; }

        public string RawString { get; set; }

        public TransactionRecord Record { get; set; }
    }
}
